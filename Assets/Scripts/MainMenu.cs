using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";

    // Start is called before the first frame update
    public void Play ()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit ()
    {
        Debug.Log("Turning Off...");
        Application.Quit();
    }
}
