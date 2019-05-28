using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collectingobject : MonoBehaviour
{
    public GpsData placedata;
    public int objectindex;
    private void OnMouseDown()
    {
        
        if (0.0003>Mathf.Abs((float)GPSTest.inst.Longitude-(float)placedata.longitude)&& 0.0003>Mathf.Abs((float)GPSTest.inst.Latitude- (float)placedata.latitude))
        {
            GameObject.Find("UI Root/Test6").GetComponent<UILabel>().text = Mathf.Abs((float)GPSTest.inst.Latitude - (float)placedata.latitude).ToString();
            GameManager.Inst.currentplace = placedata;
            GameManager.Inst.objectindex = objectindex;

            SceneManager.LoadScene("SampleScene");
        }
    }
}
