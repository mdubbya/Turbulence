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
        private int id = -1;

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

        public void AddPoint(Vector3 point)
        {
            if(points== null)
            {
                points = new List<GameObject>();
            }
            GameObject newPoint = new GameObject();
            newPoint.transform.position = point;
            newPoint.transform.parent = transform;
            points.Add( newPoint);      
        }

        public void RemovePoint(GameObject point)
        {
            if(points==null)
            {
                points = new List<GameObject>();
            }
            points.Remove(point);
        }

        public void ClearPoints()
        {

            if (points == null)
            {
                points = new List<GameObject>();
            }
            if (Application.isEditor)
            {
                foreach (var p in points)
                {
                    DestroyImmediate(p);
                }
            }
            else
            {
                foreach(var p in points)
                {
                    Destroy(p);
                }
            }
            points.Clear();
        }

        public void OnDestroy()
        {
            //RVOController.Instance.RemoveRVOObstacle(id);
        }
    }
}
