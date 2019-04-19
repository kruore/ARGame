using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPS : Singleton<GPS>
{
    private Gyroscope gyro;
    [SerializeField]
    private double latitude;
    [SerializeField]
    private UILabel aa;
    public double Latitude
    {
        set
        {

                GPS.Instance.latitude = value;
        }
        get { return latitude; }
    }
    [SerializeField]
    private double longitude;
    public double Longitude
    {
        set
        {

                GPS.Instance.longitude = value;

        }
        get { return longitude; }
    }
    public int count = 0;
    private Quaternion rotation = new Quaternion(0, 0, 1, 0);
    // Start is called before the first frame update
    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }
    void Start()
    {
        Input.compass.enabled = true;
        StartCoroutine(StartLocationServiece());
        gyro = Input.gyro;
        gyro.enabled = true;
        latitude = 7;
        longitude = 7;
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
            Input.location.Stop();
            yield return new WaitForSecondsRealtime(3);
        }
    }
}