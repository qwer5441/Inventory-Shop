using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 库存管理器
/// </summary>
public class InventoryManager : MonoBehaviour
{
    [Header("3个物品槽")]
    public InventorySlot[] itemSlots;
    [Header("物品消耗后的效果处理")]
    public UseItem useItem;

    [Header("参数")]
    public int gol; //金币
    public TMP_Text goldText;
    public GameObject lootPrefab;
    public Transform player;

    private void Start()
    {
        goldText.text=gol.ToString();
        foreach (var slot in itemSlots) 
        {
            slot.UpdateUI();
        }
    }
    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }
    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="itemSO"></param>
    /// <param name="quantity"></param>
    public void AddItem(ItemSO itemSO, int quantity)
    {
        //是金币的处理
        if (itemSO.isGold)
        {
            gol += quantity;
            goldText.text=gol.ToString();
            return;
        }
        //物品正式添加处理
        //一：先找"同类物品"的格子放
        foreach (var slot in itemSlots)
        {
            //找到 和要添加的物品一样
            //      还没叠满 的格子
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
            {
                //计算剩余容量
                //算出这个格子还能装多少
                //剩余空间 = 最大堆叠数 - 当前数量
                int availabeSpace =itemSO.stackSize-slot.quantity;

                //取 能装的数量 和 要加的数量 里更小的那个
                int amountToAdd = Mathf.Min(availabeSpace, quantity);

                //格子数量增加
                slot.quantity += amountToAdd;

                //待添加的数量减少
                quantity -= amountToAdd;

                slot.UpdateUI();

                //如果能在同类格子中全部放完，就结束
                if (quantity <= 0) return;
            }
        }
        //二：找空格子放
        foreach (var slot in itemSlots)
        {
           if (slot.itemSO == null)
           {
              // 取 物品最大堆叠数 和 剩余要放的数量里 更小的那个
              int amountToAdd =Mathf.Min(itemSO.stackSize, quantity);

              // 把空格子 设置成 要添加的物品
              slot.itemSO = itemSO;
              slot.quantity = amountToAdd;
              quantity -= amountToAdd;
              slot.UpdateUI();
              return;
           }
        }
        
        //背包完全满了，剩下的物品丢在地上
        if (quantity > 0)
        {
            DropLoot(itemSO, quantity);
        }
    }

    /// <summary>
    /// 丢弃物品时实例化一个在原地
    /// </summary>
    /// <param name="itemSO"></param>
    /// <param name="quantity"></param>
    private void DropLoot(ItemSO itemSO, int quantity)
    {
        Loot loot= Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO,quantity);
    } 

    /// <summary>
    /// 使用物品
    /// </summary>
    /// <param name="inventorySlot"></param>
    /// <exception cref="NotImplementedException"></exception>
    internal void UseItem(InventorySlot slot)
    {
        if (slot.itemSO != null&&slot.quantity>0)
        {
            useItem.ApplyItemEffects(slot.itemSO);  

            slot.quantity--;
            if(slot.quantity <= 0)
            {
                slot.itemSO = null;
            }
            slot.UpdateUI();
        }
    }

    /// <summary>
    /// 丢弃物品时物品槽变化
    /// </summary>
    /// <param name="slot"></param>
    internal void DropItem(InventorySlot slot)
    {
        DropLoot(slot.itemSO, 1);
        slot.quantity--;
        if(slot.quantity <= 0)
        {
            slot.itemSO= null;
        }
        slot.UpdateUI();
    }
}
