using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScript : MonoBehaviour
{
    private void Awake() {
        Time.timeScale = 1;
    }

    public void loadNextScene (int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

}
