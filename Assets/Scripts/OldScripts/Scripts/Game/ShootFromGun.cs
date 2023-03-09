using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFromGun : MonoBehaviour
{
    [SerializeField] 
    private float bulletSpeed = 4f;
    [SerializeField] 
    private GameObject model;
    [SerializeField] 
    private float pressureHeight = -10f;

    private float shootDelay = 1.0f;
    private float elapsed = 0f;

    protected Vector3 targetPosition;

    public float ShootDelay
    {
        get { return shootDelay; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("You can't set a negative shoot delay!");
            }
            else
            {
                shootDelay = value;
            }
        }
    }

    void Start()
    {
        targetPosition = Vector3.zero;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > ShootDelay)
        {
            targetPosition = new Vector3(0, 0, pressureHeight);
            elapsed = 0.0f;
            SpawnBullet();
        }
        else
        {
            targetPosition = Vector3.zero;
        }
        model.transform.localPosition = Vector3.Lerp(model.transform.localPosition, targetPosition, Time.deltaTime * ShootDelay);
    }
    private void SpawnBullet()
    {
        GameObject bulletObject = ObjectPool.SharedInstance.GetPooledObject();

        if (bulletObject)
        {
            Rigidbody rigidbody = bulletObject.GetComponent<Rigidbody>();
            rigidbody.angularVelocity = Vector3.zero;

            bulletObject.transform.position = transform.position + transform.forward * 2f;
            bulletObject.transform.rotation = transform.rotation;
            bulletObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            rigidbody.velocity = transform.forward * bulletSpeed;

            bulletObject.SetActive(true);
        }
    }
}
