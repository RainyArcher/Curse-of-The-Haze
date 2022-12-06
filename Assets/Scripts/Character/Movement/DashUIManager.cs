using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashUIManager : MonoBehaviour
{
    [SerializeField] private Image dashIconFill;
    private CharacterMovement player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<CharacterMovement>();
    }
    void Update()
    {
        dashIconFill.fillAmount = 1f - (player.DashCooldownTime / player.MaxDashCooldownTime);
    }
}
