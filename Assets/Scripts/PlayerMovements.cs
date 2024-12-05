using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GFX;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetpos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float crouchHeight = 0.5f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    // Reference to the UI Button
    public Button jumpButton;

    private void Start()
    {
        // Add a listener to the jump button
        jumpButton.onClick.AddListener(Jump);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetpos.position, groundDistance, groundLayer);

        // Check if the crouch button is pressed
        if (Input.GetButtonDown("Crouch"))
        {
            Crouch();
        }

        // Check if the crouch button is released
        if (Input.GetButtonUp("Crouch"))
        {
            StandUp();
        }
    }

    private void Jump()
    {
        if (isGrounded && !isJumping)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jumpTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        // If the jump button is held down and jump time is not exceeded, keep applying jump force
        if (isJumping && Input.GetButton("Jump") && jumpTimer < jumpTime)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpTimer += Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }
    }

    private void Crouch()
    {
        if (isGrounded)
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);
        }

        // If the player is jumping while crouching, adjust the scale
        if (isJumping)
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 0.5f, GFX.localScale.z);
        }
    }

    private void StandUp()
    {
        GFX.localScale = new Vector3(GFX.localScale.x, 0.5f, GFX.localScale.z);
    }
}
