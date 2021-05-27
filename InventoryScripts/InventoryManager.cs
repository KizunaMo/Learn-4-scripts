using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;//C# singleton的寫法，讓InventoryManager這個類Class=instance(可自己命名);紀錄第一個Instance然後，之後繼續使用該個Instance，只能有一個；

    public Inventory frogBag;
    public GameObject slotGrid;//包包格子
    public Slot slotPrefab;//該格子的prefab
    public Text itemInformation;


    private void Awake()//singleton用awake,如果有參考到別的class都用awake,awake執行於start之前。
    {
        if (instance != null)//如果instance不是不存在的物件，如果他已經存在
            Destroy(this);//銷毀這個instance
        instance = this;//instance等於當前的calss;上面先檢查有沒有存在instance了，如果都沒有，那instance等於這個class;如果有就先刪除那個instance，然後再讓instance等於這個classm

    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";//將instance的資訊欄位的text內容預設為空白
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;//預設為空白的text內容，更新成itemDescription
    }

    public static void CreatNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);//instantiate要臨時生成一個Slot這個calss類的的物件,生成的位置，生成時的角度
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);//設定這個生成的物件位置要放在哪，設定為放在父類的slotGrid底下
        newItem.slotItem = item;//在slot的class內有slotitem項目要設定,slot的Class內的soltitem覆值給CreatNewItem的class的item
        newItem.slotImage.sprite = item.itemImage;//在slot的class內有slotimage項目要設定
        newItem.slotNumber.text = item.itemHeld.ToString();//在slot的class內有slotnumber項目要設定
    }

    public static void RefreshItem()
    {
        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if(instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.frogBag.frogItemList.Count; i++)
        {
            CreatNewItem(instance.frogBag.frogItemList[i]);
        }
    }
}
