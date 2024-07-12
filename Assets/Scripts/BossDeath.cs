using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public GameObject winScreen;
    bool first = true;
    private void Update()
    {
        if ( GameObject.Find("Boss")==null&& first) {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
            first = false;
        }
    }
}
