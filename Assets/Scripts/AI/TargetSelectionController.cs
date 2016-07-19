using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace AI
{
    public class TargetSelectionController : MonoBehaviour
    {
        private List<ITargetModifier> targetModifiers;       

        public void Start()
        {
            targetModifiers = GetComponents<ITargetModifier>().ToList();
            targetModifiers = targetModifiers.OrderByDescending(p => p.priority).ToList();
        }


        public void FixedUpdate()
        {
            AITargetInfo targetInfo = new AITargetInfo(new Vector3(), false, null);
            if (targetModifiers != null)
            {
                foreach (ITargetModifier sel in targetModifiers)
                {
                    targetInfo = sel.GetNewTargetInfo(targetInfo);
                }
            }
            IShipMovementController movementController = GetComponent<IShipMovementController>();
            if(movementController!=null)
            {
                movementController.ApplyMovementControl(targetInfo);
            }    
        }

    }
}
