using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseManager : MonoBehaviour
{
    private PlayerManager player;
    private Transform playerController;
    [SerializeField] private Transform totem;
    private Curse curse;
    public bool isTriggered;
    private int maxStacks = 3;
    private float timerForTeleportation;
    private const float radiusOfTeleportation = 30;
    private const float radiusOfTriggering = 26;
    private const float radiusOfTriggeringEsape = 14;
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
            timerForTeleportation = 5f;
            Teleport();
        }
    }
    public void OnTrigger()
    {
        timerForTeleportation = 5f;
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
            curse.stacks++;
            DealCurse();
        }
    }
    public void Purify()
    {
        player.IncreaseMaxHealth(10 * (curse.stacks + 1));
        player.curseList.Remove(curse);
        Destroy(gameObject);
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
        Vector3 teleportVector = (transform.position - playerController.position).normalized;
        teleportVector.y = 0f;
        transform.Translate(teleportVector * (float)Random.Range(4, radiusOfTeleportation));
    }
    void TeleportTotem()
    {
        Vector3 teleportVector = (transform.position - playerController.position).normalized;
        teleportVector.y = 0f;
        totem.transform.Translate(teleportVector * (float)Random.Range(3, radiusOfTriggering));
    }
}