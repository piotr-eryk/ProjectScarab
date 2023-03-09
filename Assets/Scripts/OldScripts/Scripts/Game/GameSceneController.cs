using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    [Header("Game")]
    //[SerializeField] private Player player;

    [Header("UI")]
    [SerializeField] 
    private Text instructionText;
    [SerializeField] 
    private Text timeText;
    [SerializeField] 
    private Text endedGameText;

    private bool endedLevel;
    private float gameTimer;
    private float endGameTimer = 5f;

    void Start()
    {
        endedGameText.gameObject.SetActive(false);
        //player.OnCollectOrb = OnCollectOrb;
    }

    void Update()
    {
        if (endedLevel == false)
        {
            gameTimer += Time.deltaTime;
            timeText.text = "Time: " + Mathf.FloorToInt(gameTimer) + "s";
        }
        else
        {
            endGameTimer -= Time.deltaTime;
            if (endGameTimer <= 0f)
            {
                LevelManager.Instance.LoadNextLevel();
            }
        }
    }

    private void OnCollectOrb ()
    {
        endedGameText.gameObject.SetActive(true);
        endedLevel = true;
        instructionText.gameObject.SetActive (false);
        timeText.gameObject.SetActive(false);
        endedGameText.text = "You won!\nYour time: " + Mathf.FloorToInt(gameTimer) + "s";

        LevelManager.Instance.TotalTime += Math.Round(gameTimer, 2);
    }
}
