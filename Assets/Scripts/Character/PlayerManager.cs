using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float health;
    private float maxHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    private Vector3 position;
    public float Health {get => health; set { health = value;} }
    public Vector3 Position { get => position; }
    public List<Curse> curseList;
    void Start()
    {
        maxHealth = 20f;
        health = maxHealth;
        curseList = new List<Curse>();
    }
    private void Update()
    {
        healthText.text = $"Health:{health}/{maxHealth}";
    }

    public void DealDamage(float damage, string type = "Points")
    {
        switch (type)
        {
            case "Points":
                { health -= damage; break; }
            case "Percent":
                { health -= damage * health; break; }
        }
    }
    public void DealDamage(int damage, string type = "Points")
    {
        switch (type)
        {
            case "Points":
                { health -= (float)damage; break; }
            case "Percent":
                { health -= (float)damage / 100 * health; break; }
        }
    }
    public void ReduceMaxHealth(int percent)
    {
        maxHealth *= 1 - (float)percent / 100;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void IncreaseMaxHealth(int percent) 
    {
        float previousMaxHealth = maxHealth;
        maxHealth *= 1 + percent / 100;
        health += (maxHealth - previousMaxHealth);
    }
}
