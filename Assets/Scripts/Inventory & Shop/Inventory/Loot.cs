using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 给战利品用的
/// </summary>
public class Loot : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;

    public bool canBePickUp = true;
    public int quantity;
    public static event Action<ItemSO, int> OnItemLooted;

    // Inspector 窗口的值发生变化时，立刻对数据进行验证、修正或初始化
    private void OnValidate()
    {
        if (itemSO ==null)
        {
            return;
        }
        UpdateAppearance();
    }

    //初始化数据
    internal void Initialize(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
        canBePickUp=false;
        UpdateAppearance();
    }

    //更新显示
    private void UpdateAppearance()
    {
        sr.sprite = itemSO.icon;
        name = itemSO.itemName;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&canBePickUp==true)
        {
            anim.Play("LootPickup");
            OnItemLooted?.Invoke(itemSO, quantity);
            Destroy(gameObject, 0.5f);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBePickUp=true;
        }
    }
}
