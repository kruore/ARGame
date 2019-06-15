using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    None,Stone,Wood,Grass,Chaos
}
[System.Serializable]
public class GpsData
{
    public int Number;
    public string Name;
    public double latitude;
    public double longitude;
    public Element element;
    public string quad;
    public bool placestate=false;
    //num==DBnumber,nam==placename,lat==GPSlatitude,lon==GPSlongitude
    public GpsData (int num, string nam, double lat, double lon,Element element, string quad)
    {

        this.Number = num;
        this.Name = nam;
        this.latitude = lat;
        this.longitude = lon;
        this.element = element;
        this.quad = quad;
    }

}
