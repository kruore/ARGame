using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// 셀 데이터를 받아서 스크롤뷰셀의 데이터를 갱신한다.
public class TestCell : UIReuseScrollViewCell
{
    public UILabel label;
    public UISprite sprite;
    public bool check;
    Vector3 pointer;
    [SerializeField]
    public ItemCellData citem= new ItemCellData();
    public int num,cost,damage,rank;
    public Element element;
    public string imgname;
    Item item0;
    public override void UpdateData(IReuseCellData CellData)
    {
        citem.num = CellData.num;
        citem.ImgName = CellData.ImgName;
        citem.cost = CellData.cost;
        citem.element = CellData.element;
        citem.damage = CellData.damage;
        citem.rank = CellData.rank;
        num = CellData.num;
        imgname = CellData.ImgName;
        cost = CellData.cost;
        element = CellData.element;
        damage = CellData.damage;
        rank = CellData.rank;
        if (citem.Equals(null))
            return;
        if (InventorysceneManager.inst.currentcpDBstate.Equals(cpDBState.Deactivecard))
        {
            foreach (Item item in GameDataBase.Inst.cards_Inventory)
            {
                if (item.cardName.Equals(CellData.ImgName))
                {
                    citem.check = false;
                    check = citem.check;
                    break;
                }
                else
                {
                    foreach (Item item0 in GameDataBase.Inst.cards_Deck)
                    {
                        if (item0.cardName.Equals(CellData.ImgName))
                        {
                            citem.check = false;
                            check = citem.check;
                            break;
                        }
                        else
                        {
                            citem.check = true;
                            check = citem.check;
                        }
                    }
                }
            }
        }
        if (InventorysceneManager.inst.currentcpDBstate.Equals(cpDBState.Multiselect))
        {
            if(InventorysceneManager.inst.DestoryItem.Count>0)
            {
                foreach (int item in InventorysceneManager.inst.DestoryItem)
                {
                    if (item.Equals(citem.num))
                    {
                        citem.check = true;
                        check = citem.check;
                        break;
                    }
                    else
                    {
                        citem.check = false;
                        check = citem.check;
                    }
                }
            }
            else
            {
                citem.check = false;
                check = citem.check;
            }
            
        }
        //label.text = string.Format("{0} {1}", citem.ImgName, citem.Index);
        sprite.spriteName = citem.ImgName;
        if (citem.check.Equals(true) && InventorysceneManager.inst.currentcpDBstate.Equals(cpDBState.Deactivecard))
        {
            GetComponentInChildren<UISprite>().color = new Color(0.52f, 0.52f, 0.52f);

        }
        else if(citem.check.Equals(true) && InventorysceneManager.inst.currentcpDBstate.Equals(cpDBState.Multiselect))
        {
            GetComponentInChildren<UISprite>().alpha = 0.5f;
        }
        else
        {
            GetComponentInChildren<UISprite>().color = new Color(1, 1, 1);
            GetComponentInChildren<UISprite>().alpha = 1f;
        }
        GetComponent<AddDeckButton>().item = citem;
    }
}