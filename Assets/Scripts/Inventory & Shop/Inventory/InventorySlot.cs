using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 物品槽
/// </summary>
public class InventorySlot : MonoBehaviour,IPointerClickHandler
{
    public ItemSO itemSO; //关联的物品数据
    public int quantity;//存放数量

    public Image itemImage;//图像UI
    public TMP_Text quantityText; 

    private InventoryManager inventoryManager; //库存管理器

    private static ShopManager activeShop;
    private void Start()
    {
        inventoryManager=GetComponentInParent<InventoryManager>();
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopStateChanged += HandleShopStateChanged;
    }
    private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChanged;
    }

    private void HandleShopStateChanged (ShopManager shopManager,bool isOpen)
    {
        activeShop=isOpen?shopManager:null;
    }

    
    /// <summary>
    /// 点击物品使用
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //如果是打开商店就出售
                if (activeShop != null)
                {
                    activeShop.SellItem(itemSO);
                    quantity--;
                    UpdateUI();
                }
                //商店没打开就进行使用
                else
                {                   
                    //是回血物品并且血量满了 不使用
                    if (itemSO.currentHealth > 0 && PlayerManager.instance.currentHealth >= PlayerManager.instance.MaxHealth) return;
                    inventoryManager.UseItem(this);
                }


            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }
    }


    /// <summary>
    /// 更新物品槽的数据显示
    /// </summary>
    public void UpdateUI()
    {
        if (quantity <= 0)
            itemSO = null;

        
        if(itemSO != null)
        {
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text=quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
}
