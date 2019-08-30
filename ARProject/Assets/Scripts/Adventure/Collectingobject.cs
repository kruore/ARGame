using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collectingobject : MonoBehaviour
{
    GpsData m_placedata;
    public GpsData placedata
    {
        get { return m_placedata; }
        set
        {
            switch (value.element)
            {
                case Element.Wood:
                    gameObject.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("tree-oak_T");
                    gameObject.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("LOW-POLY-COLORS");
                    break;
                case Element.Stone:
                    gameObject.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Rock_Round_m_2C_01");
                    break;
                case Element.Grass:
                    gameObject.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("grass05_T");
                    gameObject.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("LOW-POLY-COLORS");
                    break;
            }
            m_placedata = value;
        }
    }
    public int objectindex;
    private void OnMouseDown()
    {
        if (0.0003>Mathf.Abs((float)GPSTest.inst.Longitude-(float)placedata.longitude)&& 0.0003>Mathf.Abs((float)GPSTest.inst.Latitude- (float)placedata.latitude))
        {
            GameManager.Inst.currentplace = placedata;
            GameManager.Inst.objectindex = objectindex;
            GameManager.Inst.Scenestack.Push("ARscene");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
