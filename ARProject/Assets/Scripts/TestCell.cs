using UnityEngine;
using System.Collections;

// 셀 데이터를 받아서 스크롤뷰셀의 데이터를 갱신한다.
public class TestCell : UIReuseScrollViewCell 
{
	public UILabel label;
    Vector3 pointer;
    //해당오브젝트가 클릭인지 드래그인지 판별하기위한 이동위치 크기
    int tolerance=3;

    public override void UpdateData (IReuseCellData CellData)
	{
		ItemCellData item = CellData as ItemCellData;
		if( item == null )
			return;

		label.text = string.Format( "{0} {1}",item.ImgName, item.Index );
	}
    public void OnMouseDown()
    {
        pointer = transform.position;
    }
    public void OnMouseUp()
    {
        if(Mathf.Abs(pointer.x-transform.position.x)<tolerance)
        {
            
        }
    }
}
