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
    private double west = 131.872799, east = 124.609875, south = 33.112479, north = 38.617382;
    private string currenttree;
    public string Currenttree
    {
        set
        {
            if (this.currenttree != value)
            {
                //현재 트리에 맞는 장소 리스트 재조정
                this.currenttree = value;
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
            if (this.latitude != value)
            {
                this.latitude = value;
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
        latitude = 37.713364;
        longitude = 126.890129;
        currenttree = "";

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
                yield break;
            }
            Input.location.Start();
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                yield return new WaitForSeconds(2);
            }
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                yield break;
            }
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            Debug.Log("aa");
            Locationquadtree(west, east, north, south);
            Input.location.Stop();
            yield return new WaitForSecondsRealtime(3);
        }
    }
    private bool Locationquadtree(double dwest, double deast, double dnorth, double dsouth)
    {
        bool checklon;
        int position;
        double dlon, dlat;
        dlon = (dwest + deast) / 2;
        dlat = (dnorth + dsouth) / 2;
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
        currenttree += position;
        if (currenttree.Length < 8)
        {
            switch (position)
            {
                //서북
                case 1:
                    Debug.Log(dwest + "," + dlon + "," + dnorth + "," + dlat);
                    Locationquadtree(dwest, dlon, dnorth, dlat);
                    break;
                //동북
                case 2:
                    Debug.Log(dlon + "," + deast + "," + dnorth + "," + dlat);
                    Locationquadtree(dlon, deast, dnorth, dlat);
                    break;
                //서남
                case 3:
                    Debug.Log(dwest + "," + dlon + "," + dlat + "," + dsouth);
                    Locationquadtree(dwest, dlon, dlat, dsouth);
                    break;
                //동남
                case 4:
                    Debug.Log(dlon + "," + deast + "," + dlat + "," + dsouth);
                    Locationquadtree(dlon, deast, dlat, dsouth);
                    break;
            }
        }
        else if (currenttree.Length == 8)
        {
            return true;
        }
        else
        {
            return false;
        }
        return false;
    }
}