using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;

    public Text itemNameText;
    public Text itemDescriptionText;
    public Text[] statTexts;
    private RectTransform infoPanelRect;
    private void Awake()
    {
        infoPanelRect= GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        infoPanel.alpha = 1;
        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDes;  

        List<string>stats=new List<string>();
        if (itemSO.currentHealth > 0) stats.Add("生命值:" + itemSO.currentHealth.ToString());
        if (itemSO.damage > 0) stats.Add("攻击力：" + itemSO.damage.ToString());
        if (itemSO.speed > 0) stats.Add("速度：" + itemSO.speed.ToString());
        if (itemSO.duration > 0) stats.Add("延迟：" + itemSO.duration.ToString());

        if (stats.Count <= 0)
            return;
        for (int i = 0; i < statTexts.Length; i++)
        {
            if (i < stats.Count)
            {
                statTexts[i].text = stats[i];
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
          
        }
        
    }

    /// <summary>
    /// 隐藏物品信息
    /// </summary>
    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    /// <summary>
    /// 鼠标跟随
    /// </summary>
    public void FollowMouse()
    {
        Vector3 mousePosition=Input.mousePosition;
        Vector3 offset = new Vector3(10, -10, 0);
        infoPanelRect.position= mousePosition+offset;
    }
}
