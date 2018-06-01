using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameControl;
using Zenject;

namespace AI.PathCalculation
{
    public class Radar : MonoBehaviour, IRadar 
    {
        public float detectionRadius;
        [Inject]
        TeamInfo _teamInfo;

        private List<GameObject> _detectedEntities =new List<GameObject>();


        private void FixedUpdate()
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            _detectedEntities = (from p in nearbyColliders select p.gameObject).ToList();
        }

        public List<GameObject> GetAllDetected()
        {
            return _detectedEntities;
        }

        public GameObject GetClosestDetected()
        {
            return GetClosest(_detectedEntities);
        }

        public GameObject GetClosestDetectedEnemy()
        {
            return GetClosest(GetDetectedEnemies());
        }

        public List<GameObject> GetDetectedEnemies()
        {
            List<GameObject> detectedEnemies = (from p in _detectedEntities
                          where
            _teamInfo.enemyTeams.Contains(p.GetComponent<TeamInfo>().owningTeam)
                          select p.gameObject).ToList();
            if(detectedEnemies.Count==0)
            {
                detectedEnemies= null;
            }
            return detectedEnemies;
        }

        private GameObject GetClosest(List<GameObject> entities)
        {
            if(entities!=null && entities.Count>0)
            {
                return entities.Aggregate(
                        (minItem,NextItem) => 
                        (Vector3.Distance(minItem.transform.position,transform.position) < 
                        Vector3.Distance(NextItem.transform.position,transform.position)) ? minItem : NextItem);
            }
            else
            {
                return null;
            }
        }
    }
}