using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ITriggable
{
    [SerializeField] 
    private GameObject doorTop;
    [SerializeField] 
    private GameObject doorBot;
    [SerializeField] 
    private float speed = 3f;
    [SerializeField] 
    private float openedHeightTop;
    [SerializeField] 
    private float openedHeightBot;

    private Vector3 targetPositionTop;
    private Vector3 targetPositionBot;

    void Start()
    {
        targetPositionTop = Vector3.zero;
        targetPositionBot = Vector3.zero;
    }

    void Update()
    {
        doorTop.transform.localPosition = Vector3.Lerp(doorTop.transform.localPosition, targetPositionTop, Time.deltaTime * speed);
        doorBot.transform.localPosition = Vector3.Lerp(doorBot.transform.localPosition, targetPositionBot, Time.deltaTime * speed);
    }

    public void OnTrigger()
    {
        targetPositionTop = new Vector3(0, openedHeightTop, 0);
        targetPositionBot = new Vector3(0, openedHeightBot, 0);
    }

    public void OnUnTrigger()
    {
        targetPositionTop = Vector3.zero;
        targetPositionBot = Vector3.zero;
    }
}
