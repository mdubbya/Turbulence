using UnityEngine;
using System.Collections.Generic;

namespace AI
{
    class TargetSelectionController : MonoBehaviour
    {
        private TargetSelectorBase[] targetSelectors;
        private TargetModifierBase[] targetModifiers;
        public Transform navTarget;

        public void Start()
        {
            targetSelectors = GetComponents<TargetSelectorBase>();
        }

        public void FixedUpdate()
        {
            Vector3 currentPosition = transform.position;

            Vector3 targetPosition = new Vector3(); 

            if (targetSelectors != null)
            {
                foreach (TargetSelectorBase sel in targetSelectors)
                {
                    targetPosition = sel.GetNewTargetPosition(targetPosition);
                }

                if (targetModifiers != null)
                {
                    foreach (TargetModifierBase mod in targetModifiers)
                    {
                        //targetPosition = mod.GetNewTargetPosition(targetPosition,)
                    }
                }
            }

            navTarget.position = targetPosition;
        }
    }
}
