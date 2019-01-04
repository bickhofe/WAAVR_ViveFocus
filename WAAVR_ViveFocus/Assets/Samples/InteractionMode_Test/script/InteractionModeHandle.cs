using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using wvr;
using WaveVR_Log;

public class InteractionModeHandle : MonoBehaviour {
    private static string LOG_TAG = "InteractionModeHandle";
    private WaveVR_PermissionManager pmInstance = null;
    private static bool isDeny = false;
    private static int retryCount = 0;
    private static int RETRY_LIMIT = 0;
    private static bool requested = false;
    private static int systemCheckFailCount = 0;
    private WaveVR_Resource wvrRes = null;
    //bool inited = false;
    private WVR_InteractionMode _interactionMode;
    private WVR_InteractionMode wvr_interactionModeinit;
    private int _interactionModeinit;
    private int _gazeTriggermodeInit;
    private int _interactionModeStatus = 0;
    private string readinteractionModeInitValue = null;
    private string readgazeTriggermodeInitValue = null;
    private string interactionModeStatus = null;
    private bool isGazeMode = false;
    private const string CONTENT_PROVIDER_CLASSNAME = "com.htc.vr.unity.OEMContentProvider";
    private AndroidJavaObject contentProvider = null;

    // Use this for initialization
    void Start()
    {
        SetOverrideDefault(false);
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            return;
        }
        else
#endif
        {
            pmInstance = WaveVR_PermissionManager.instance;
            string[] tmpStr =
            {
                "vive.wave.vr.oem.data.OEMDataWrite", "vive.wave.vr.oem.data.OEMDataRead"
            };
            //pmInstance.requestPermissions(tmpStr, requestDoneCallback);
            if (pmInstance != null)
            {
                Log.d(LOG_TAG, "isPermissionGranted(vive.wave.vr.oem.data.OEMDataWrite) = " + pmInstance.isPermissionGranted("vive.wave.vr.oem.data.OEMDataWrite"));
                Log.d(LOG_TAG, "isPermissionGranted(vive.wave.vr.oem.data.OEMDataRead) = " + pmInstance.isPermissionGranted("vive.wave.vr.oem.data.OEMDataRead"));
                //Log.d(LOG_TAG, "isPermissionGranted(com.htc.vr.core.server.VRDataProvider) = " + pmInstance.isPermissionGranted("com.htc.vr.core.server.VRDataProvider"));
            }

            AndroidJavaClass ajc = new AndroidJavaClass(CONTENT_PROVIDER_CLASSNAME);
            if (ajc == null)
            {
                Log.e(LOG_TAG, "Interaction mode Start() " + CONTENT_PROVIDER_CLASSNAME + " is null");
                return;
            }
            contentProvider = ajc.CallStatic<AndroidJavaObject>("getInstance");
            if (contentProvider != null)
            {
                readinteractionModeInitValue = readinteraction_value();
                readgazeTriggermodeInitValue = readgazeTriggermode_value();
                int interactionModevalue = System.Convert.ToInt32(readinteractionModeInitValue);
                int gazeTriggermodevalue = System.Convert.ToInt32(readgazeTriggermodeInitValue);
                _interactionModeinit = interactionModevalue;
                _gazeTriggermodeInit = gazeTriggermodevalue;
            }
            else
            {
                Log.e(LOG_TAG, "Interaction mode Start() could NOT get instance of " + CONTENT_PROVIDER_CLASSNAME);
            }

            Log.d(LOG_TAG, "Interaction mode Start() : interaction mode init : " + CONTENT_PROVIDER_CLASSNAME + _interactionModeinit + "Interaction mode Start() : gazeTrigger mode init : " + CONTENT_PROVIDER_CLASSNAME + _gazeTriggermodeInit);
        }
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            return;
        }
#endif
        //WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.SWIPE_EVENT, onSwipeEvent);
        writeinteractionvalue(_interactionModeinit);
        writeGazeTriggermodevalue(_gazeTriggermodeInit);
        Log.w(LOG_TAG, "OnDisable, _interactionModeinit = " + _interactionModeinit + " _gazeTriggermodeInit = " + _gazeTriggermodeInit);
    }

    public void changetoControllerMode() {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            return;
        }
        else
#endif
        {
            writeinteractionvalue(3);
            isGazeMode = false;
        }
    }

    public void changetoGazeMode() {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            return;
        }
        else
#endif
        {
            writeinteractionvalue(2);
            isGazeMode = true;
        }
    }

    public void gaze_timeoutMode()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            return;
        }
        else
#endif
        {
            writeGazeTriggermodevalue(1);
        }
    }

    public void gaze_buttonMode() {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            return;
        }
        else
#endif
        {
            writeGazeTriggermodevalue(2);
        }
    }

    public void gaze_TimeoutandButtonMode() {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            return;
        }
        else
#endif
        {
            writeGazeTriggermodevalue(3);
        }
    }

    // Update is called once per frame
    void Update () {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            return;
        }
