using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public static class BuildScript
{
    // Opcional: menu para testar no Editor
    // [MenuItem("Build/CI/Build iOS")]
    public static void BuildiOS()
    {
        // Caminho de saída (use aspas retas)
        var buildPath = Path.Combine("build", "ios");

        // Limpa a pasta de build
        if (Directory.Exists(buildPath))
            Directory.Delete(buildPath, true);
        Directory.CreateDirectory(buildPath);

        // Player Settings (ajuste para o seu app/Bundle ID)
        PlayerSettings.productName = "SeuAppName";                 // TODO
        PlayerSettings.applicationIdentifier = "com.seu.dominio";  // TODO
        PlayerSettings.bundleVersion = "1.0.0";

        // iOS-specific
        PlayerSettings.iOS.buildNumber = "1";
        PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
        PlayerSettings.iOS.targetOSVersionString = "12.0";
        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;

        // API obsoleta trocada:
        // PlayerSettings.iOS.allowHTTPDownload = true;
        PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed; // ou NotAllowed se não precisar HTTP

        // (Recomendado p/ iOS)
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, 1); // 1 = ARM64

        // Cenas habilitadas no Build Settings
        string[] scenes = GetEnabledScenes();
        Debug.Log($"Building iOS with {scenes.Length} scenes → {buildPath}");

        // Build
        BuildReport report = BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.iOS, BuildOptions.None);

        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"✅ Build succeeded. Size: {report.summary.totalSize} bytes");
        }
        else
        {
            Debug.LogError($"❌ Build failed: {report.summary.result}");
            throw new System.Exception("Build failed: " + report.summary.result);
        }
    }

    private static string[] GetEnabledScenes()
    {
        var enabled = EditorBuildSettings.scenes;
        var scenes = new string[enabled.Length];
        for (int i = 0; i < enabled.Length; i++)
            scenes[i] = enabled[i].path;
        return scenes;
    }
}
