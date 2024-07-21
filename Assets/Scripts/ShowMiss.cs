using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMiss : MonoBehaviour
{
    public TMPro.TextMeshProUGUI missText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.Instance.isPlaying)
        {
            switch (PlayerPrefs.GetInt("CurrentMiss"))
            {
                case 1:
                    missText.text = "X";
                    break;
                case 2:
                    missText.text = "X X";
                    break;
                case 3:
                    missText.text = "X X X";
                    break;
                default:
                    missText.text = "";
                    break;
            }
        }
    }
}
