using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startButton() {

        SceneManager.LoadScene(1);
    
    }

    public void controlsButton() {

        SceneManager.LoadScene(3);
    
    }

    public void creditsButton() {

        SceneManager.LoadScene(2);
    
    }
    public void quitButton() {

        Application.Quit();
    
    }

    public void BackButton() {

        SceneManager.LoadScene(0);
    
    }
}
