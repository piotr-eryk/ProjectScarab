using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunButton : BaseButton
{
    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Projectile"))
        {
            isPressed = true;
        }
    }
}
