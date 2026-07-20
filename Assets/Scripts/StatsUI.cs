using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    private bool isOpen;
    private void Start()
    {
        UpdateAllStats();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isOpen)
            {
                UpdateAllStats();

                Time.timeScale = 1;
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                isOpen = false;

            }
            else
            {
                UpdateAllStats();

                Time.timeScale = 0;
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;

                isOpen = true;

            }
        }
    }
    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<Text>().text = "攻击力:" + PlayerManager.instance.atk;
    }
    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<Text>().text = "速度:" + PlayerManager.instance.speed;

    }
    public void UpdateMaxHealth()
    {
        statsSlots[2].GetComponentInChildren<Text>().text = "最大血量:" + PlayerManager.instance.MaxHealth;

    }
    public void UpdatecurrentHealth()
    {
        statsSlots[3].GetComponentInChildren<Text>().text = "当前血量:" + PlayerManager.instance.currentHealth;

    }
    public void UpdateattackRange()
    {
        statsSlots[4].GetComponentInChildren<Text>().text = "攻击范围:" + PlayerManager.instance.attackRange;
    }
    public void UpdateknockBackForce()
    {
        statsSlots[5].GetComponentInChildren<Text>().text = "击退力量:" + PlayerManager.instance.knockBackForce;

    }
    public void UpdateknockBackTime()
    {
        statsSlots[6].GetComponentInChildren<Text>().text = "击退时间:" + PlayerManager.instance.knockBackTime;

    }
    public void UpdatestunTime()
    {
        statsSlots[7].GetComponentInChildren<Text>().text = "眩晕时间:" + PlayerManager.instance.stunTime;

    }


    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
        UpdateMaxHealth();
        UpdatecurrentHealth();
        UpdateattackRange();
        UpdateknockBackForce();
        UpdateknockBackTime();
        UpdatestunTime();
    }
}
