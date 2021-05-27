using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item WhatIsThisItem;//要把之前設定的Item物件拖移到unity public的選項內。這個腳本要使用哪一個物件。
    public Inventory playerInvetory;//要把之前設定的包包物件，拖移進unity public 的選項內。就是這個腳本要使用哪一個包包。

    private void OnTriggerEnter2D(Collider2D collision)//碰撞的時候要做什麼事情
    {
        if (collision.gameObject.CompareTag("Player"))//當碰撞到TAG為Player時，做以下動作
        {
            AddNewItem(); //執行AddNewItem()函式
            Destroy(gameObject);//破壞物件
        }   
    }
    public void AddNewItem()
    {
        if (!playerInvetory.frogItemList.Contains(WhatIsThisItem))//如果unity public的包包底下的LIST清單裡面不包含WhatIsThisItem(設定的Item物件);
        {
            playerInvetory.frogItemList.Add(WhatIsThisItem);//新增該物件到該包包的List底下
            //InventoryManager.CreatNewItem(WhatIsThisItem);//在IventoryManager的class底下新增一個物件，為當前物件unity public選擇的物件
        }
        else //如果上面沒有執行，則執行下方動作
        {
            WhatIsThisItem.itemHeld += 1;//unity public 的物件+1
        }

        InventoryManager.RefreshItem();//執行Inventory這個class底下的RefreshItem的函式。
    }
}
