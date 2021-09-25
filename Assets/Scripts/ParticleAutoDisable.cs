using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDisable : MonoBehaviour
 {
     private ParticleSystem particle;
 
     public void Start()
     {
         particle = GetComponentInChildren<ParticleSystem>();
     }
 
     public void FixedUpdate()
     {
         if (particle && !particle.IsAlive())
         {
            gameObject.SetActive(false);
         }
     }
 }