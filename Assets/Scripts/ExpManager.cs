using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expTolevel = 10;
    public float expGrowthMultiplier = 1.2f;//下一次所需升级经验的倍数

    public Slider expSlider;
    public Text currenttLevelText;

    public static event Action<int> OnLevelUp;
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GainExperience(2);
            Debug.Log("当前经验" + currentExp);

            Debug.Log("下次经验" + expTolevel);


        }
    }
    private void OnEnable()
    {
        Enemy.OnMonsterDefeated += GainExperience;
    }
    private void OnDisable()
    {
        Enemy.OnMonsterDefeated -= GainExperience;
    }
    /// <summary>
    /// 获得经验
    /// </summary>
    /// <param name="amount"></param>
    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp > expTolevel)
        {
            //经验值到了，升级
            UpdateLevel();
        }
        UpdateUI();

    }
    /// <summary>
    /// 进行升级
    /// </summary>
    public void UpdateLevel()
    {
        level++;
        currentExp -= expTolevel;
        expTolevel = Mathf.RoundToInt(expTolevel * expGrowthMultiplier);
        OnLevelUp?.Invoke(5);
    }
    public void UpdateUI()
    {
        expSlider.maxValue = expTolevel;
        expSlider.value = currentExp;
        currenttLevelText.text = "Level:" + level;
    }
}
