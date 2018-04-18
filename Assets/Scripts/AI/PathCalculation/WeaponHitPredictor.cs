//using UnityEngine;
//using System.Collections.Generic;
//using System.Linq;

//namespace AI
//{
//    public class FireAllWhenAligned
//    {
//        public float targetZoneWidth;
//        private List<IWeaponController> weapons;


//        public void UpdateObjectiveInfo()
//        {
//            Vector3 checkVector = transform.position + ((_commandInfo.attackTarget - transform.position).magnitude * transform.parent.forward);
//            if (Vector3.Distance(checkVector, _commandInfo.attackTarget) < targetZoneWidth)
//            {
//                foreach (IWeaponController weapon in weapons)
//                {
//                    weapon.Fire();
//                }
//            }
//        }
//    }
//}