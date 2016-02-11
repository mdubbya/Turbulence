using UnitySteer.RVO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RVOObstacleBoundary))]
public class ObstacleBoundaryEditor : Editor
{
    private static bool editModeOn = false;
    

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector(); ;
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
            ((RVOObstacleBoundary)target).ClearPoints();
        }


    }

    public void OnSceneGUI()
    {
        if (editModeOn)
        {
            int controlId = GUIUtility.GetControlID(FocusType.Passive);
            GUIUtility.hotControl = controlId;
            if (Event.current.type == EventType.mouseDown)
            {

                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;


                if (Physics.Raycast(worldRay, out hitInfo))
                {
                    Vector3 point = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                    RVOObstacleBoundary obstacle = ((RVOObstacleBoundary)target);
                    obstacle.AddPoint(point);
                }
                Event.current.Use();
            }
        }
    }

}


    public class RVOObstacleBoundaryGizmoDrawer
    {

        [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.Pickable)]
        static void DrawGizmoForRVOObstacleBoundary(RVOObstacleBoundary boundary, GizmoType gizmoType)
        {
            if (boundary.points != null)
            {
                foreach (GameObject point in boundary.points)
                {
                    Gizmos.DrawSphere(point.transform.position, 0.5f);
                }
            }
        }

    }
