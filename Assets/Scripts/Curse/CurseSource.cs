using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseSource : MonoBehaviour
{
    private CurseManager manager;
    [SerializeField] private bool isColliding;
    [SerializeField] private float timer;
    private const float timerTime = 3f;

    private void Awake()
    {
        isColliding = false;
        timer = timerTime;
        manager = this.GetComponentInParent<CurseManager>();
    }
    private void Update()
    {
        if (isColliding)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = timerTime;
                if (manager.isTriggered)
                {
                    manager.AddStack();
                }
            }

        }
        else
        {
            timer = timerTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isColliding = true;
            if (!manager.isTriggered)
            {
                manager.OnTrigger();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isColliding = false;
        }
    }
}
