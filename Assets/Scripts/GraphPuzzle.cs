using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphPuzzle : MonoBehaviour
{
    [HideInInspector] public GameObject[] crossroads;

    [SerializeField] GameObject player;

    [Header("Scarabs Sprite")]
    [SerializeField] private Sprite goldenScarab;
    [SerializeField] private Sprite silverScarab;
    [SerializeField] private Sprite stoneScarab;

    [Header("Graphic effects")]
    [SerializeField] private LineManager lineManagerScript;
    [SerializeField] private Text winnerText;
    [SerializeField] private ParticleSystem scarabTrail;
    [SerializeField] private GameObject scarabTrailContainer;

    [Header("Sound effects")]
    [SerializeField] private AudioClip gongSound;
    [SerializeField] private AudioClip resetSound;
    [SerializeField] private AudioClip clickSound;

    private GameObject prevNode;
    private AudioSource graphSound;
    private ParticleSystem particle;
    private Scarab currentScarab;

    void Awake()
    {
        crossroads = new GameObject[transform.childCount];
        graphSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        winnerText.enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            crossroads[i] = transform.GetChild(i).gameObject;
            crossroads[i].GetComponent<SpriteRenderer>().sprite = stoneScarab;
        }
    }

    private void DisableAllNodesColliders()
    {
        Collider scarabCollider;
        for (int i = 0; i < transform.childCount; i++)
        {
            scarabCollider = crossroads[i].GetComponentInChildren<Collider>();

            if (scarabCollider.enabled == true)
                scarabCollider.enabled = false;
        }
    }

    public void ScarabIsChosen(GameObject scarab)
    {
        graphSound.PlayOneShot(clickSound, 0.2f);

        currentScarab = scarab.GetComponent<Scarab>();

        if (prevNode) // If prevNode exist so if player chose second node
        {
            Scarab prevScarab = prevNode.GetComponent<Scarab>();
            currentScarab.RemoveNeightbour(prevNode);
            prevScarab.RemoveNeightbour(scarab);
            prevScarab.ChangeScarab(silverScarab);
            prevScarab.Explode(Color.blue);
        }
        else //do this only with first chosen scarab
        {
            particle = Instantiate(scarabTrailContainer, scarab.transform.position, Quaternion.identity).GetComponentInChildren<ParticleSystem>();
            particle.Play();
        }

        currentScarab.ChangeScarab(goldenScarab, Color.yellow);
        WriteConnection(scarab);
        DisableAllNodesColliders();

        if (currentScarab.currentPossibleNeightbours.Count == 0)
        {
            currentScarab.ChangeScarab(silverScarab);
            WinOrLose();
        }
        else
        {
            foreach (GameObject scarabNode in currentScarab.currentPossibleNeightbours)
            {
                scarabNode.GetComponent<Collider>().enabled = true;
            }
        }
        prevNode = scarab;
    }

    private void WinOrLose()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            currentScarab = crossroads[i].GetComponent<Scarab>();
            if (currentScarab.currentPossibleNeightbours.Count != 0)
            {
                StartCoroutine(Lose());
                return;
            }
        }
        StartCoroutine(Win());
    }
    IEnumerator Win()
    {
        graphSound.PlayOneShot(gongSound, 0.5f);
        winnerText.enabled = true;
        winnerText.text = "Congratulation! You won!";

        yield return new WaitForSeconds(0.1f);
        particle.transform.position = player.transform.position;

        yield return new WaitForSeconds(2f);
        Destroy(particle.transform.root.gameObject);
        winnerText.enabled = false;
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(1f);
        ResetGraph();
    }

    void WriteConnection(GameObject scarab)
    {
        lineManagerScript.AddPoint(scarab.transform);
        
        particle.transform.position = scarab.transform.position;
    }

    private void ResetGraph()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            currentScarab = crossroads[i].GetComponent<Scarab>();

            if (currentScarab.currentPossibleNeightbours.Count != currentScarab.possibleNeightbours.Count)
            {
                currentScarab.Explode(Color.magenta);
            }
            currentScarab.ChangeScarab(stoneScarab);
            currentScarab.CopyPossibleNeightbourToCurrentNeightbour();
            crossroads[i].GetComponent<Collider>().enabled = true;

        }
        lineManagerScript.ResetAllPoints();
        graphSound.PlayOneShot(resetSound, 1f);
        Destroy(particle.transform.root.gameObject);
        prevNode = null;
    }
}
