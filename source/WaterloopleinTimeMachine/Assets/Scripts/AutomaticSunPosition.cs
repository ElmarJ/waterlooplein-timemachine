using GeoJsonCityBuilder.Data;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AutomaticSunPosition : MonoBehaviour
{
    public Coordinate worldPosition;
    public TimeController timeController;
    public UnityEvent SunSetOrRise;
    public bool sunIsUp;
    private static readonly string[] monthNames = { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

    void Start()
    {
        timeController.OnMonthChanged.AddListener(UpdateSunPosition);
        timeController.OnDayChanged.AddListener(UpdateSunPosition);
        timeController.OnTimeChanged.AddListener(UpdateSunPosition);
    }

    public void UpdateSunPosition()
    {
        DateTime localTime = timeController.Date;
        var amsterdamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        var utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, amsterdamTimeZone);

        CoordinateSharp.Coordinate c = new(worldPosition.Lat, worldPosition.Lon, utcTime);
        var ci = c.CelestialInfo;

        var previousSunIsUp = sunIsUp;
        sunIsUp = ci.SunAltitude > 0;
        if (previousSunIsUp != sunIsUp)
        {
            SunSetOrRise?.Invoke();
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
