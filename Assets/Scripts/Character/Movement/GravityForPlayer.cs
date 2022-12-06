using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForPlayer : MonoBehaviour
{
    // Classes and Objects
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private CharacterController playerController;

    // Changable variables
    [Range(0, 100)]
    [SerializeField] private int gravityMultiplier;
    [Range(0.0f, 40.0f)]
    [SerializeField] private float JumpHeight;

    // Constants
    private const float Gravity = 9.8f;

    // variables
    private Vector3 _velocity;

    void Update()
    {
        _velocity.y -= Gravity * Time.deltaTime * gravityMultiplier;
        playerController.Move(_velocity * Time.deltaTime);
        if (movement.isGrounded && _velocity.y < 0)
            _velocity.y = -2f;
    }
    public void Jump()
    {
        _velocity.y += Mathf.Sqrt(JumpHeight * Gravity * 2);
    }
}
