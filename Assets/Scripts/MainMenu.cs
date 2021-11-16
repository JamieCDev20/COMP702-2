using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Object sceneToLoad;
    [SerializeField] private int sceneIndexToLoad;

    public void StartButton()
    {
        if (sceneToLoad != null)
            SceneManager.LoadScene(sceneToLoad.name);
        else
            SceneManager.LoadScene(sceneIndexToLoad);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }



}
