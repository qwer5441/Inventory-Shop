using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDes;
    public Sprite icon;

    public bool isGold;
    public int stackSize = 3; // 最大存储数量
    public int currentHealth;
    public int MaxHealth;
    public int speed;
    public int damage;

    public float duration;//持续时间
}
