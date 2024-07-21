using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText, gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.isPlaying)
        {
            scoreText.text = PlayerPrefs.GetInt("CurrentScore").ToString();
            gameOverText.text = "Game Over!\nYour Score: " + PlayerPrefs.GetInt("CurrentScore").ToString();
        }
    }
}
