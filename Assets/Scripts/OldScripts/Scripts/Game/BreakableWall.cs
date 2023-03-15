using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class BreakableWall : MonoBehaviour, IBreakable
{
    [SerializeField] 
    private GameObject model;
    [SerializeField] 
    private float destroyDuration = 3f;
    [SerializeField] 
    private GameObject explodeParticle;

    private ObjectPool<GameObject> explodeParticlePool;

    private float t = 0f;

    private void Awake()
    {
        explodeParticlePool = new ObjectPool<GameObject>(createFunc: () => Instantiate(explodeParticle),
actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false),
actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 20, maxSize: 50);
    }

    void Update()
    {
        if (model && t > destroyDuration)
        {
            Explode();
            Destroy(model);
        }
    }

    public void OnTouch()
    {
        t += Time.deltaTime;
    }

    private void Explode()
    {
        explodeParticle = explodeParticlePool.Get();
        explodeParticle.GetComponent<ParticleSystem>().Play();
        //        explodeParticlePool.Release(explodeParticle);
    }

}
