using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuSceneController : MonoBehaviour
{
    public void OnPlay ()
    {
        LevelManager.Instance.LoadFirstLevel();

        SceneManager.LoadScene("level" + LevelManager.Instance.LastLevel);
    }

    public void OnExit()
    {
        LevelManager.Instance.SaveLevel();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
