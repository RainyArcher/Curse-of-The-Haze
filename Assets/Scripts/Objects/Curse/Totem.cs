using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    private CurseManager manager;

    private void Awake()
    {
        manager = this.GetComponentInParent<CurseManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            manager.Purify();
        }
    }
}
