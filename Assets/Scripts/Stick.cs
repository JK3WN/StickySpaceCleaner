using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DebrisFree"))
        {
            collision.transform.SetParent(this.transform);
            collision.gameObject.tag = new string("DebrisStuck");
            collision.gameObject.AddComponent<Stick>();
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            PlayerPrefs.SetInt("CurrentScore", PlayerPrefs.GetInt("CurrentScore") + 1);
        }
    }
}
