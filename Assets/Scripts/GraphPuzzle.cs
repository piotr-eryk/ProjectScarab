using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class GraphPuzzle : MonoBehaviour
{
    [SerializeField] 
    GameObject player;
    [SerializeField]
    GameObject scarabParticlePrefab;
    [SerializeField]
    List<Scarab> scarabs;

    [Header("Scarabs Sprite")]
    [SerializeField] 
    private Sprite goldenScarab;
    [SerializeField] 
    private Sprite silverScarab;
    [SerializeField] 
    private Sprite stoneScarab;

    [Header("Graphic effects")]
    [SerializeField] 
    private LineManager lineManagerScript;
    [SerializeField] 
    private Text winnerText;
    [SerializeField] 
    private ParticleSystem scarabTrail;
    [SerializeField] 
    private GameObject scarabTrailContainer;

    [Header("Sound effects")]
    [SerializeField] 
    private AudioClip gongSound;
    [SerializeField] 
    private AudioClip resetSound;
    [SerializeField] 
    private AudioClip clickSound;
    [SerializeField]
    private AudioSource audioSource;

    private Scarab prevNode;
    private Scarab currentScarab;
    private ObjectPool<GameObject> scarabParticlePool;

    private void Start()
    {
        winnerText.enabled = false;
        for (int i = 0; i < scarabs.Count; i++)
        {
            scarabs[i].SpriteRenderer.sprite = stoneScarab;
        }
    }

    public void ScarabIsChosen(Scarab scarab)
    {
        audioSource.PlayOneShot(clickSound, 0.2f);

        currentScarab = scarab;

        if (prevNode)
        {
            Scarab prevScarab = prevNode;
            currentScarab.RemoveNeightbour(prevNode.gameObject);
            prevScarab.RemoveNeightbour(scarab.gameObject);
            prevScarab.ChangeScarab(silverScarab);
            prevScarab.Explode(Color.blue);
        }
        else
        {
            scarabParticlePool = new ObjectPool<GameObject>(createFunc: () => Instantiate(scarabParticlePrefab), 
                actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), 
                actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 20, maxSize: 50);

            scarabParticlePrefab = scarabParticlePool.Get();
            if (scarabParticlePrefab != null)
            {
                scarabParticlePrefab.transform.position = scarab.transform.position;
                scarabParticlePrefab.SetActive(true);
            }
            scarabParticlePrefab.GetComponent<ParticleSystem>().Play();
        }

        currentScarab.ChangeScarab(goldenScarab, Color.yellow);
        WriteConnection(scarab.gameObject);
        DisableAllNodesColliders();

        if (currentScarab.CurrentPossibleNeightbours.Count == 0)
        {
            currentScarab.ChangeScarab(silverScarab);
            WinOrLose();
        }
        else
        {
            foreach (GameObject scarabNode in currentScarab.CurrentPossibleNeightbours)
            {
                scarabNode.GetComponent<Collider>().enabled = true;
            }
        }
        prevNode = scarab;
    }

    private void DisableAllNodesColliders()
    {
        for (int i = 0; i < scarabs.Count; i++)
        {
            Collider scarabCollider = scarabs[i].GetComponent<Collider>();

            if (scarabCollider)
            {
                scarabCollider.enabled = false;
            }
        }
    }

    private void WinOrLose()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            currentScarab = scarabs[i];
            if (currentScarab.CurrentPossibleNeightbours.Count != 0)
            {
                StartCoroutine(Lose());
                return;
            }
        }
        StartCoroutine(Win());
    }
    private IEnumerator Win()
    {
        audioSource.PlayOneShot(gongSound, 0.5f);
        winnerText.enabled = true;
        winnerText.text = "Congratulation! You won!";

        yield return new WaitForSeconds(0.1f);
        scarabParticlePrefab.transform.position = player.transform.position;

        yield return new WaitForSeconds(2f);
        scarabParticlePool.Release(scarabParticlePrefab);
        winnerText.enabled = false;
    }

    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(1f);
        ResetGraph();
    }

    private void WriteConnection(GameObject scarab)
    {
        lineManagerScript.AddPoint(scarab.transform);

        scarabParticlePrefab.transform.position = scarab.transform.position;
    }

    private void ResetGraph()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            currentScarab = scarabs[i];

            if (currentScarab.CurrentPossibleNeightbours.Count != currentScarab.PossibleNeightbours.Count)
            {
                currentScarab.Explode(Color.magenta);
            }
            currentScarab.ChangeScarab(stoneScarab);
            currentScarab.CopyPossibleNeightbourToCurrentNeightbour();
            scarabs[i].gameObject.GetComponent<Collider>().enabled = true;
        }

        lineManagerScript.ResetAllPoints();
        audioSource.PlayOneShot(resetSound, 1f);
        scarabParticlePool.Release(scarabParticlePrefab);
        prevNode = null;
    }
}
