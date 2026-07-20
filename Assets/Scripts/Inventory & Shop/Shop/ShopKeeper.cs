using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShopManager;

public class ShopKeeper : MonoBehaviour
{
    public Animator anim;
    private bool playerInRange;
    private bool isShopOpen;
    public CanvasGroup shopCanvasGrop;
    public ShopManager shopManager;

    [SerializeField]
    private List<ShopItems>  shopItems;

    [SerializeField]
    private List<ShopItems> shopWeapons;

    [SerializeField]
    private List<ShopItems> shopArmour;

    public static event Action<ShopManager, bool> OnShopStateChanged;
    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!isShopOpen)
                {
                    Time.timeScale = 0;
                    isShopOpen = true;
                    OnShopStateChanged?.Invoke(shopManager,true);

                    shopCanvasGrop.alpha = 1;
                    shopCanvasGrop.blocksRaycasts = true;
                    shopCanvasGrop.interactable = true;
                    OpenItemShop();
                }
                else
                {
                    Time.timeScale = 1;

                    isShopOpen = false;
                    OnShopStateChanged?.Invoke(shopManager, false);

                    shopCanvasGrop.alpha = 0;
                    shopCanvasGrop.blocksRaycasts = false;
                    shopCanvasGrop.interactable = false;
                }

            }
        }
    }
    //------------三种物品类型的切换---------------
    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);
    }
    public void OpenWeaponShop()
    {
        shopManager.PopulateShopItems(shopWeapons);

    }
    public void OpenArmourShop()
    {
        shopManager.PopulateShopItems(shopArmour);

    }


    //---------------------玩家进入与退出检测------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }
}
