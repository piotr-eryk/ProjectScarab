using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private GameObject player;

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);

        //if (collision.gameObject.GetComponent<Player>())
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }
}
