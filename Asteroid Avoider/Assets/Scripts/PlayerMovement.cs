using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float forceMagnitude;
    [SerializeField] float maxVelocity;

    Rigidbody rb;
    Camera mainCamera;

    Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
    }

    void ProcessInput()
    {
        if (!Touchscreen.current.press.isPressed) { return; }

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        movementDirection = transform.position - worldPosition;
        movementDirection.z = 0f;
        movementDirection.Normalize();
    }

    void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewPortPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewPortPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if (viewPortPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }

        if (viewPortPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if (viewPortPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
    }

    void FixedUpdate() 
    {
        if (movementDirection == Vector3.zero) { return; }

        rb.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

    }
}
