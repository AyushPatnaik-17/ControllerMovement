using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathDrawer))]
public class PathEditor : Editor
{
    void OnSceneGUI()
    {
        PathDrawer pathDrawer = (PathDrawer)target;
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && e.shift)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Undo.RecordObject(pathDrawer, "Add Path Point");
                pathDrawer.AddPoint(hit.point);
                e.Use();
            }
        }
    }
}