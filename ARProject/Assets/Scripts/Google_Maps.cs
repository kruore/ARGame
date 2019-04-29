using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.Networking;
using System;

public class Google_Maps : Singleton<Google_Maps>
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
    public GameObject player;
    
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
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();
                Rect rect = new Rect(0, 0, ((DownloadHandlerTexture)www.downloadHandler).texture.width, ((DownloadHandlerTexture)www.downloadHandler).texture.height);
                SpriteRenderer img = gameObject.GetComponent<SpriteRenderer>();
                img.sprite = Sprite.Create(((DownloadHandlerTexture)www.downloadHandler).texture, rect, new Vector2(0.5f, 0.5f));
            }
            latitude.text = GPS.Instance.Latitude.ToString();
            longitude.text = GPS.Instance.Longitude.ToString();
            //img.sprite = www.texture;
            //img.SetNativeSize();
            yield return new WaitForSecondsRealtime(3);
        }
    }
    private IEnumerator StartcompassServiece()
    {
        while (true)
        {
            player.transform.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
    //Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Mapsupdate());
        StartCoroutine(StartcompassServiece());
    }
}
