using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    public SkillSlot[]skillSlots;
    public Text pointsText;
    public int availablePoints;//可用点数
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateAbilityPoints;
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateAbilityPoints;

    }

    private void HandleSkillMaxed(SkillSlot skillslot)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            //还没有解锁并且可以解锁
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlocked();
            }
        }
    }

    private void HandleAbilityPointSpent(SkillSlot skillslot)
    {
        if (availablePoints > 0)
        {
            UpdateAbilityPoints(-1);
        }
    }

    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(()=>CheckAvailablePoints(slot));
        }
        UpdateAbilityPoints(0);
    }
    public void UpdateAbilityPoints(int amount)
    {
        availablePoints += amount;
        pointsText.text = "Points:" + availablePoints;
    }
    private void CheckAvailablePoints(SkillSlot slot)
    {
        if(availablePoints > 0)
        {
            slot.TryUpgradeSkill(); 
        }
    }


}
