using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class GPSTest : MonoBehaviour
{
    public static GPSTest inst;
    public static string b;
    public static string c;
    string url = "";
    public static double lat, lon;
    LocationInfo li;

    public string strBaseURL = "https://maps.googleapis.com/maps/api/staticmap?center=";
    public int zoom = 17;
    public int mapWidth = 640;
    public int mapHeight = 640;
    public enum mapType { roadmap, satellite, hybrid, terrain };
    public mapType mapselected = mapType.roadmap;
    public int scale = 2;
    public string strPath = "weight:3%7Ccolor:blue%7Cenc:{coaHnetiVjM??_SkM??~R";
    public string GoogleAPIKey = "AIzaSyBPENHUkpJHP24GDn98EaqW8qkZeO86pM0";
    public string NaverAPIID = "yjj47jszgj";
    public GameObject player;
    public List<GameObject> placeobject;
    private Gyroscope gyro;
    public int count = 0;
    //동서는 longitude 남북은 latitude 
    private double west = 131.872799f, east = 124.609875f, south = 33.112479f, north = 38.617382f;
    private string currenttree = "44444444";
    string nowposition;
    public string sCurrenttree
    {
        set
        {
            Debug.Log(value);
            if (currenttree != value)
            {
                //현재 트리에 맞는 장소 리스트 재조정

                currenttree = value;
                if (placeobject.Count != 0)
                {
                    Placedestroy();
                }
                Placesetting();
                Placemove();
            }
        }
        get { return currenttree; }
    }

    [SerializeField]
    private double latitude;
    public double Latitude
    {
        set
        {
            if (0.0001 < Mathf.Abs((float)latitude - (float)value))
            {
                latitude = value;
                StartCoroutine(UpdateLocationServiece());
            }
        }
        get { return latitude; }
    }
    [SerializeField]
    private double longitude;
    public double Longitude
    {
        set
        {
            if (0.0001 < Mathf.Abs((float)longitude - (float)value))
            {
                longitude = value;
                StartCoroutine(UpdateLocationServiece());
            }
        }
        get { return longitude; }
    }

    bool mapcorutine = true;



    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        b = "0";
        StartCoroutine(StartLocationServiece());
    }

    private IEnumerator StartLocationServiece()
    {
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }
        b = "0.1";
        Input.location.Start();
        b = "0.2";
        while (Input.location.status.Equals(LocationServiceStatus.Initializing))
        {
            yield return new WaitForSeconds(1f);
        }
        b = "0.3";
        if (Input.location.status.Equals(LocationServiceStatus.Failed))
        {
            yield break;
        }
        b = "0.4";
        Input.compass.enabled = true;
        b = "0.5";
        c = "0";
        b = "0.6";
        gyro = Input.gyro;
        gyro.enabled = true;

        sCurrenttree = "44444444";
        nowposition = "";
        Latitude = Input.location.lastData.latitude;
        Longitude = Input.location.lastData.longitude;
        //b = "0.7";

        Locationquadtree(west, east, north, south);
        sCurrenttree = nowposition;
        Debug.Log(sCurrenttree);
        nowposition = "";
        Locationquadtree(west, east, north, south);

        //Latitude = 37.713364;
        //Longitude = 126.890129;

        url = "https://maps.googleapis.com/maps/api/staticmap"
            + "?center=" + Latitude.ToString() + "," + Longitude.ToString()
            + "&zoom=" + zoom
            + "&size=" + mapWidth + "x" + mapHeight
            + "&scale=" + scale
            + "&maptype=" + mapselected
            + "&key=" + GoogleAPIKey;
        //url = "https://openapi.map.naver.com/openapi/v3/maps.js?ncpClientId=" + NaverAPIID;

        Debug.Log(url);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        yield return new WaitForSeconds(1);
        Rect rect = new Rect(0, 0, ((DownloadHandlerTexture)www.downloadHandler).texture.width, ((DownloadHandlerTexture)www.downloadHandler).texture.height);
        SpriteRenderer img = gameObject.GetComponent<SpriteRenderer>();
        img.sprite = Sprite.Create(((DownloadHandlerTexture)www.downloadHandler).texture, rect, new Vector2(0.5f, 0.5f));
        b = "0.8";
        Placemove();
        b = "0.9";
        mapcorutine = false;
        StartCoroutine(StartcompassServiece());
        Resources.UnloadUnusedAssets();
        b = "0.a";
    }
    private IEnumerator UpdateLocationServiece()
    {

        count++;
        if (mapcorutine)
            yield break;
        mapcorutine = true;
        nowposition = "";
        Locationquadtree(west, east, north, south);
        sCurrenttree = nowposition;
        Debug.Log(sCurrenttree);
        url = "https://maps.googleapis.com/maps/api/staticmap"
            + "?center=" + Latitude.ToString() + "," + Longitude.ToString()
            + "&zoom=" + zoom
            + "&size=" + mapWidth + "x" + mapHeight
            + "&scale=" + scale
            + "&maptype=" + mapselected.ToString()
            + "&key=" + GoogleAPIKey;
        //url = "https://openapi.map.naver.com/openapi/v3/maps.js?ncpClientId=" + NaverAPIID;
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        Rect rect = new Rect(0, 0, ((DownloadHandlerTexture)www.downloadHandler).texture.width, ((DownloadHandlerTexture)www.downloadHandler).texture.height);
        SpriteRenderer img = gameObject.GetComponent<SpriteRenderer>();
        img.sprite = Sprite.Create(((DownloadHandlerTexture)www.downloadHandler).texture, rect, new Vector2(0.5f, 0.5f));
        Placemove();
        mapcorutine = false;
        Resources.UnloadUnusedAssets();


    }
    private void Update()
    {
        if (mapcorutine.Equals(false))
        {
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
        }
    }
    private bool Locationquadtree(double dwest, double deast, double dnorth, double dsouth)
    {
        bool checklon;
        int position;
        double dlon, dlat;
        dlon = (double)(dwest + deast) / 2;
        dlat = (double)(dnorth + dsouth) / 2;
        if (dlon < Longitude)
        {
            checklon = true;
        }
        else
        {
            checklon = false;
        }
        if (dlat < Latitude)
        {
            if (checklon)
            {
                //서북
                position = 1;
            }
            else
            {
                //동북
                position = 2;
            }
        }
        else
        {
            if (checklon)
            {
                //서남
                position = 3;
            }
            else
            {
                //동남
                position = 4;
            }
        }
        nowposition += position;
        if (nowposition.Length < 8)
        {
            switch (position)
            {
                //서북
                case 1:
                    Locationquadtree(dwest, dlon, dnorth, dlat);
                    break;
                //동북
                case 2:
                    Locationquadtree(dlon, deast, dnorth, dlat);
                    break;
                //서남
                case 3:
                    Locationquadtree(dwest, dlon, dlat, dsouth);
                    break;
                //동남
                case 4:
                    Locationquadtree(dlon, deast, dlat, dsouth);
                    break;
                default:
                    break;
            }
        }
        else if (nowposition.Length.Equals(8))
        {
            sCurrenttree = nowposition;
            return true;
        }
        else
        {
            return false;
        }
        return false;
    }

    private IEnumerator StartcompassServiece()
    {
        while (true)
        {
            player.transform.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);
            c = Input.compass.trueHeading.ToString();
            yield return null;
        }
    }
    //Start is called before the first frame update

    //map Scene종료전에 필히 호출할것
    void Sceneend()
    {
        StopCoroutine(StartcompassServiece());
    }

    void Placemove()
    {
        List<GpsData> quadplaces = GameDataBase.Instance.currentquadplaces;
        Debug.Log(placeobject.Count);
        if (quadplaces.Count != 0)
        {
            for (int i = 0; i < quadplaces.Count; i++)
            {
                double longi = (double)(quadplaces[i].longitude - Longitude);
                double latti = (double)(quadplaces[i].latitude - Latitude);
                placeobject[i].transform.position = new Vector3((float)longi * 50000, 2, (float)latti * 50000);
                Debug.Log("moving");
            }
        }
        else
        {
            Debug.Log("placeobjectnull");
        }
    }
    void Placesetting()
    {
        if (GameDataBase.Instance.currentquadplaces.Count != 0)
        {
            Placedestroy();
        }
        GameDataBase.Inst.DS_GpsplaceFinding(sCurrenttree);
        if (GameDataBase.Instance.currentquadplaces != null)
            foreach (GpsData place in GameDataBase.Instance.currentquadplaces)
            {
                GameObject ppp = Instantiate(Resources.Load("specialplace") as GameObject);
                ppp.GetComponent<Collectingobject>().placedata = place;
                ppp.transform.position = new Vector3(0, 0, 0);
                placeobject.Add(ppp);
                Debug.Log("setting");
            }
        else
        {
            Debug.Log("currentquadplacesnull");
        }

    }
    void Placedestroy()
    {
        Debug.Log("Destroy");
        if (placeobject != null)
        {
            foreach (GameObject place in placeobject)
            {
                Destroy(place);
            }
            placeobject.Clear();
        }
        else
        {
            Debug.Log("placeobjectnull");
        }

    }
}