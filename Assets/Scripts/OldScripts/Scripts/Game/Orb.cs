using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] 
    private float rotatingSpeed = 100f;

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.right, rotatingSpeed * Time.deltaTime);
        transform.RotateAround(transform.position, Vector3.up, rotatingSpeed * Time.deltaTime);
        transform.RotateAround(transform.position, Vector3.forward, rotatingSpeed * Time.deltaTime);
    }
}
