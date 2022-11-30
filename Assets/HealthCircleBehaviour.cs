using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCircleBehaviour : MonoBehaviour
{
    [SerializeField] private Image healthCircle;
    [SerializeField] private Image curseCircle;
    [SerializeField] private Image emptyCircle;
    private PlayerManager player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerManager>();
    }
    void Update()
    {
        healthCircle.fillAmount = player.Health / player.HealthLimit;
        curseCircle.fillAmount = 1f - player.MaxHealth / player.HealthLimit;
        emptyCircle.fillAmount = 1f - (player.Health / (player.MaxHealth)) + curseCircle.fillAmount;
    }
}
