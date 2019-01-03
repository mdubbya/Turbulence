using System;
using System.Collections.Generic;
using System.Linq;
using AI.PathCalculation;
using Component;
using UnityEngine;


namespace AI.Task
{
    //TODO: needs optimization
    public class AttackClosestTargetInRange : MonoBehaviour, IAITask
    {
        public float relativePriority;

        private IRadar _radar;
        private RVOAgent _rvoAgent;
        private Rigidbody _rigidBody;
        public AITaskType taskType { get { return AITaskType.Attack; } }

        public void Start()
        {
            _radar = gameObject.GetComponent<IRadar>();
            _rvoAgent = GetComponent<RVOAgent>();
            _rigidBody = GetComponent<Rigidbody>();
        }


        public float GetPriority()
        {
            GameObject enemy = _radar.GetClosestDetectedEnemy();
            if (enemy != null)
            {
                return relativePriority / Vector3.Distance(enemy.transform.position, transform.position);
            }
            else
            {
                return 0;
            }
        }

        public Vector3 GetTarget()
        {
            GameObject enemy = _radar.GetClosestDetectedEnemy();
            Vector3 target = Vector3.zero;
            if (enemy != null)
            {
                GameObject closest = _radar.GetClosestDetected();
                target = closest == null ? target : closest.transform.position;
            }
            return target;
        }
    }
}