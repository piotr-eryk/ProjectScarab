using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [SerializeField] 
    private GunButton gunButtonScript;
    [SerializeField] 
    private BreakableObject targetObject;

    private int reflections = 5;
    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (!hit.collider.gameObject.GetComponent<Mirror>())
                    break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction);
            }
        }
        switch (hit.collider.tag)
        {
            case "Player":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "GunButton":
                gunButtonScript.IsPressed = true;
                break;
            case "BreakableObject":
                OnTouch();
                break;
        }
    }

    public void OnTouch()
    {
        targetObject.OnTouch();
    }
}
