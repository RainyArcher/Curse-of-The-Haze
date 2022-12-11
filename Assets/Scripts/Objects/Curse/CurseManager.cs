using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurseManager : MonoBehaviour
{
    private PlayerManager player;
    private Transform playerController;
    [SerializeField] private GameObject pointer;
    [SerializeField] private Transform totem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip teleportClip;
    [SerializeField] private AudioClip appleEatingClip;
    [SerializeField] private AudioClip curseStackClip;
    private Curse curse;
    public bool isTriggered;
    private int maxStacks = 3;
    private float timerForTeleportation;
    private float timerForTeleportationTime = 3.5f;
    private const int radiusOfTeleportation = 30;
    private const int radiusOfTriggering = 75;
    private const int radiusOfTriggeringEsape = 20;
    void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerManager>();
        playerController = GameObject.Find("Player").GetComponent<Transform>();

        isTriggered = false;

    }
    private void Update()
    {
        if (timerForTeleportation > 0)
        {
            timerForTeleportation -= Time.deltaTime;
        }
        if (isTriggered && (playerController.position - transform.position).magnitude < (float)radiusOfTriggeringEsape && timerForTeleportation <= 0)
        {
            timerForTeleportation = timerForTeleportationTime;
            Teleport();
        }
    }
    public void OnTrigger()
    {
        audioSource.PlayOneShot(curseStackClip);
        pointer.SetActive(true);
        totem.gameObject.SetActive(true);
        timerForTeleportation = timerForTeleportationTime;
        isTriggered = true;
        TeleportTotem();
        curse = new Curse() { stacks = 0, type = "Health" };
        if (player.curseList.Count == 2)
        {
            player.curseList.RemoveAt(0);
        }
        player.curseList.Add(curse);
        DealCurse();
    }
    public void AddStack()
    {
        if (curse.stacks < maxStacks)
        {
            audioSource.PlayOneShot(curseStackClip);
            curse.stacks++;
            DealCurse();
        }
    }
    public void Purify()
    {
        audioSource.PlayOneShot(appleEatingClip);
        player.IncreaseMaxHealth(10 * (curse.stacks + 1));
        player.curseList.Remove(curse);
        Invoke("Destroy", 0.9f);
    }
    void OnSourceDestroy()
    {
        // Boss's shield interaction
        // Changing texture
        Debug.Log("Source is destroyed");
    }

    void DealCurse()
    {
        if (curse.type == "Health")
        {
            player.ReduceMaxHealth(10);
        }
    }
    void Teleport()
    {
        audioSource.PlayOneShot(teleportClip);
        Vector3 teleportVector = (transform.position - playerController.position).normalized;
        teleportVector.y = 0f;
        transform.Translate(teleportVector * (float)Random.Range(20, radiusOfTeleportation * 2));
    }
    void TeleportTotem()
    {
        Vector3 teleportVector = (transform.position - playerController.position).normalized;
        teleportVector.y = 0f;
        totem.transform.Translate(teleportVector * (float)Random.Range(30, radiusOfTriggering));
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}