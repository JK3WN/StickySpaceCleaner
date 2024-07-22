using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHighScore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = PlayerPrefs.GetInt("1st").ToString() + "\n" + PlayerPrefs.GetInt("2nd").ToString() + "\n" + PlayerPrefs.GetInt("3rd").ToString() + "\n" + PlayerPrefs.GetInt("4th").ToString() + "\n" + PlayerPrefs.GetInt("5th").ToString();
    }
}
