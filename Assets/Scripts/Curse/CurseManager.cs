using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseManager : MonoBehaviour
{
    private PlayerManager player;
    private Curse curse;
    public bool isTriggered;
    private int maxStacks = 3;
    void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerManager>();
        isTriggered = false;
    }
    public void OnTrigger()
    {
        isTriggered= true;
        curse = new Curse() { stacks = 0, type = "Health" };
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
        player.IncreaseMaxHealth(10 * curse.stacks);
        Destroy(this);
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
}