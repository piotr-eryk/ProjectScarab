using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LaserSphere : MonoBehaviour, ITriggable
{
    [SerializeField] 
    private GameObject model;
    [SerializeField] 
    private bool rotateRight = true;
    [SerializeField] 
    private float rotateVelocity = 0;

    private float actualRotateVelocity = 0.3f;

    void Start()
    {
        OnUnTrigger();
    }
    void Update()
    {
        model.transform.Rotate(0, 0, actualRotateVelocity, Space.Self);
    }

    public void OnTrigger()
    {
        actualRotateVelocity = 0;
    }

    public void OnUnTrigger()
    {
        if (rotateRight == true)
        {
            actualRotateVelocity = 1f * rotateVelocity;
        }
        else
        {
            actualRotateVelocity = -1f * rotateVelocity;
        }
    }
}
