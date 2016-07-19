using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace AI
{
    public class TargetSelectionController : MonoBehaviour
    {
        private List<ITargetModifier> targetModifiers;
        private List<IAIWeaponController> weapons; 

        public void Start()
        {
            targetModifiers = GetComponents<ITargetModifier>().ToList();
            targetModifiers = targetModifiers.OrderByDescending(p => p.priority).ToList();
            weapons = GetComponents<IAIWeaponController>().ToList();
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
            if (weapons != null)
            {
                foreach(IAIWeaponController weapon in weapons)
                {
                    weapon.AttackIfTargetValid(targetInfo);
                }
            }
        }

    }
}
