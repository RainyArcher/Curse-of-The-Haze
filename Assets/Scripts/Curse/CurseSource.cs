using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseSource : MonoBehaviour
{
    [SerializeField] private CurseManager manager;

    private void Awake()
    {
        manager = this.GetComponentInParent<CurseManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (manager.isTriggered) 
            {
                manager.AddStack();
            }
            else
            {
                manager.OnTrigger();
            }
        }
    }
}
