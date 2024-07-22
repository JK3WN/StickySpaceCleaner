using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    public TMPro.TextMeshProUGUI HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreText.text = PlayerPrefs.GetInt("1st").ToString() + "\n\n" + PlayerPrefs.GetInt("2nd").ToString() + "\n\n" + PlayerPrefs.GetInt("3rd").ToString() + "\n\n" + PlayerPrefs.GetInt("4th").ToString() + "\n\n" + PlayerPrefs.GetInt("5th").ToString();
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
