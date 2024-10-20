using GeoJsonCityBuilder.Data;
using NUnit.Framework.Interfaces;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AutomaticSunPosition : MonoBehaviour
{
    public Coordinate worldPosition;
    public int year;
    public int month;
    public int day;
    public int hour;
    private DateTime dateLastFrame;
    private DateTime Date => new(year, month, day, hour, 0, 0);
    public UnityEvent SunSetOrRise;
    public bool sunIsUp;


    private static readonly string[] monthNames = { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

    // Update is called once per frame
    void Update()
    {
        // Only update if the date has changed (has huge CPU impact)
        // TODO: reimplement outside of frame-lifecycle system
        if (dateLastFrame != Date)
        {
            dateLastFrame = Date;
            UpdateSunPosition();
        }
    }

    public void UpdateSunPosition()
    {
        DateTime localTime = this.Date;
        var amsterdamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        var utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, amsterdamTimeZone);
        CoordinateSharp.Coordinate c = new(worldPosition.Lat, worldPosition.Lon, utcTime);
        var ci = c.CelestialInfo;

        var previousSunIsUp = this.sunIsUp;
        this.sunIsUp = ci.SunAltitude > 0;
        if (previousSunIsUp != this.sunIsUp)
        {
            this.SunSetOrRise?.Invoke();
        }

        gameObject.transform.rotation = Quaternion.Euler((float)ci.SunAltitude, (float)ci.SunAzimuth - 180, 0);
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
