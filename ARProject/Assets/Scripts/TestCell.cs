using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// 셀 데이터를 받아서 스크롤뷰셀의 데이터를 갱신한다.
public class TestCell : UIReuseScrollViewCell 
{
	public UILabel label;
    public UISprite sprite;
    Vector3 pointer;
    [SerializeField]
    public ItemCellData item;
    Item item0;
    private void Start()
    {
        GetComponent<AddDeckButton>().item = item;
    }
    public override void UpdateData (IReuseCellData CellData)
	{
		item = CellData as ItemCellData;
		if( item == null )
			return;


        label.text = string.Format( "{0} {1}",item.ImgName, item.Index );
        sprite.spriteName = item.ImgName;
        GetComponent<AddDeckButton>().item = item;
    }
}
