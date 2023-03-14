using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Action OnCollectOrb;

    private GrabbableObject grabbableObject;

    [Header("Grabbable Objects")]
    [SerializeField]
    private float grabbingDistance = 2f;
    [SerializeField]
    private float throwingForce = 4f;
    [SerializeField]
    private Camera playerCamera;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (grabbableObject)
            {
                grabbableObject.Release();
                grabbableObject = null;
            }

            else
            {
                if (Physics.Raycast(
                    transform.position,
                    playerCamera.transform.forward,
                    out RaycastHit hit,
                    grabbingDistance) && hit.transform.GetComponent<GrabbableObject>())

                {
                    grabbableObject = hit.transform.GetComponent<GrabbableObject>();
                    grabbableObject?.Hold();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Orb>())
        {
            OnCollectOrb?.Invoke();
        }
    }
}
