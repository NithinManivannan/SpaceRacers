using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedCar : MonoBehaviour
{
    public float moveSpeed = 20f;        // Base movement speed
    public float boostSpeed = 40f;       // Speed when boosted
    public float rotationSpeed = 100f;   // Rotation speed
    private float currentSpeed;          // Current speed
    private bool canMove = true; // Flag to control whether the car can move


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed;
        Debug.Log("Current Speed: " + currentSpeed);
        Debug.Log("Move Speed: " + moveSpeed);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FlipToCorrectSide();
        }
    }

    private void FlipToCorrectSide()
    {
        // Check the current orientation of the car
        float angle = Vector3.Angle(Vector3.up, transform.up);

        // If the car is upside down, flip it to the correct side
        if (angle >= 90f)
        {
            // Calculate the rotation needed to align with the correct side
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;

            // Smoothly rotate the car to the correct side
            StartCoroutine(RotateCar(targetRotation, 1f));
        }
    }

    private IEnumerator RotateCar(Quaternion targetRotation, float duration)
    {
        float elapsed = 0f;
        Quaternion initialRotation = transform.rotation;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exact to avoid small discrepancies
        transform.rotation = targetRotation;
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            // Get input for car movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Check if the car is upside down
            if (IsUpsideDown())
            {
                // If upside down, prevent movement
                moveHorizontal = 0f;
                moveVertical = 0f;
            }

            // Calculate movement and rotation
            Vector3 movement = transform.forward * moveVertical * currentSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0, moveHorizontal * rotationSpeed * Time.deltaTime, 0);

            // Apply movement and rotation to the Rigidbody
            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }
    private bool IsUpsideDown()
    {
        // Check the current orientation of the car
        float angle = Vector3.Angle(Vector3.up, transform.up);

        // If the angle is greater than 90 degrees, the car is upside down
        return angle > 90f;
    }
    public void SetSpeedBoost(bool isBoosted)
    {
        currentSpeed = isBoosted ? boostSpeed : moveSpeed;
    }

}