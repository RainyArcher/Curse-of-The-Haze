using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Classes and Objects
    [SerializeField] private Camera mainCamera;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private MainManager mainManager;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private GravityForPlayer gravity;

    // Changable variables
    [Range(0f, 30f)]
    [SerializeField] private float walkingSpeed;
    [Range(0f, 50f)]
    [SerializeField] private float runningSpeed;

    [SerializeField] private bool hasDoubleJump;
    [SerializeField] private bool canDoubleJump;


    // Constants
    private const float RotationSmoothTime = 0.1f;
    private const float GroundDistance = 0;
    
    // Private variables
    private Vector3 _direction;

    private float speed;
    private float _rotationVelocity;
    private float _targetAngle = 0f;
    
    public bool isGrounded;
    public bool isSprinting;
    public bool isJumping;



    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.isDialogueActive)
        {
            if (Input.GetButtonDown("Continue") || Input.GetButtonDown("Submit"))
                {
                    dialogueManager.DisplayNextSentence();
                }
            else if (Input.GetButtonDown("Cancel"))
                {
                    dialogueManager.EndDialog();
                }
        }
        else
        {
            Move();
        }
    }
    private void Move()
    {
        _direction = Vector3.zero;
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (_direction != Vector3.zero)
        {
            speed = isSprinting ? runningSpeed : walkingSpeed;
        }
        else
        {
            speed = 0;
        }
        if (_direction.magnitude >= 0.2f)
        {
            _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg +
                                  mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _rotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            Vector3 move = Quaternion.Euler(0.0f, _targetAngle, 0.0f) * Vector3.forward;
            playerController.Move(move.normalized * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            mainManager.ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            dialogueManager.StartDialog(new Dialogue()
            {
                name = "Tester",
                sentences = new string[] { "Hey, I'm an admin",
                "I am testing this application", "Thanks for listening, bye"}
            });
        }
        CheckIsGrounded();
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || canDoubleJump)
            {
                gravity.Jump();
            }
            if (canDoubleJump && !isGrounded)
            {
                canDoubleJump = false;
                StartCoroutine("SetDoubleJump");
            }
        }
    }
    private void CheckIsGrounded()
    {
        isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
    }
    IEnumerator SetDoubleJump()
    {
        yield return new WaitForSeconds(2);
        canDoubleJump = true;
    }

}
