using UnityEngine;
using System.Collections.Generic;

public class PathDrawer : MonoBehaviour
{
    public List<Vector3> pathPoints = new List<Vector3>();
    public GameObject prefab;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(pathPoints[i], pathPoints[i + 1]);
            Gizmos.DrawSphere(pathPoints[i], 0.1f);
        }
        if (pathPoints.Count > 0)
        {
            Gizmos.DrawSphere(pathPoints[pathPoints.Count - 1], 0.1f);
        }
    }

    public void AddPoint(Vector3 point)
    {
        pathPoints.Add(point);
        Instantiate(prefab, point, Quaternion.identity);
    }
}