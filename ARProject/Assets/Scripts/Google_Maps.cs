using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
public class Google_Maps : Singleton<Google_Maps>
{
    public SpriteRenderer img;
    string url = "";
    public double lat = 37.713364, lon = 126.890129;
    LocationInfo li;
    public string strBaseURL = "https://maps.googleapis.com/maps/api/staticmap?center=";
    public int zoom = 19;
    public int mapWidth = 640;
    public int mapHeight = 640;
    public enum mapType { roadmap, satellite, hybrid, terrain };
    public mapType mapselected = mapType.roadmap;
    public int scale = 2;
    public string strPath = "weight:3%7Ccolor:blue%7Cenc:{coaHnetiVjM??_SkM??~R";
    public string GoogleAPIKey = "AIzaSyBPENHUkpJHP24GDn98EaqW8qkZeO86pM0";
    public IEnumerator Mapupdate()
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
            WWW www = new WWW(url);

            yield return www;
            Rect rect = new Rect(0, 0, www.texture.width, www.texture.height);

            img.sprite = Sprite.Create(www.texture, rect, new Vector2(0.5f, 0.5f));
            //img.sprite = www.texture;
            //img.SetNativeSize();
            yield return new WaitForSecondsRealtime(3);
        }
    }
    //Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Mapupdate());
    }
}
