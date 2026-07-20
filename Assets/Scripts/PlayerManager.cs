using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public StatsUI statsUI;
    public TMP_Text healthText;

    public int atk;
    public float attackRange;
    public float knockBackForce;
    public float knockBackTime;
    public float stunTime;
    public float speed;
    public int MaxHealth;
    public int currentHealth;
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateMaxHealth(int amount)
    {
        MaxHealth += amount;
        healthText.text = "HP:" + currentHealth + "/" + MaxHealth;
    }
    public void UpdateHealth(int amount)
    {
        currentHealth+= amount;
        if (currentHealth >= MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        healthText.text="HP:"+currentHealth+"/"+MaxHealth;
    }
    public void UpdateSpeed(int amount)
    {
        speed += amount;
        //statsUI.UpdateAllStats();
    }
    public void Updatedamage(int amount)
    {
        atk+= amount;
    }
}
