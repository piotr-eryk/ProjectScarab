using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Scarab : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> possibleNeightbours;
    [SerializeField]
    private GameObject scarabParticlePrefab;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private new ParticleSystem particleSystem;

    private List<GameObject> currentPossibleNeightbours = new();
    private ObjectPool<GameObject> scarabParticlePool;

    public List<GameObject> CurrentPossibleNeightbours => currentPossibleNeightbours;
    public List<GameObject> PossibleNeightbours => possibleNeightbours;

    public SpriteRenderer SpriteRenderer => spriteRenderer;

    private void Awake()
    {
        CopyPossibleNeightbourToCurrentNeightbour();
        scarabParticlePool = new ObjectPool<GameObject>(createFunc: () => Instantiate(scarabParticlePrefab),
    actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false),
    actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 20, maxSize: 50);
    }

    public void ChangeScarab(Color scarabColor, Color? explosionColor = null)
    {
        spriteRenderer.color = scarabColor;

        if (explosionColor != null)
        {
            Explode((Color)explosionColor);
        }
    }

    public void RemoveNeightbour(GameObject neightbourToRemove)
    {
        currentPossibleNeightbours.Remove(neightbourToRemove);
    }

    public void CopyPossibleNeightbourToCurrentNeightbour()
    {
        currentPossibleNeightbours.Clear();
        currentPossibleNeightbours.AddRange(possibleNeightbours);
    }

    public void Explode(Color explosionColor)
    {
        scarabParticlePrefab = scarabParticlePool.Get();
        scarabParticlePrefab.transform.position = gameObject.transform.position;
        scarabParticlePrefab.SetActive(true);
        scarabParticlePrefab.GetComponent<ParticleSystem>().Play();

        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = new ParticleSystem.MinMaxGradient(explosionColor);
        particleSystem.Play();
    }
}
