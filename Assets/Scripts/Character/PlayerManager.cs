using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float health;
    // maxHealth is a health value that is the highest right now, that can be increased or decreased
    [SerializeField] private float maxHealth;
    // a maximum health value that player can have at the moment by upgrading it is called a healthLimit
    // a healthLimit variable can only be Increased
    [SerializeField] float healthLimit;
    [SerializeField] private MainManager manager;
    private Vector3 position;
    public float Health {get => health;}
    public float MaxHealth { get => maxHealth;}
    public float HealthLimit { get => healthLimit;}

    public Vector3 Position { get => position; }
    public List<Curse> curseList;
    void Start()
    {
        healthLimit = 20f;
        maxHealth = healthLimit;
        health = healthLimit;
        curseList = new List<Curse>();
    }
    private void Update()
    {
        if (health < 0)
        {
            Debug.Log("You died");
            manager.ReloadScene();
        }
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
        maxHealth -= ((float)percent / 100) * healthLimit;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void IncreaseMaxHealth(int percent) 
    {
        float previousMaxHealth = maxHealth;
        maxHealth += ((float)percent / 100) * healthLimit;
        health += (maxHealth - previousMaxHealth);
    }
}
