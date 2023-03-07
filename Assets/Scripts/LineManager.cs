using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private List<Transform> points = new();

    private void Awake()
    {
        lineRenderer.positionCount = 0;
    }

    public void AddPoint(Transform point)
    {
        lineRenderer.positionCount++;
        points.Add(point);
    }

    void LateUpdate()
    {
        if (points.Count >= 2)
        {
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }
        }
    }

    public void ResetAllPoints()
    {
        lineRenderer.positionCount = 0;
        points.Clear();
    }
}