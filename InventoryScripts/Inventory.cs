using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory",menuName ="Inventory/New Inventory")]//在滑鼠點右鍵時，可以多一個選項新增一個物件

public class Inventory : ScriptableObject
{
    public List<Item> frogItemList = new List<Item>();//建立一個列表，這個列表存在的東西是item裡面的東西；定義新的陣列

}
