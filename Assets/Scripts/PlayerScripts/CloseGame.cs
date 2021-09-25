using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    [SerializeField] private bool closeInspectorWithESC;
    public void ExitGame()
    {
        if (closeInspectorWithESC)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        Application.Quit();
    }
}
