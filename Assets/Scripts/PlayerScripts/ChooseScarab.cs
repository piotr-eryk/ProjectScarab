using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseScarab : MonoBehaviour
{
    [SerializeField] 
    LayerMask scarabLayer;
    [SerializeField] 
    float activeDistance = 10f;

    private Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    public void ClickOnScarab()
    {
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, activeDistance, scarabLayer))
        {
            if (hit.collider.GetComponent<Scarab>())
                hit.collider.GetComponentInParent<GraphPuzzle>().ScarabIsChosen(hit.collider.gameObject);
        }
    }
}
