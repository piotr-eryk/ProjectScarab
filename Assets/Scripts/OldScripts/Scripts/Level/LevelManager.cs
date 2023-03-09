using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour
{
    private double totalTime;

    public double TotalTime
    {
        get 
        { 
            return totalTime; 
        }
        set
        {
            totalTime = value;
        }
    }

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadLevel();
    }

    private int level;
    public int LastLevel;
    private const int MAXIMUM_LEVEL = 6;
    public void LoadFirstLevel()
    {
        level = 1;
        SceneManager.LoadScene("Level" + level);
    }

    public void LoadNextLevel()
    {
        level++;
        if (level <= MAXIMUM_LEVEL)
        {
            SceneManager.LoadScene("Level" + level);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
    [System.Serializable]
    class SaveData
    {
        public int LastLevel;
    }

    public void SaveLevel()
    {
        SaveData data = new SaveData();
        data.LastLevel = LastLevel;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadLevel()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            LastLevel = data.LastLevel;
        }
    }
}
