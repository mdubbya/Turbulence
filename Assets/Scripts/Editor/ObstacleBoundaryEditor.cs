using UnitySteer.RVO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RVOObstacleBoundary))]
public class ObstacleBoundaryEditor : Editor
{
    private static bool editModeOn = false;


    public void OnSceneGUI()
    {
        if (editModeOn)
        {
            if (Event.current.type == EventType.mouseDown)
            {

                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;


                if (Physics.Raycast(worldRay, out hitInfo))
                {
                    UnitySteer.RVO.Vector2 point = new UnitySteer.RVO.Vector2(hitInfo.point.x, hitInfo.point.z);
                    RVOObstacleBoundary obstacle = (RVOObstacleBoundary)target;
                    obstacle.AddPoint(point);
                }

            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (editModeOn)
        {
            if (GUILayout.Button("Disable Editing"))
            {
                editModeOn = false;
            }
        }
        else
        {
            if (GUILayout.Button("Enable Editing"))
            {
                editModeOn = true;
            }
        }

        if (GUILayout.Button("Clear Points"))
        {
            RVOObstacleBoundary obstacle = (RVOObstacleBoundary)target;
            obstacle.ClearPoints();
        }


    }

}

public class RVOObstacleBoundaryGizmoDrawer
{

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmoForRVOObstacleBoundary(RVOObstacleBoundary boundary, GizmoType gizmoType)
    {
        foreach(UnitySteer.RVO.Vector2 point in boundary.vertices)
        {
            Gizmos.DrawSphere(new Vector3(point.x, 0, point.y), 0.5f);            
        }
    }

}

