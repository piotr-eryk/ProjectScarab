# Scarab
It is game using 2D puzzles on 3D environment.
## Technologies
* C#
* Unity 2021.1.20f1
## Code example
An example code would be a function called every time the player selects a node in a graph-based puzzle

### Main Function

 ```
    public void ScarabIsChosen(Scarab scarab)
    {
        audioSource.PlayOneShot(clickSound, 0.2f);

        currentScarab = scarab;

        if (prevNode)
        {
            Scarab prevScarab = prevNode;
            currentScarab.RemoveNeightbour(prevNode.gameObject);
            prevScarab.RemoveNeightbour(scarab.gameObject);
            prevScarab.ChangeScarab(silverScarabColor);
            prevScarab.Explode(Color.blue);
        }
        else
        {
            scarabParticlePrefab = scarabParticlePool.Get();
            if (scarabParticlePrefab != null)
            {
                scarabParticlePrefab.transform.position = scarab.transform.position;
                scarabParticlePrefab.SetActive(true);
            }
            scarabParticlePrefab.GetComponent<ParticleSystem>().Play();
        }

        currentScarab.ChangeScarab(goldenScarabColor, Color.yellow);
        WriteConnection(scarab.gameObject);
        DisableAllNodesColliders();

        if (currentScarab.CurrentPossibleNeightbours.Count == 0)
        {
            currentScarab.ChangeScarab(silverScarabColor);
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
 ```   
### Win/Lose Function
An example code called by the above code would be whether the player with no moves has lost or won the game.

```
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
 ```   
### Win Coroutine
And the Lose coroutine...

```
    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(1f);
        ResetGraph();
    }
 ```   
### Reset Function
... with Reset Graph function

```
    private void ResetGraph()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            currentScarab = scarabs[i];

            if (currentScarab.CurrentPossibleNeightbours.Count != currentScarab.PossibleNeightbours.Count)
            {
                currentScarab.Explode(Color.magenta);
            }
            currentScarab.ChangeScarab(stoneScarabColor);
            currentScarab.CopyPossibleNeightbourToCurrentNeightbour();
            scarabs[i].gameObject.GetComponent<Collider>().enabled = true;
        }

        lineManagerScript.ResetAllPoints();
        audioSource.PlayOneShot(resetSound, 1f);
        scarabParticlePool.Release(scarabParticlePrefab);
        prevNode = null;
    }
 ```   
