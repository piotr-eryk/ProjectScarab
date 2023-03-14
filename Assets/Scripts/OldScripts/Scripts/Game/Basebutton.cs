using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BaseButton : MonoBehaviour
{
    [SerializeField]
    protected TriggableObject triggableObject;
    [SerializeField]
    protected GameObject model;
    [SerializeField]
    protected float pressureHeight = -1.6f;
    [SerializeField]
    protected float pressedSpeed = 3f;

    protected Vector3 targetPosition;
    protected bool isPressed;

    public bool IsPressed 
    { 
        get { return isPressed; } 
        set { isPressed = value; } 
    }

    protected void Start()
    {
        targetPosition = Vector3.zero;
    }
    protected virtual void Update()
    {
        if (isPressed == true)
        {
            targetPosition = new Vector3(0, 0, pressureHeight);
            OnPress();
        }
        model.transform.localPosition = Vector3.Lerp(model.transform.localPosition, targetPosition, Time.deltaTime * pressedSpeed); //if button is pressed move attach model
    }

    protected virtual void OnPress()
    {
        triggableObject.OnTrigger();
    }
}
