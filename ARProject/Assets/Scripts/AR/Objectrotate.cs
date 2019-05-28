using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectrotate : MonoBehaviour
{
    GpsData placedata;
    int objectindex;
    private void Start()
    {
        placedata = GameManager.Inst.currentplace;
        switch (objectindex)//해당 오브젝트일때 넣을 이미지 및 이펙트
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                break;
        }
        gameObject.transform.Rotate(new Vector3(0,Random.Range(0,359),0));
        Debug.Log(gameObject.transform.localRotation.y);
    }
    //오브젝트 터치시 동작할것
    public void OnMouseDown()
    {
        
    }
}
