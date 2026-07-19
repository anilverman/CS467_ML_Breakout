using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleScript : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            movement = Vector2.left;
            Debug.Log("Pressing left by " + movement);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            movement = Vector2.right;
            Debug.Log("Pressing right by " + movement);
        }

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}

