using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// 셀 데이터를 받아서 스크롤뷰셀의 데이터를 갱신한다.
public class SecondCell : UIReuseScrollViewCell
{
    public UILabel Name,Count;
    public UISprite Icon;
    public bool check;
    Vector3 pointer;
    [SerializeField]
    public Toolcelldata citem = new Toolcelldata();
    public int num, cost, damage, rank;
    public Element element;
    public string imgname;
    Item item0;
    private void Start()
    {
    }
    public override void UpdateData(IReuseCellData CellData)
    {
        citem.num = CellData.num;
        citem.ImgName = CellData.ImgName;
        citem.cost = CellData.cost;
        citem.element = CellData.element;
        citem.damage = CellData.damage;
        citem.rank = CellData.rank;
        Icon.spriteName = citem.ImgName;
        Name.text = citem.ImgName;
        Count.text = "x"+citem.cost.ToString();
    }
}