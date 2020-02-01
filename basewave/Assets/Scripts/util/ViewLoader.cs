using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ViewLoader : SceneLoader {
    public void StartScene() {
        SceneManagement.LoadScene("StartScene");
    }
} 