using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wvr;
using WaveVR_Log;

public class MotionControllerTest : MonoBehaviour
{
    private const string LOG_TAG = "MotionControllerTest";
    private void PrintDebugLog(string msg)
    {
        #if UNITY_EDITOR
        Debug.Log(LOG_TAG + " " + msg);
        #endif
        Log.d (LOG_TAG, msg);
    }

    // Use this for initialization
    void Start ()
    {
    }

    private GameObject rightController = null, leftController = null;
    // Update is called once per frame
    void Update ()
    {
        this.rightController = WaveVR_EventSystemControllerProvider.Instance.GetControllerModel (WVR_DeviceType.WVR_DeviceType_Controller_Right);
        this.leftController = WaveVR_EventSystemControllerProvider.Instance.GetControllerModel (WVR_DeviceType.WVR_DeviceType_Controller_Left);
    }

    public void SimulatePose()
    {
        WaveVR_ControllerPoseTracker _cpt = null;
        if (this.rightController != null)
        {
            _cpt = this.rightController.GetComponent<WaveVR_ControllerPoseTracker> ();
            if (_cpt != null)
            {
                PrintDebugLog ("SimulatePose() simulate right.");
                _cpt.SimulationOption = WVR_SimulationOption.ForceSimulation;
            }
        }
        if (this.leftController != null)
        {
            _cpt = this.leftController.GetComponent<WaveVR_ControllerPoseTracker> ();
            if (_cpt != null)
            {
                PrintDebugLog ("SimulatePose() simulate left.");
                _cpt.SimulationOption = WVR_SimulationOption.ForceSimulation;
            }
        }
    }

    public void RealPose()
    {
        WaveVR_ControllerPoseTracker _cpt = null;
        if (this.rightController != null)
        {
            _cpt = this.rightController.GetComponent<WaveVR_ControllerPoseTracker> ();
            if (_cpt != null)
            {
                PrintDebugLog ("RealPose() real right.");
                _cpt.SimulationOption = WVR_SimulationOption.NoSimulation;
            }
        }
        if (this.leftController != null)
        {
            _cpt = this.leftController.GetComponent<WaveVR_ControllerPoseTracker> ();
            if (_cpt != null)
            {
                PrintDebugLog ("RealPose() real left.");
                _cpt.SimulationOption = WVR_SimulationOption.NoSimulation;
            }
        }
    }

    public void FollowHMD()
    {
        WaveVR_ControllerPoseTracker _cpt = null;
        if (this.rightController != null)
        {
            _cpt = this.rightController.GetComponent<WaveVR_ControllerPoseTracker> ();
            if (_cpt != null)
            {
                PrintDebugLog ("FollowHMD() right.");
                _cpt.FollowHead = true;
            }
        }
        if (this.leftController != null)
        {
            _cpt = this.leftController.GetComponent<WaveVR_ControllerPoseTracker> ();
            if (_cpt != null)
            {
                PrintDebugLog ("FollowHMD() left.");
                _cpt.FollowHead = true;
            }
        }
    }

    public void NoFollowHMD()
    {
        WaveVR_ControllerPoseTracker _cpt = null;
        if (this.rightController != null)
        {
            _cpt = this.rightController.GetComponent<WaveVR_ControllerPoseTracker> ();
            if (_cpt != null)
            {
                PrintDebugLog ("NoFollowHMD() right.");
                _cpt.FollowHead = false;
            }
        }
        if (this.leftController != null)
        {
            _cpt = this.leftController.GetComponent<WaveVR_ControllerPoseTracker> ();
            if (_cpt != null)
            {
                PrintDebugLog ("NoFollowHMD() left.");
                _cpt.FollowHead = false;
            }
        }
    }

    public void ShowPointer()
    {
        WaveVR_ControllerPointer _cp = null;
        if (this.rightController != null)
        {
            _cp = this.rightController.GetComponentInChildren<WaveVR_ControllerPointer> ();
            if (_cp != null)
            {
                PrintDebugLog ("ShowPointer() right.");
                _cp.ShowPointer = true;
            }
        }
        if (this.leftController != null)
        {
            _cp = this.leftController.GetComponentInChildren<WaveVR_ControllerPointer> ();
            if (_cp != null)
            {
                PrintDebugLog ("ShowPointer() left.");
                _cp.ShowPointer = true;
            }
        }
    }

    public void HidePointer()
    {
        WaveVR_ControllerPointer _cp = null;
        if (this.rightController != null)
        {
            _cp = this.rightController.GetComponentInChildren<WaveVR_ControllerPointer> ();
            if (_cp != null)
            {
                PrintDebugLog ("HidePointer() right.");
                _cp.ShowPointer = false;
            }
        }
        if (this.leftController != null)
        {
            _cp = this.leftController.GetComponentInChildren<WaveVR_ControllerPointer> ();
            if (_cp != null)
            {
                PrintDebugLog ("HidePointer() left.");
                _cp.ShowPointer = false;
            }
        }
    }
}
