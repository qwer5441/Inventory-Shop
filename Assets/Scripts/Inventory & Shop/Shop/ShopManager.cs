using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private ShopSlot[]shopSlots; //所有商品格子

    [SerializeField]
    private InventoryManager inventoryManager; //库存管理器

    //单个商品
    [System.Serializable]
    public class ShopItems 
    {
        public ItemSO itemSO;
        public int price;
    }

    /// <summary>
    /// 加载商品
    /// </summary>
    /// <param name="shopItems"></param>
    public void PopulateShopItems(List<ShopItems> shopItems)
    {
        //填充格子里面的商品数据
        for(int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemSO, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }

        //隐藏没有商品数据的格子
        for(int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 购买物品
    /// </summary>
    /// <param name="itemSO"></param>
    /// <param name="price"></param>
    public void TryBuyItem(ItemSO itemSO, int price)
    {
        if (itemSO != null && inventoryManager.gol >= price)
        {
            if (HasSpaceForItem(itemSO))
            {
                inventoryManager.gol -= price;
                inventoryManager.goldText.text=inventoryManager.gol.ToString();
                inventoryManager.AddItem(itemSO, 1);
            }
        }
    }

    private bool HasSpaceForItem(ItemSO itemSO)
    {
        foreach (var slot in inventoryManager.itemSlots)
        {
            //先检查同类型可堆叠
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
            {
                return true;
            }
            //再检查空位
            else if (slot.itemSO == null)
            {
                return true;
            }
        }
        return false;

    }
    /// <summary>
    /// 出售商品
    /// </summary>
    /// <param name="itemSO"></param>
    public void SellItem(ItemSO itemSO)
    {
        if (itemSO == null)
            return;
        foreach (var slot in shopSlots)
        {
            if (slot.itemSO == itemSO)
            {
                inventoryManager.gol += slot.price;
                inventoryManager.goldText.text = inventoryManager.gol.ToString();
                return;
            }
        }
    }



}
