using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mirror : TriggableObject
{
    private Vector3 targetAngle;
    private Vector3 currentAngle;

    [SerializeField] 
    private float speed = 3f;
    [SerializeField] 
    private Vector3 targetRotate;

    void Start()
    {
        currentAngle = transform.eulerAngles;
    }
    void Update()
    {
        currentAngle = new Vector3(
        Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime * speed),
        Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime * speed),
        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime * speed));

        transform.eulerAngles = currentAngle;
    }

    public override void OnTrigger()
    {
        base.OnTrigger();

        targetAngle = targetRotate;
    }

    public override void OnUnTrigger()
    {
        base.OnUnTrigger();

        targetAngle = Vector3.zero;
    }
}
