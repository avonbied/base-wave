using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// TODO - implement menu
public class MenuManager : MonoBehaviour {
    // Start is called before the first frame update
    public void LoadGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadCredits() {
        SceneManager.LoadScene("CreditsScene");
    }
    public void QuitGame() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update() {

    }
}
