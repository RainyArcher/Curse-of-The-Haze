using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private CharacterMovement player;
    private void Start()
    {
        player = GetComponentInParent<CharacterMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grass"))
            player.groundMaterial = "grass";
        else if (other.CompareTag("Wood"))
            player.groundMaterial = "wood";
    }
}
