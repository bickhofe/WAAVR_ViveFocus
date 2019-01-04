using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WaveVR_Log;

public class MasterSceneManager : MonoBehaviour
{
//    private static string LOG_TAG = "MasterSceneManager";
    public static Stack previouslevel;
    public static MasterSceneManager Instance;
    public static GameObject bs, hs;


    private static string[] scenes = new string[] { "Main",
                                                    "CameraTexture_Test",
                                                    "PermissionMgr_Test",
                                                    "MotionController_Test",
                                                    "ControllerInputMode_Test",
                                                    "MixedInputModule_Test",
                                                    "InAppRecenter",
                                                    "Battery_Test",
                                                    "VirtualWall_Test",
                                                    "PassengerMode_Test",
                                                    "InteractionMode_Test",
                                                    "Resource_Test1",
                                                    "Resource_Test2"};
    private static string[] scene_names = new string[] { "SeaOfCubes",
                                                         "CameraTexture Test",
                                                         "PermissionMgr Test",
                                                         "MotionController Test",
                                                         "ControllerInputMode Test",
                                                         "MixedInputModule Test",
                                                         "InAppRecenter Test",
                                                         "Battery Test",
                                                         "VirtualWall Test",
                                                         "PassengerMode Test",
                                                         "InteractionMode Test",
                                                         "Resource Test1",
                                                         "Resource Test2"};
    private static string[] scene_paths = new string[] { "Assets/Samples/SeaOfCube/scenes/Main.unity",
                                                         "Assets/Samples/CameraTexture_Test/scenes/CameraTexture_Test.unity",
                                                         "Assets/Samples/PermissionMgr_Test/scenes/PermissionMgr_Test.unity",
                                                         "Assets/Samples/MotionController_Test/Scenes/MotionController_Test.unity",
                                                         "Assets/Samples/ControllerInputMode_Test/ControllerInputMode_Test.unity",
                                                         "Assets/Samples/ControllerInputModule_Test/scenes/MixedInputModule_Test.unity",
                                                         "Assets/Samples/InAppRecenter_Test/scene/InAppRecenter.unity",
                                                         "Assets/Samples/Battery_Test/Scenes/Battery_Test.unity",
                                                         "Assets/Samples/VirtualWall_Test/scenes/VirtualWall_Test.unity",
                                                         "Assets/Samples/PassengerMode_Test/scenes/PassengerMode_Test.unity",
                                                         "Assets/Samples/InteractionMode_Test/scene/InteractionMode_Test.unity",
                                                         "Assets/Samples/Resource2_Test/Scenes/Resource_Test1.unity",
                                                         "Assets/Samples/Resource2_Test/Scenes/Resource_Test2.unity"};

    private static int scene_idx = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
            previouslevel = new Stack();
            bs = GameObject.Find("BackSphere");
            if (bs != null)
            {
                DontDestroyOnLoad(bs);
                bs.SetActive(false);
            }
            hs = GameObject.Find("HelpSphere");
            if (hs != null)
            {
                DontDestroyOnLoad(hs);
                hs.SetActive(false);
            }
        }
        else
        {
            previouslevel.Clear();
            if (bs != null)
                bs.SetActive (false);
            if (hs != null)
                hs.SetActive (false);
            GameObject dd = GameObject.Find("BackSphere");
            if (dd != null)
                dd.SetActive (false);
            dd = GameObject.Find("HelpSphere");
            if (dd != null)
                dd.SetActive (false);
        }

        GameObject ts = GameObject.Find("SceneText");
        if (ts != null)
        {
            Text sceneText = ts.GetComponent<Text>();
            if (sceneText != null)
            {
                sceneText.text = scene_idx + ", " + scene_names[scene_idx];
            }
        }
    }

    public void ChangeToNext()
    {
        scene_idx++;

        if (scene_idx >= scenes.Length)
            scene_idx = 0;

        GameObject ts = GameObject.Find("SceneText");
        if (ts != null)
        {
            Text sceneText = ts.GetComponent<Text>();
            if (sceneText != null)
            {
                sceneText.text = scene_idx + ", " + scene_names[scene_idx];
            }
        }
    }

    public void ChangeToPrevious()
    {
        scene_idx--;

        if (scene_idx < 0)
            scene_idx = scenes.Length - 1 ;

        GameObject ts = GameObject.Find("SceneText");
        if (ts != null)
        {
            Text sceneText = ts.GetComponent<Text>();
            if (sceneText != null)
            {
                sceneText.text = scene_idx + ", " + scene_names[scene_idx];
            }
        }
    }

    public void LoadPrevious()
    {
        if (previouslevel.Count > 0)
        {
            string scene_name = previouslevel.Pop().ToString();
            if (previouslevel.Count != 0)
            {
                hs.SetActive (true);
            }
            SceneManager.LoadScene(scene_name);
        }
    }

    public void LoadScene()
    {
        string scene = scenes[scene_idx];
        string scene_path = scene_paths[scene_idx];
        bs.SetActive (true);
        LoadNext(scene, scene_path);
    }

    public void loadHelpScene()
    {
        string help_scene = SceneManager.GetActiveScene().name + "_Help";
        LoadNext(help_scene, "");
    }

    private void LoadNext(string scene, string scene_path)
    {
        previouslevel.Push(SceneManager.GetActiveScene().name);
        if (scene_path.Length > 6)
        {
            scene_path = scene_path.Remove(scene_path.Length - 6);
            scene_path += "_Help.unity";
            if (SceneUtility.GetBuildIndexByScenePath(scene_path) != -1)
            {
                hs.SetActive (true);
            }
            else
            {
                hs.SetActive (false);
            }
        }
        else
        {
            hs.SetActive (false);
        }
        SceneManager.LoadScene(scene);
    }

    public void ChooseQuit()
    {
        Application.Quit();
    }
}
