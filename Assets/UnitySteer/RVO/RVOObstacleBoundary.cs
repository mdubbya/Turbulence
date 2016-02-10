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
        public List<Vector2> vertices;
        private int id = -1;

        public void Awake()
        {
            id = RVOController.Instance.AddRVOObstacle(vertices);
        }

        public void AddPoint(Vector2 point)
        {
            if(vertices== null)
            {
                vertices = new List<Vector2>();
            }
            vertices.Add(point);      
        }

        public void RemovePoint(Vector2 point)
        {
            if(vertices==null)
            {
                vertices = new List<Vector2>();
            }
            vertices.Remove(point);
        }

        public void ClearPoints()
        {
            if(vertices == null)
            {
                vertices = new List<Vector2>();
            }
            vertices.Clear();
        }

        public void OnDestroy()
        {
            //RVOController.Instance.RemoveRVOObstacle(id);
        }
    }
}
