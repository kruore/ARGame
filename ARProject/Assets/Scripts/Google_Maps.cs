using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.Networking;
using System;

public class Google_Maps : MonoBehaviour
{

    string url = "";
    public double lat = 37.713364, lon = 126.890129;
    LocationInfo li;
    public string strBaseURL = "https://maps.googleapis.com/maps/api/staticmap?center=";
    public int zoom = 15;
    public int mapWidth = 640;
    public int mapHeight = 640;
    public enum mapType { roadmap, satellite, hybrid, terrain };
    public mapType mapselected = mapType.roadmap;
    public int scale = 2;
    public string strPath = "weight:3%7Ccolor:blue%7Cenc:{coaHnetiVjM??_SkM??~R";
    public string GoogleAPIKey = "AIzaSyBPENHUkpJHP24GDn98EaqW8qkZeO86pM0";
    public UILabel latitude;
    public UILabel longitude;
    public UILabel prefablat;
    public UILabel prefablon;
    public GameObject player;
    public List<GameObject> placeobject;

    public IEnumerator Mapsupdate()
    {
        while (true)
        {
            
            url = "https://maps.googleapis.com/maps/api/staticmap"
                + "?center=" + GPS.Instance.Latitude.ToString() + "," + GPS.Instance.Longitude.ToString()
                + "&zoom=" + zoom
                + "&size=" + mapWidth + "x" + mapHeight
                + "&scale=" + scale
                + "&maptype=" + mapselected
                + "&key=" + GoogleAPIKey;
            Debug.Log(url);
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();
            Rect rect = new Rect(0, 0, ((DownloadHandlerTexture)www.downloadHandler).texture.width, ((DownloadHandlerTexture)www.downloadHandler).texture.height);
            SpriteRenderer img = gameObject.GetComponent<SpriteRenderer>();
            img.sprite = Sprite.Create(((DownloadHandlerTexture)www.downloadHandler).texture, rect, new Vector2(0.5f, 0.5f));
            Resources.UnloadUnusedAssets();
            Placemove();
            latitude.text = GPS.Inst.Latitude.ToString();
            longitude.text = GPS.Inst.Longitude.ToString();
            yield return new WaitForSeconds(1);
        }
    }
    private IEnumerator StartcompassServiece()
    {
        while (true)
        {
            player.transform.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);
            yield return new WaitForSecondsRealtime(1);
        }
    }
    //Start is called before the first frame update
    void Start()
    {
        Placesetting();
        StartCoroutine(Mapsupdate());
        StartCoroutine(StartcompassServiece());
        
    }
    //map Scene종료전에 필히 호출할것
    void Sceneend()
    {
        StopCoroutine(Mapsupdate());
        StopCoroutine(StartcompassServiece());
    }
    void Placesetting()
    {
        foreach (GpsData place in GameDataBase.Inst.currentquadplaces)
        {
            GameObject ppp= Instantiate(Resources.Load("ppp") as GameObject);
            ppp.transform.position = new Vector3(0,0,0);
            placeobject.Add(ppp);
            Debug.Log("setting");
        }
    }
    void Placemove()
    {
        List<GpsData> quadplaces = GameDataBase.Inst.currentquadplaces;
        for (int i=0;i< placeobject.Count;i++)
        {
            Debug.Log("move");
            float longi =(float)(quadplaces[i].longitude - GPS.Instance.Longitude);
            Debug.Log(longi);
            float latti = (float)(quadplaces[i].latitude - GPS.Instance.Latitude);
            Debug.Log(latti);
            placeobject[i].transform.position=new Vector3(longi*30000,2 ,latti*30000);
            prefablat.text = placeobject[i].transform.position.x.ToString();
            prefablon.text = placeobject[i].transform.position.z.ToString();
        }
        Debug.Log("moving");
    }
}
