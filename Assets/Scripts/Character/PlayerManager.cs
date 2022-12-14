using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float health;
    // maxHealth is a health value that is the highest right now, that can be increased or decreased
    [SerializeField] private float maxHealth;
    // a maximum health value that player can have at the moment by upgrading it is called a healthLimit
    // a healthLimit variable can only be Increased
    [SerializeField] float healthLimit;
    [SerializeField] private TextMeshProUGUI leftEffectText;
    [SerializeField] private TextMeshProUGUI rightEffectText;
    private MainManager manager;
    private Vector3 position;
    public float Health {get => health;}
    public float MaxHealth { get => maxHealth;}
    public float HealthLimit { get => healthLimit;}
    public bool isDead;

    public Vector3 Position { get => position; }
    public List<Curse> curseList;
    void Start()
    {
        isDead= false;
        healthLimit = 20f;
        maxHealth = healthLimit;
        health = healthLimit;
        curseList = new List<Curse>();
        manager = GameObject.FindObjectOfType<MainManager>();
    }
    private void Update()
    {
        DisplayEffects();
        if (health < 0)
        {
            isDead = true;
            manager.OnDeath();
        }
    }

    public void DealDamage(float damage, string type = "Points")
    {
        if (!isDead)
        {
            switch (type)
            {
                case "Points":
                    { health -= damage; break; }
                case "Percent":
                    { health -= damage * health; break; }
            }
        }
    }
    public void DealDamage(int damage, string type = "Points")
    {
        if (!isDead)
        {
            switch (type)
            {
                case "Points":
                    { health -= (float)damage; break; }
                case "Percent":
                    { health -= (float)damage / 100 * health; break; }
            }
        }
    }
    public void ReduceMaxHealth(int percent)
    {
        if (!isDead)
        {
            maxHealth -= ((float)percent / 100) * healthLimit;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }
    public void IncreaseMaxHealth(int percent) 
    {
        float previousMaxHealth = maxHealth;
        maxHealth += ((float)percent / 100) * healthLimit;
        health += (maxHealth - previousMaxHealth);
    }
    private void DisplayEffects()
    {
        if (curseList.Count == 1)
        {
            ClearEffects();
            DisplayLeftEffect();
        }
        else if (curseList.Count == 2)
        {
            DisplayLeftEffect();
            DisplayRightEffect();
        }
        else if (curseList.Count == 0)
        {
            ClearEffects();
        }
    }
    private void DisplayLeftEffect()
    {
        if (curseList[0].stacks != 3)
            leftEffectText.text = new string('I', curseList[0].stacks + 1);
        else
            leftEffectText.text = "IV";
    }
    private void DisplayRightEffect()
    {
        if (curseList[1].stacks != 3)
            rightEffectText.text = new string('I', curseList[1].stacks + 1);
        else
            rightEffectText.text = "IV";
    }
    private void ClearEffects()
    {
        leftEffectText.text = "";
        rightEffectText.text = "";
    }
}
