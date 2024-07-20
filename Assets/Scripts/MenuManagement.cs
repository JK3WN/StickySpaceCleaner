using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ControlsPressed()
    {
        
    }

    public void QuitPressed()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }
}
