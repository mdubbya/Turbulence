using System.Collections.Generic;
using UnityEngine;

namespace AI.PathCalculation
{
    public interface IRadar
    {
        List<GameObject> GetDetectedEnemies();
        List<GameObject> GetAllDetected();

        GameObject GetClosestDetected();

        GameObject GetClosestDetectedEnemy();
    }
}