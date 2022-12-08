using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Classes and Objects
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip grassWalkingSound;
    [SerializeField] private AudioClip woodWalkingSound;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private GravityForPlayer gravity;
    private MainManager mainManager;
    private QuestManager questManager;

    // Changable variables
    [Range(0f, 30f)]
    [SerializeField] private float walkingSpeed;
    [Range(0f, 50f)]
    [SerializeField] private float runningSpeed;
    [Range(0f, 50f)]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;

    [SerializeField] private bool hasDoubleJump;
    [SerializeField] private bool canDoubleJump;


    // Constants
    private const float RotationSmoothTime = 0.1f;
    private const float GroundDistance = 0.2f;

    // Private variables
    private Vector3 _direction;

    private float speed;
    private float _rotationVelocity;
    private float _targetAngle = 0f;

    // Dash
    private bool dashButtonPressed;
    private KeyCode dashButton;
    private float dashCooldownTime = 2f;
    public float DashCooldownTime { get => dashCooldownTime; }
    private const float maxDashCooldownTime = 2f;
    public float MaxDashCooldownTime {get => maxDashCooldownTime;}

    // RayCasts
    private RaycastHit hit;
    [SerializeField] private Vector3 bottomPosition;
    
    public bool isGrounded;
    public bool isSprinting;
    public string groundMaterial;

    private void Awake()
    {
        questManager = GameObject.FindObjectOfType<QuestManager>();
        mainManager = GameObject.FindObjectOfType<MainManager>();
        transform.position = mainManager.savedPositionData;
    }

    // Update is called once per frame
    void Update()
    {
        // quests
        if (questManager != null)
        {
            Quest quest = questManager.villageQuests[questManager.currentQuestIndex];
            if (quest.id == 0)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    questManager.currentStages++;
                }
            }
            else if (quest.id == 1)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                    questManager.currentStages++;
            }
            else if (quest.id == 2)
            {
                if (GetObjectUnder((int)gravity.JumpHeight) != null)
                    if (GetObjectUnder((int)gravity.JumpHeight).name.Contains("Garden Bed") && Input.GetKeyDown(KeyCode.Space))
                        questManager.currentStages++;
            }
        }
        if (dashCooldownTime < 2f)
        {
            dashCooldownTime += Time.deltaTime;
        }
        else
            dashCooldownTime = 2f;
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
        bottomPosition = _groundChecker.transform.position;
        _direction = Vector3.zero;
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (_direction != Vector3.zero)
        {
            speed = isSprinting ? runningSpeed : walkingSpeed;
            GameObject hitObject = GetObjectUnder(2);
            if (hitObject != null)
            {
                if ((hitObject.tag.ToLower() != groundMaterial) && isGrounded && (new string[] { "wood", "grass" }).Contains(hitObject.tag.ToLower()))
                {
                    audioSource.Stop();
                    groundMaterial = hit.collider.tag.ToLower();
                }
            }
            SetWalkSound();
        }
        else
        {
            speed = 0;
            audioSource.Stop();
        }
        if (dashCooldownTime == 2f) { 
            if (Input.GetKeyDown(KeyCode.S))
            {
                dashButton = KeyCode.S;
                if (dashButtonPressed)
                {
                    StartCoroutine(Dash(dashButton));
                }
                else
                { 
                    dashButtonPressed = true;
                    Invoke("TapDelay", 0.25f);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                dashButton = KeyCode.A;
                if (dashButtonPressed)
                {
                    StartCoroutine(Dash(dashButton));
                }
                else
                {
                    dashButtonPressed = true;
                    Invoke("TapDelay", 0.25f);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                dashButton = KeyCode.D;
                if (dashButtonPressed)
                {
                    StartCoroutine(Dash(dashButton));
                }
                else
                {
                    dashButtonPressed = true;
                    Invoke("TapDelay", 0.25f);
                }
            }
        }
        if (_direction.magnitude >= 0.2f)
        {
            _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg +
                                  mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _rotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            Vector3 move = Quaternion.Euler(0.0f, _targetAngle, 0.0f) * Vector3.forward;
            playerController.Move(move.normalized * Time.deltaTime * speed);
            if (!audioSource.isPlaying && isGrounded)
                audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            mainManager.ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Cursor.lockState = CursorLockMode.Confined;
            dialogueManager.StartDialog(new Dialogue()
            {
                name = "Tester",
                sentences = new string[] { "Hey, I'm an admin",
                "I am testing this application", "Thanks for listening, bye"}
            });
        }
        CheckIsGrounded();
        if (!isGrounded)
        {
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
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
    private void SetWalkSound()
    {
        if (groundMaterial == "wood")
        {
            audioSource.clip = woodWalkingSound;
        }
        else if (groundMaterial == "grass")
        {
            audioSource.clip = grassWalkingSound;
        }
        if (isSprinting)
        {
            audioSource.volume = 0.3f;
        }
        else
        {
            audioSource.volume = 0.05f;
        }
    }
    private void TapDelay()
    {
        dashButtonPressed = false;
        dashButton = KeyCode.None;
    }
    private IEnumerator Dash(KeyCode keyCode)
    {
        float dashStart = Time.time;
        while (Time.time < dashStart + dashTime) 
        {
            Vector3 direction = Vector3.zero;
            switch (keyCode)
            {
                case KeyCode.A: direction = Vector3.left; break;
                case KeyCode.S: direction = Vector3.back; break;
                case KeyCode.D: direction = Vector3.right; break;
            }
            playerController.Move(mainCamera.transform.rotation * direction * dashForce * Time.deltaTime);
            dashCooldownTime = 0f;
            yield return null;
        }
    }
    private GameObject GetObjectUnder(int size)
    {
        if (Physics.Raycast(bottomPosition, Vector3.down, out hit, size))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
