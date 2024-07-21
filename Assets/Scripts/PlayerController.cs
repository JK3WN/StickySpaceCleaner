using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;

    public Rigidbody2D rb;

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
    }
}
