using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDisable : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    public void FixedUpdate()
    {
        if (particle && !particle.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
}