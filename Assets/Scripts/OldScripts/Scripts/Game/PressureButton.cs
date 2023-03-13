using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : Basebutton
{
    [SerializeField] 
    private float pressedDuration = 1f;

    private float pressedTimer;

    protected override void Update()
    {
        pressedTimer -= Time.deltaTime;
        if (pressedTimer > 0)
        {
            targetPosition = new Vector3(0, pressureHeight, 0);

            if (isPressed == false)
            {
                isPressed = !isPressed;
                OnPress();
            }
        }
        else
        {
            targetPosition = Vector3.zero;
            if (isPressed == true)
            {
                isPressed = !isPressed;
                OnUnpress();
            }
        }
        model.transform.localPosition = Vector3.Lerp (model.transform.localPosition, targetPosition, Time.deltaTime * pressedSpeed);
    }

    void OnTriggerStay(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Player>() != null || otherCollider.GetComponent<GrabbableObject>() != null)
        {
            pressedTimer = pressedDuration;
        }
    }
                  
    protected override void OnPress ()
    {
        model.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.green);

        targetObject.OnTrigger();
    }
    private void OnUnpress()
    {
        model.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);

        targetObject.OnUnTrigger();
    }
}
