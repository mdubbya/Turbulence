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
        public float scanRate;
        [Inject]
        TeamInfo _teamInfo;

        private List<GameObject> _detectedEntities =new List<GameObject>();
        private List<GameObject> _detectedEnemies;

        GameObject _closestDetected;
        GameObject _closestEnemy;
        
        float _timeSinceLastScan=0;

        private void Update()
        {
            _timeSinceLastScan+=Time.deltaTime;
            if(_timeSinceLastScan>=scanRate)
            {
                _timeSinceLastScan=0;
                UpdateAllDetected();
                UpdateDetectedEnemies();
                UpdateClosestDetected();
                UpdateClosestDetectedEnemy();
            }
        }

        public List<GameObject> GetAllDetected()
        {
            return _detectedEntities;
        }

        private void UpdateAllDetected()
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            _detectedEntities = (from p in nearbyColliders where p.gameObject.transform.position!=transform.position select p.gameObject).ToList();
        }

        public GameObject GetClosestDetected()
        {
            return _closestDetected;
        }

        private void UpdateClosestDetected()
        {
            _closestDetected = GetClosest(_detectedEntities);
        }

        public GameObject GetClosestDetectedEnemy()
        {
            return _closestEnemy;
        }

        private void UpdateClosestDetectedEnemy()
        {
            _closestEnemy = GetClosest(_detectedEnemies);
        }

        public List<GameObject> GetDetectedEnemies()
        {
            return _detectedEnemies;
        }

        private void UpdateDetectedEnemies()
        {
            List<GameObject> detectedEnemies = (from p in _detectedEntities
                          where
            _teamInfo.enemyTeams.Contains(p.GetComponent<TeamInfo>().owningTeam)
                          select p.gameObject).ToList();
            if(detectedEnemies.Count==0)
            {
                detectedEnemies= null;
            }
            _detectedEnemies = detectedEnemies;
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