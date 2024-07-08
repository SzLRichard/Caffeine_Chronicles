using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unfreeze : MonoBehaviour
{
    public void unpause(){
        Time.timeScale = 1f;
    }
}
