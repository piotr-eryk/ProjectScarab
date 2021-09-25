# Scarab
It is game using 2D puzzles on 3D environment.
## Technologies
* C#
* Unity 2021.1.20f1
## Code example
An example code would be a function called every time the player selects a node in a graph-based puzzle

### Main Function

 ```
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
 ```   
### Win/Lose Function
An example code called by the above code would be whether the player with no moves has lost or won the game.

```
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
 ```   
### Win Coroutine
And the Lose coroutine...

```
    IEnumerator Lose()
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
 ```   
