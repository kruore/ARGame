using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// 메인 클래스. 여기서 grid에 데이터를 추가시켜준다.
public class GUITestScrollView : MonoBehaviour 
{
    public int count;
    private UIReuseGrid grid;
    public List<Item> inventory;
    public UIReuseGrid Grid
    {
        get
        {
            return grid;
        }
    }

    void Awake()
    {
		grid = GetComponentInChildren<UIReuseGrid>();
    }

	void Start () 
    {
        inventory = GameDataBase.Instance.cards_Inventory;
        count = inventory.Count;
        // 임의의 데이터가 생성해서 gird에 추가시켜둔다.
        // ItemCellData 는 IReuseCellData 상속받아서 구현된 데이터 클래스다.
        for ( int i=0; i< inventory.Count; ++i )
        {
			ItemCellData cell = new ItemCellData();
			cell.Index = inventory[i].cardInventoryNum;
			cell.ImgName = inventory[i].cardName;
            cell.HP = inventory[i].cardHp;
            cell.cost = inventory[i].cardCost;
            cell.element = inventory[i].cardElement;
            grid.AddItem( cell, false );
        }
		grid.UpdateAllCellData();
	}

	#region Event
	public void EV_Add()
	{
		ItemCellData cell = new ItemCellData();
		cell.Index = grid.MaxCellData;
		cell.ImgName = string.Format( "name:{0}", cell.Index );
		grid.AddItem( cell, true );
	}

	public void EV_Remove(int removecellnumber)
	{
		grid.RemoveItem( grid.GetCellData(removecellnumber), true );
	}

	public void EV_RemoveAll()
	{
		grid.ClearItem(true);
	}
	#endregion
}
