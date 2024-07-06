using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PreviousScene : MonoBehaviour
{
    int sceneIndex;
    int sceneToOpen;
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (!PlayerPrefs.HasKey("previousScene"))
            PlayerPrefs.SetInt("previousScene", sceneIndex);

        sceneToOpen = PlayerPrefs.GetInt("previousScene");
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneToOpen);
    }
}
