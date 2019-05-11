using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class GPS : MonoBehaviour
{
    string url = "";
    public double lat = 37.713364f, lon = 126.890129f;
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
    public GameObject player;
    public List<GameObject> placeobject;
    private Gyroscope gyro;
    public int count = 0;
    //동서는 longitude 남북은 latitude 
    private double west = 131.872799f, east = 124.609875f, south = 33.112479f, north = 38.617382f;
    private string currenttree = "44444444";
    string nowposition;
    public static string test;
    public static string finddingstring;
    public static string mystring;
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
                test = value;
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
            if (latitude != value)
            {
                latitude = value;
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
            if (longitude != value)
            {
                longitude = value;
            }
        }
        get { return longitude; }
    }





    // Start is called before the first frame update
    void Start()
    {
        Input.compass.enabled = true;
        StartCoroutine(StartLocationServiece());
        StartCoroutine(StartcompassServiece());
        gyro = Input.gyro;
        gyro.enabled = true;
        sCurrenttree = "44444444";
    }

    private IEnumerator StartLocationServiece()
    {
        while (true)
        {

            if (!Input.location.isEnabledByUser)
            {
                yield break;
            }
            Input.location.Start();
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                yield return new WaitForSeconds(1f);
            }
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                yield break;
            }

            nowposition = "";
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            //Latitude = 37.713364;
            //Longitude = 126.890129;
            Locationquadtree(west, east, north, south);
            sCurrenttree = nowposition;
            Debug.Log(sCurrenttree);
            Input.location.Stop();
            //nowposition = "";
            //Locationquadtree(west, east, north, south);
            count++;
            url = "https://maps.googleapis.com/maps/api/staticmap"
                + "?center=" + Latitude.ToString() + "," + Longitude.ToString()
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
            Placemove();
            Resources.UnloadUnusedAssets();
            yield return new WaitForSecondsRealtime(3);
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
        else if (nowposition.Length == 8)
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
            yield return new WaitForSecondsRealtime(1);
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
                placeobject[i].transform.position = new Vector3((float)longi * 40000, 2, (float)latti * 50000);
                if (longi < 0.0003 && latti < 0.0003)
                {
                    quadplaces[i].placestate = true;
                }
                else
                {
                    quadplaces[i].placestate = false;
                }
                Debug.Log("moving");
                mystring = longi+","+ latti;
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