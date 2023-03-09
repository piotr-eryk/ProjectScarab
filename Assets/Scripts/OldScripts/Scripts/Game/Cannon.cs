using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] 
    private Transform target;
    [SerializeField] 
    private List<GameObject> panels;
    [SerializeField] 
    private ShootFromGun shootFromGunScript;

    private float scriptDelay;

    void Awake()
    {
        scriptDelay = shootFromGunScript.ShootDelay;
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return StartCoroutine(LightPanel()); 
            yield return StartCoroutine(PutPanel());
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90f, 0);
        }
    }

    private IEnumerator LightPanel()
    {
        WaitForSeconds wait = new WaitForSeconds(scriptDelay / panels.Count);
        foreach (GameObject panel in panels)
        {
            yield return wait;
            panel.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private IEnumerator PutPanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.GetComponent<Renderer>().material.color = Color.red;
            yield return null;
        }
    }
}
