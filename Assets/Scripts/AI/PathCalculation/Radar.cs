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
        public List<GameObject> GetDetectedEnemies()
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            return (from p in nearbyColliders
                          where
            _teamInfo.enemyTeams.Contains(p.GetComponent<TeamInfo>().owningTeam)
                          select p.gameObject).ToList();
        }
    }
}