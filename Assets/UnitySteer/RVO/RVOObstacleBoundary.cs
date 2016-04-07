using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnitySteer.RVO
{
    [ExecuteInEditMode]
    public class RVOObstacleBoundary : MonoBehaviour
    {
        [SerializeField]
        public List<GameObject> points;

        public void Awake()
        {
            if (points != null)
            {
                List<Vector2> vertices = (from p in points select new Vector2(p.transform.position.x, p.transform.position.y)).ToList();
                if (vertices != null)
                {
                    
                }
            }
        }
    }
}
