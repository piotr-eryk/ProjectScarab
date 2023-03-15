using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private bool isGrabbed = false;

    [Header("Grabbable Objects")]
    [SerializeField]
    private float grabbingDistance = 2f;
    [SerializeField]
    private Player player;

    void Update()
    {
        if (isGrabbed)
        {
            transform.position = GetComponent<Camera>().transform.position + GetComponent<Camera>().transform.forward * grabbingDistance;
            transform.LookAt(player.transform);
        }
    }

    public void Hold()
    {
        EnablePhysics(false);
    }

    public void Release()
    {
        EnablePhysics(true);
    }

    private void EnablePhysics(bool enable)
    {
        GetComponent<Collider>().enabled = enable;
        GetComponent<Rigidbody>().useGravity = enable;
    }
}
