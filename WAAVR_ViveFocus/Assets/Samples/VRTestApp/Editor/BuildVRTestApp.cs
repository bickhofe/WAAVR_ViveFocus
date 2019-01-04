// "WaveVR SDK
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEditor;

public class BuildVRTestApp
{
    private static void GeneralSettings()
    {
        PlayerSettings.Android.bundleVersionCode = 1;
        PlayerSettings.bundleVersion = "2.0.0";
        PlayerSettings.companyName = "HTC Corp.";
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel22;
    }

    [UnityEditor.MenuItem("WaveVR/Build Android APK/Build VRTestAppUnity.apk")]
    static void BuildApk()
    {
        BuildApk(null, false);
    }

    [UnityEditor.MenuItem("WaveVR/Build Android APK/Build+Run VRTestAppUnity.apk")]
    static void BuildAndRunApk()
    {
        BuildApk(null, true);
    }

    public static void BuildApk(string destPath, bool run)
    {
        string[] levels = {
                "Assets/Samples/VRTestApp/scenes/VRTestApp.unity",
                "Assets/Samples/SeaOfCube/scenes/Main.unity",
                "Assets/Samples/SeaOfCube/scenes/Main_Help.unity",
                "Assets/Samples/SeaOfCube/scenes/SeaOfCubeWithTwoHead.unity",
                "Assets/Samples/SeaOfCube/scenes/SeaOfCubeWithTwoHead_Help.unity",
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
                "Assets/Samples/Resource2_Test/Scenes/Resource_Test1_Help.unity",
                "Assets/Samples/Resource2_Test/Scenes/Resource_Test2.unity",
                "Assets/Samples/Resource2_Test/Scenes/Resource_Test2_Help.unity"
            };
        BuildApkInner("VRTestApp", "VRTestAppUnity.apk", destPath, run, levels);
    }

    private static void BuildApkInner(string idName, string apkName, string destPath, bool run, string[] levels)
    {
        GeneralSettings();

        PlayerSettings.productName = "VRTestApp";
#if UNITY_5_6_OR_NEWER
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.vrm.unity." + idName);
#else
        PlayerSettings.bundleIdentifier = "com.vrm.unity.VRTestApp";
#endif
        Texture2D icon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Samples/VRTestApp/Textures/test.png", typeof(Texture2D));
        if (icon == null)
            Debug.LogError("Fail to read app icon");

        Texture2D[] group = { icon, icon, icon, icon, icon, icon };

        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Android, group);
        PlayerSettings.gpuSkinning = false;
#if UNITY_2017_2_OR_NEWER
        PlayerSettings.SetMobileMTRendering(BuildTargetGroup.Android, true);
#else
        PlayerSettings.mobileMTRendering = true;
#endif
        PlayerSettings.graphicsJobs = true;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel25;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel25;

        string outputFilePath = string.IsNullOrEmpty(destPath) ? apkName : destPath + "/" + apkName;
        BuildPipeline.BuildPlayer(levels, outputFilePath, BuildTarget.Android, run ? BuildOptions.AutoRunPlayer : BuildOptions.None);
    }
}
