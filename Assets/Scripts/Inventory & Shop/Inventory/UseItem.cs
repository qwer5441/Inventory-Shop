using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消耗物品使用
/// </summary>
public class UseItem : MonoBehaviour
{
    public void ApplyItemEffects(ItemSO itemSO)
    {
        if (itemSO.currentHealth > 0)
            PlayerManager.instance.UpdateHealth(itemSO.currentHealth);

        if(itemSO.MaxHealth > 0)
            PlayerManager.instance.UpdateMaxHealth(itemSO.MaxHealth);

        if (itemSO.speed > 0)
            PlayerManager.instance.UpdateSpeed(itemSO.speed);

        if(itemSO.damage>0)
            PlayerManager.instance.Updatedamage(itemSO.damage);

        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));

        
    }
    /// <summary>
    /// 效果延迟恢复原状态
    /// </summary>
    /// <param name="itemSO"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (itemSO.currentHealth > 0)
            PlayerManager.instance.UpdateHealth(-itemSO.currentHealth);

        if (itemSO.MaxHealth > 0)
            PlayerManager.instance.UpdateMaxHealth(-itemSO.MaxHealth);

        if (itemSO.speed > 0)
            PlayerManager.instance.UpdateSpeed(-itemSO.speed);

        if (itemSO.damage > 0)
            PlayerManager.instance.Updatedamage(-itemSO.damage);
    }
}
