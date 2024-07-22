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
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetString("HighScore", "0\n\n0\n\n0\n\n0\n\n0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreText.text = PlayerPrefs.GetString("HighScore");
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
