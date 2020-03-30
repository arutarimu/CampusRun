using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    public void reset()
    {
        PlayerPrefs.SetInt("reset", 1);
        PlayerPrefs.SetInt("levelReached", 1);
        SceneManager.LoadScene("LevelSelection");
    }

   
}
