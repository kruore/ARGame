using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// 메인 클래스. 여기서 grid에 데이터를 추가시켜준다.
public class GUITestScrollView : MonoBehaviour 
{
    public int count;
    private UIReuseGrid grid;
    public List<Item> inventory;
    public List<Item> items=new List<Item>();
    public List<Item> categoryinventory;
    public GameObject copydb;
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
        if(grid==null)
        {
            Debug.Log("grid == null");
        }
        
    }

	void Start () 
    {
        items = CPGameDataBase.inst.GetComponent<CPGameDataBase>().cpcards_Inventory;
        inventory = GameDataBase.Instance.cards_Inventory;
        count = inventory.Count;
        // 임의의 데이터가 생성해서 gird에 추가시켜둔다.
        // ItemCellData 는 IReuseCellData 상속받아서 구현된 데이터 클래스다.
        for ( int i=0; i< inventory.Count; ++i )
        {
			ItemCellData cell = new ItemCellData();
			cell.num = inventory[i].cardInventoryNum;
			cell.ImgName = inventory[i].cardName;
            cell.cost = inventory[i].cardCost;
            cell.element = inventory[i].cardElement;
            cell.damage = inventory[i].cardDamage;
            cell.rank = inventory[i].cardRank;
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
    public void EV_UpdateAll()
    {
        categoryinventory.Clear();
        if (UIButton.current.gameObject.CompareTag("Wood"))
        {
            CPGameDataBase.inst.element = Element.Wood;
        }
        else if (UIButton.current.gameObject.CompareTag("Stone"))
        {
            CPGameDataBase.inst.element = Element.Stone;
        }
        else if (UIButton.current.gameObject.CompareTag("Grass"))
        {
            CPGameDataBase.inst.element = Element.Grass;
        }
        else if (UIButton.current.gameObject.CompareTag("Chaos"))
        {
            CPGameDataBase.inst.element = Element.Chaos;
        }
        else if (UIButton.current.gameObject.CompareTag("None"))
        {
            CPGameDataBase.inst.element = Element.None;
        }
        if (CPGameDataBase.inst.currentcpDBstate == cpDBState.Deactivecard)
        {
            foreach (Item card in GameDataBase.Inst.cards)
            {
                if (CPGameDataBase.inst.element == Element.None)
                {
                    categoryinventory.Add(card);
                }
                else if (card.cardElement == CPGameDataBase.inst.element)
                {
                    categoryinventory.Add(card);
                }
            }
            
        }
        else
        {
            foreach (Item card in CPGameDataBase.inst.cpcards_Inventory)
            {
                if (CPGameDataBase.inst.element == Element.None)
                {
                    categoryinventory.Add(card);
                }
                else if (card.cardElement == CPGameDataBase.inst.element)
                {
                    categoryinventory.Add(card);
                }
            }
        }
        CPGameDataBase.inst.testScrollView.items = categoryinventory;
        grid.ClearItem(true);
        inventory = items;
        count = inventory.Count;
        // 임의의 데이터가 생성해서 gird에 추가시켜둔다.
        // ItemCellData 는 IReuseCellData 상속받아서 구현된 데이터 클래스다.
        for (int i = 0; i < inventory.Count; ++i)
        {
            ItemCellData cell = new ItemCellData();
            cell.Index= inventory[i].cardInventoryNum;
            cell.num = inventory[i].cardInventoryNum;
            cell.ImgName = inventory[i].cardName;
            cell.rank = inventory[i].cardRank;
            cell.cost = inventory[i].cardCost;
            cell.element = inventory[i].cardElement;
            cell.damage = inventory[i].cardDamage;
            grid.AddItem(cell, false);
        }
        grid.UpdateAllCellData();
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
