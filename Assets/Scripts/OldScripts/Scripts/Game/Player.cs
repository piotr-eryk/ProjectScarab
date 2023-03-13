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
    private Sprite cubeIcon;

    private Camera playerCamera;

    void Awake()
    {
        playerCamera = transform.GetComponentInChildren<Camera>();
    }

    void Start()
    {
        //cubeIcon = false;
    }

    void Update()
    {
        if (grabbableObject)
        {
            grabbableObject.transform.LookAt(transform);

            if (Input.GetMouseButtonUp(0))
            {
                Release();
             //   cubeIcon.enabled = false;

            }
        }
        else if (!grabbableObject)
        {
            if (Physics.Raycast(transform.position, playerCamera.transform.forward, out RaycastHit hit, grabbingDistance) &&
                hit.transform.GetComponent<GrabbableObject>() && Input.GetMouseButtonUp(0)) //if player dont hold grabbable object + player click at grabbable object
            {
                GrabbableObject targetObject = hit.transform.GetComponent<GrabbableObject>();
                Hold(targetObject);
              //  cubeIcon.enabled = true;
            }
        }
        if (grabbableObject != null)
        {
            grabbableObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * grabbingDistance;
        }
    }

    private void Hold(GrabbableObject targetObject)
    {
        grabbableObject = targetObject;
        grabbableObject.GetComponent<Collider>().enabled = false;
        grabbableObject.GetComponent<Rigidbody>().useGravity = false;
    }

    private void Release()
    {
        grabbableObject.GetComponent<Collider>().enabled = true;
        grabbableObject.GetComponent<Rigidbody>().useGravity = true;
        grabbableObject.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * throwingForce);
        grabbableObject = null;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log(otherCollider.name);
        if (otherCollider.GetComponent<Orb>() != null)
        {
            Debug.Log("Tu");
            OnCollectOrb?.Invoke();
        }
    }
}
