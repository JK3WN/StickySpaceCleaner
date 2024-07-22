using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    public float startMoveSpeed = 5f;
    public float rotateSpeed = 10f;

    public Rigidbody2D rb;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.isPlaying)
        {
            moveSpeed = startMoveSpeed * cam.orthographicSize / 5f;
            rb.velocity = new Vector2 (Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
            rotateSpeed = 10000f * rb.inertia;

            if (Input.GetMouseButton(0))
            {
                rb.AddTorque(rotateSpeed * Time.deltaTime, ForceMode2D.Force);
            }

            if (Input.GetMouseButton(1))
            {
                rb.AddTorque(-rotateSpeed * Time.deltaTime, ForceMode2D.Force);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
