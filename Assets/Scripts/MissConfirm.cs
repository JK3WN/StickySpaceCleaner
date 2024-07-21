using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissConfirm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManagement.Instance.isPlaying && collision.gameObject.CompareTag("DebrisFree"))
        {
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("CurrentMiss", PlayerPrefs.GetInt("CurrentMiss") + 1);
        }
    }
}
