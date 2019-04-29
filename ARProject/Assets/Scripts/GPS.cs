using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class GPS : Singleton<GPS>
{
    private Gyroscope gyro;

    //동서는 longitude 남북은 latitude 
    private float west = 131.872799f, east = 124.609875f, south = 33.112479f, north = 38.617382f;
    private string currenttree="";
    string nowposition;
    public string test;
    public string sCurrenttree
    {
        set
        {
            if (currenttree != value)
            {
                //현재 트리에 맞는 장소 리스트 재조정
                currenttree = value;
               //GameDataBase.Instance.DS_GpsplaceFinding(value);
                

            }
        }
        get { return currenttree; }
    }

    [SerializeField]
    private float latitude;
    public float Latitude
    {
        set
        {
            if (this.latitude != value)
            {
                this.latitude = value;
            }
        }
        get { return latitude; }
    }
    [SerializeField]
    private float longitude;
    public float Longitude
    {
        set
        {
            if (this.longitude != value)
            {
                this.longitude = value;
            }
        }
        get { return longitude; }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Permissioncheck());
        Input.compass.enabled = true;
        StartCoroutine(StartLocationServiece());
        gyro = Input.gyro;
        gyro.enabled = true;
        sCurrenttree = "";
    }
    private IEnumerator Permissioncheck()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            yield return null;
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            yield return null;
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            yield return null;
        }
    }
    private IEnumerator StartLocationServiece()
    {
        while (true)
        {

            if (!Input.location.isEnabledByUser)
            {
                test = "enablefail";
                yield break;
            }
            Input.location.Start();
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                test = "Initializfail";
                yield return new WaitForSeconds(2);
            }
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                test = "fail";
                yield break;
            }
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            nowposition = string.Empty;
            Locationquadtree(west, east, north, south);
            sCurrenttree = nowposition;
            Debug.Log(sCurrenttree);
            Input.location.Stop();
            test = "good";
            yield return new WaitForSecondsRealtime(3);
        }
    }
    private bool Locationquadtree(float dwest, float deast, float dnorth, float dsouth)
    {
        bool checklon;
        int position;
        float dlon, dlat;
        dlon = (float)(dwest + deast) / 2;
        dlat = (float)(dnorth + dsouth) / 2;
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
        else if (sCurrenttree.Length == 8)
        {
            return true;
        }
        else
        {
            return false;
        }
        return false;
    }
    void Quadinitobject(string quadtree)
    {

    }
}