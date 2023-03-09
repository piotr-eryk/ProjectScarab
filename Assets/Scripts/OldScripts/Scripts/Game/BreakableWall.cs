using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BreakableWall : BreakableObject
{
    [SerializeField] 
    private GameObject model;
    [SerializeField] 
    private float destroyDuration = 3f;
    [SerializeField] 
    private GameObject particle;

    private float t = 0f;

    void Update()
    {
        if (model && t > destroyDuration)
        {
            Explode();
            Destroy(model);
        }
    }
    public override void OnTouch()
    {
        t += Time.deltaTime;
        base.OnTouch();
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(particle, model.transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, 2f);
    }

}
