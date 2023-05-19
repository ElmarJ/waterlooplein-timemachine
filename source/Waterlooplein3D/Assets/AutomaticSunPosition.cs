using GeoJsonCityBuilder.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticSunPosition : MonoBehaviour
{
    public Coordinate worldPosition;
    public int year;
    public int month;
    public int day;
    public int hour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSunPosition();
    }

    public void UpdateSunPosition()
    {
        DateTime time = new(year, month, day, hour, 0, 0);
        CoordinateSharp.Coordinate c = new(worldPosition.Lat, worldPosition.Lon, time);
        var ci = c.CelestialInfo;
        var alt = c.CelestialInfo.SunAltitude;
        gameObject.transform.rotation = Quaternion.Euler((float) ci.SunAltitude, (float) ci.SunAzimuth - 180, 0);
    }
}
