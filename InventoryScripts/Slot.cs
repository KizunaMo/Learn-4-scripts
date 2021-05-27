using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public Text slotNumber;

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInformation);//顯示文本當點擊物品時
    }
}
