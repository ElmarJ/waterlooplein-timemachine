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
        DateTime localTime = new(year, month, day, hour, 0, 0);
        var amsterdamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        var utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, amsterdamTimeZone);
        CoordinateSharp.Coordinate c = new(worldPosition.Lat, worldPosition.Lon, utcTime);
        var ci = c.CelestialInfo;
        gameObject.transform.rotation = Quaternion.Euler((float) ci.SunAltitude, (float) ci.SunAzimuth - 180, 0);
    }
}
