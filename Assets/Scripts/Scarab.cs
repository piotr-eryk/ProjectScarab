using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarab : MonoBehaviour
{
    public List<GameObject> possibleNeightbours;
    [HideInInspector] public List<GameObject> currentPossibleNeightbours;

    private void Awake()
    {
        CopyPossibleNeightbourToCurrentNeightbour();
    }

    public void ChangeScarab(Sprite scarab, Color? explosionColor = null)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = scarab;

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
        GameObject particleToPlay = ObjectPool.SharedInstance.GetPooledObject();
        ParticleSystem particleSystem = particleToPlay.GetComponentInChildren<ParticleSystem>();

        particleToPlay.SetActive(true);
        particleToPlay.transform.position = gameObject.transform.position;

        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = new ParticleSystem.MinMaxGradient(explosionColor);
        particleSystem.Play();
    }

}
