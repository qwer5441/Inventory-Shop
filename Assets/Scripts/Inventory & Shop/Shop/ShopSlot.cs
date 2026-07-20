    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// 商店物品槽
    /// </summary>
    public class ShopSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
    {
        public ItemSO itemSO;
        public TMP_Text itemNameText;
        public TMP_Text priceText;
        public Image itemImage;

        [SerializeField]
        private ShopManager shopManager;

        [SerializeField]
        private ShopInfo shopInfo;
        public int price;

        public void Initialize(ItemSO newItemSO,int price)
        {
            itemSO=newItemSO;
            itemImage.sprite=itemSO.icon;
            itemNameText.text = itemSO.itemName;

            this.price = price;
            priceText.text=price.ToString();
        }

    //---------------鼠标点击事件进行购买处理---------------------
        public void OnBuyButtonClicked()
        {
            shopManager.TryBuyItem(itemSO, price);
        }


     //------------------鼠标悬停时，商品详细信息的处理-------------------
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(itemSO!=null) 
            shopInfo.ShowItemInfo(itemSO);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            shopInfo.HideItemInfo();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (itemSO != null)
                shopInfo.FollowMouse();

        }
    }
