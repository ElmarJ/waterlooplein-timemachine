using GeoJsonCityBuilder.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AutomaticSunPosition : MonoBehaviour
{
    public Coordinate worldPosition;
    public int year;
    public int month;
    public int day;
    public int hour;

    private static readonly string[] monthNames = { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

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

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
#endif
    public static void RegisterConverters()
    {    
        var monthConverterGroup = new ConverterGroup("Integer To Month Name (string)");
        monthConverterGroup.AddConverter((ref int value) => monthNames[value - 1]);
        ConverterGroups.RegisterConverterGroup(monthConverterGroup);

        var hourConverterGroup = new ConverterGroup("Integer To formatted time string");
        hourConverterGroup.AddConverter((ref int value) => $"{value:00}:00");
        ConverterGroups.RegisterConverterGroup(hourConverterGroup);
    }
    
}
