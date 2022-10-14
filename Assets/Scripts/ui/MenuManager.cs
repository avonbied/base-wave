using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// TODO - implement menu
public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartMenu()
    {
        Global.GameOver = false;
        Global.ProjectilePool = null;
        Global.Controller = null;
        Global.UnitSpawner = null;
        SceneManager.LoadScene("StartScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadOverlay(string overlayName)
    {

    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
