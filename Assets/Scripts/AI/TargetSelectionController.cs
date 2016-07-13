using UnityEngine;
using System.Collections.Generic;

namespace AI
{
    class TargetSelectionController : MonoBehaviour
    {
        private TargetSelectorBase[] targetSelectors;
        private TargetModifierBase[] targetModifiers;       

        public void Start()
        {
            targetSelectors = GetComponents<TargetSelectorBase>();
            targetModifiers = GetComponents<TargetModifierBase>();
        }

        private Vector3 _targetPosition;
        public Vector3 targetPosition
        {
            get { return _targetPosition; }
        }


        public void FixedUpdate()
        {            
            if (targetSelectors != null)
            {
                foreach (TargetSelectorBase sel in targetSelectors)
                {
                    _targetPosition = sel.GetNewTargetPosition(targetPosition);
                }
            }            
        }
    }
}