#endif
    }
    public string readinteraction_value()
    {
        return contentProvider.Call<string>("readInteractionModeValue");
    }

    public string readgazeTriggermode_value()
    {
        return contentProvider.Call<string>("readGazeTriggerModeValue");
    }

    public void writeinteractionvalue(int value)
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            return;
        }
        else
#endif
        {
            if (isGazeMode == true)
            {
                if (pmInstance != null)
                {
                    Log.d(LOG_TAG, "isPermissionGranted(vive.wave.vr.oem.data.OEMDataWrite) = " + pmInstance.isPermissionGranted("vive.wave.vr.oem.data.OEMDataWrite"));
                    Log.d(LOG_TAG, "isPermissionGranted(vive.wave.vr.oem.data.OEMDataRead) = " + pmInstance.isPermissionGranted("vive.wave.vr.oem.data.OEMDataRead"));
                    //Log.d(LOG_TAG, "isPermissionGranted(com.htc.vr.core.server.VRDataProvider) = " + pmInstance.isPermissionGranted("com.htc.vr.core.server.VRDataProvider"));
                }

                    AndroidJavaClass ajc = new AndroidJavaClass(CONTENT_PROVIDER_CLASSNAME);
                if (ajc == null)
                {
                    Log.e(LOG_TAG, "writeinteractionvalue() " + CONTENT_PROVIDER_CLASSNAME + " is null");
                    return;
                }
                contentProvider = ajc.CallStatic<AndroidJavaObject>("getInstance");
                if (contentProvider != null)
                {
                    string valueStr = value.ToString();
                    Log.d(LOG_TAG, "writeinteractionvalue() got instance of " + CONTENT_PROVIDER_CLASSNAME + ", change Interaction mode to " + valueStr);
                    contentProvider.Call("writeInteractionModeValue", valueStr);
                }
                else
                {
                    Log.e(LOG_TAG, "writeinteractionvalue() could NOT get instance of " + CONTENT_PROVIDER_CLASSNAME);
                }
            }
        }
    }

    public void writeGazeTriggermodevalue(int value)
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            return;
        }
        else
#endif
        {
            if (isGazeMode == true)
            {
                if (pmInstance != null)
                {
                    Log.d(LOG_TAG, "isPermissionGranted(vive.wave.vr.oem.data.OEMDataWrite) = " + pmInstance.isPermissionGranted("vive.wave.vr.oem.data.OEMDataWrite"));
                    Log.d(LOG_TAG, "isPermissionGranted(vive.wave.vr.oem.data.OEMDataRead) = " + pmInstance.isPermissionGranted("vive.wave.vr.oem.data.OEMDataRead"));
                    //Log.d(LOG_TAG, "isPermissionGranted(com.htc.vr.core.server.VRDataProvider) = " + pmInstance.isPermissionGranted("com.htc.vr.core.server.VRDataProvider"));
                }

                AndroidJavaClass ajc = new AndroidJavaClass(CONTENT_PROVIDER_CLASSNAME);
                if (ajc == null)
                {
                    Log.e(LOG_TAG, "write GazeTrigger mode value() " + CONTENT_PROVIDER_CLASSNAME + " is null");
                    return;
                }
                contentProvider = ajc.CallStatic<AndroidJavaObject>("getInstance");
                if (contentProvider != null)
                {
                    string valueStr = value.ToString();
                    Log.d(LOG_TAG, "write GazeTrigger mode value()  got instance of " + CONTENT_PROVIDER_CLASSNAME + ", change Gaze Trigger mode to " + valueStr);
                    contentProvider.Call("writeGazeTriggerModeValue", valueStr);
                }
                else
                {
                    Log.e(LOG_TAG, "write Gaze Trigger Mode Value() could NOT get instance of " + CONTENT_PROVIDER_CLASSNAME);
                }
            }
        }
    }

    public static void requestDoneCallback(List<WaveVR_PermissionManager.RequestResult> results)
    {
        Log.d(LOG_TAG, "requestDoneCallback, count = " + results.Count);
        isDeny = false;

        foreach (WaveVR_PermissionManager.RequestResult p in results)
        {
            Log.d(LOG_TAG, "requestDoneCallback " + p.PermissionName + ": " + (p.Granted ? "Granted" : "Denied"));
            if (!p.Granted)
            {
                isDeny = true;
            }
        }

        if (isDeny)
        {
            if (retryCount++ < RETRY_LIMIT)
            {
                Log.d(LOG_TAG, "Permission denied, retry count = " + retryCount);
                requested = false;
            }
            else
            {
                Log.w(LOG_TAG, "Permission denied, exceed RETRY_LIMIT and skip request");
            }
        }
    }

    void SetOverrideDefault(bool value)
    {
        Log.d(LOG_TAG, "SetOverrideDefault: " + value);
        if (WaveVR_InputModuleManager.Instance != null)
        {
            WaveVR_InputModuleManager.Instance.OverrideSystemSettings = value;
            Log.d(LOG_TAG, "WaveVR_InputModuleManager.Instance.OverrideSystemSettings = " + value);
        }
    }
}
