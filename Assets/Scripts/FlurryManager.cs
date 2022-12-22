#define IV_FLURRY_LANG_KR

using UnityEngine;
using FlurrySDK;
using System;

public class FlurryManager : MonoBehaviour
{
    public static string IV_FLURRY_AOS_KEY = "CW5CG9YT4VV7FJGM62RZ";// FLURRY key
    public static string IV_FLURRY_AOS_MOBILE_KEY = "4YGTCK97HK2RXQ7M5V9N";// FLURRY key
    public static string IV_FLURRY_IOS_MOBILE_KEY = "K7QR96Q4XBPYCJQJTC86";// K7QR96Q4XBPYCJQJTC86   
#if IV_FLURRY_LANG_KR
    public static string IV_FLURRY_EVENT_PREFIX = "EX2KR_";// FLURRY key
#elif IV_FLURRY_LANG_JP
    public static string IV_FLURRY_EVENT_PREFIX = "EX2JP_";// FLURRY key
#elif IV_FLURRY_LANG_US || IV_FLURRY_LANG_EN
    public static string IV_FLURRY_EVENT_PREFIX = "EX2US_";// FLURRY key
#else
    public static string IV_FLURRY_EVENT_PREFIX = "EX2KR_";// FLURRY key
#endif

    public static void InitFlurry()
    {
        Debug.Log("InitFlurry");

        // Initialize Flurry once.
#if UNITY_ANDROID
        // Note: When enabling Messaging, Flurry Android should be initialized by using AndroidManifest.xml.
        new Flurry.Builder()
                  .WithCrashReporting(true)
                  .WithLogEnabled(true)
                  .WithLogLevel(Flurry.LogLevel.VERBOSE)
                  .WithMessaging(false)
                  .Build(IV_FLURRY_AOS_MOBILE_KEY);
        Flurry.SetVersionName(Application.version);
#elif UNITY_IOS
        new Flurry.Builder()
                          .WithAppVersion(Application.version)
                          .WithCrashReporting(true)
                          .WithLogEnabled(true)
                          .WithLogLevel(Flurry.LogLevel.VERBOSE)
                          .WithMessaging(false)
                          .Build(IV_FLURRY_AOS_MOBILE_KEY);
#else 
        throw new Exception("Unsupported platform detected.");
#endif
        // Example to get Flurry versions.
        Debug.Log("AgentVersion: " + Flurry.GetAgentVersion());
        Debug.Log("ReleaseVersion: " + Flurry.GetReleaseVersion());

        // Set user preferences.
        Flurry.SetReportLocation(true);
        Flurry.UserProperties.Set(Flurry.UserProperties.PROPERTY_REGISTERED_USER, "True");

        // Log Flurry events.
        Flurry.EventRecordStatus status = LogFlurry("Into.Init");
        Debug.Log("Log Unity Event status: " + status);
    }

    public static Flurry.EventRecordStatus LogFlurry(string e)
    {
        Flurry.EventRecordStatus status = Flurry.LogEvent(IV_FLURRY_EVENT_PREFIX + e);
        Debug.Log(IV_FLURRY_EVENT_PREFIX + e + ":" + status);

        return status;
    }


    private void Awake()
    {
        InitFlurry();
    }
}
