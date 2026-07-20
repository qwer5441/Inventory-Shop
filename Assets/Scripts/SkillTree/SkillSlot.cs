using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public List<SkillSlot> prerequisiteSkillSlots;
    public SkillSO skillSO;

    public int currentlevel;
    public bool isUnlocked;
    public Image skillIcon;
    public Button skillButton;
    public Text skillLevelText;
    public static event Action<SkillSlot> OnAbilityPointSpent; //技能花费点数时
    public static event Action<SkillSlot> OnSkillMaxed;//技能点满时

    private void OnValidate()
    {
        if(skillSO != null&&skillLevelText!=null)
        {
            UpdateUI();
        }
    }
    //尝试升级技能
    public void TryUpgradeSkill()
    {
        // 只有 解锁 + 没满级 才能升级
        if (isUnlocked && currentlevel < skillSO.maxLevel)
        {
            currentlevel++;
            OnAbilityPointSpent?.Invoke(this);// 发事件：技能点-1
            if (currentlevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);//发事件：技能满级了
            }
            UpdateUI();
        }
    }
    // 更新UI：图标、文字、按钮状态、颜色
    private void UpdateUI()
    {
       skillIcon.sprite = skillSO.skillIcon;

        if (isUnlocked)
        {
            skillButton.interactable= true;
            skillLevelText.text = currentlevel.ToString()+"/"+skillSO.maxLevel.ToString();
            skillIcon.color=Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.gray;
        }
    }
    public bool CanUnlockSkill()
    {
        foreach(SkillSlot slot in prerequisiteSkillSlots)
        {
            // 没解锁 or 等级没满
            if (!slot.isUnlocked || slot.currentlevel < slot.skillSO.maxLevel)
            {
                return false;
            } 
        }
        return true;
    }
    //解锁这个技能
    public void Unlocked()
    {
        isUnlocked = true;
        UpdateUI();
    }


}
