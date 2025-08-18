using UnityEditor;
using UnityEngine;
using System.IO;

public static class BuildScript
{
    public static void BuildiOS()
    {
        // Configurações do build
        string buildPath = “build/ios”;

        // Limpar pasta de build se existir
        if (Directory.Exists(buildPath))
        {
            Directory.Delete(buildPath, true);
        }
        Directory.CreateDirectory(buildPath);

        // Configurar Player Settings para iOS
        PlayerSettings.productName = "SeuAppName"; // MUDE AQUI
        PlayerSettings.applicationIdentifier = "com.seudominio.seuapp"; // MUDE AQUI
        PlayerSettings.bundleVersion = "1.0.0";

        // Configurações específicas do iOS
        PlayerSettings.iOS.buildNumber = "1";
        PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
        PlayerSettings.iOS.targetOSVersionString = "12.0";
        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;

        // Desabilitar bitcode (Apple descontinuou)
        PlayerSettings.iOS.allowHTTPDownload = true;

        // Pegar todas as cenas habilitadas no Build Settings
        string[] scenes = GetEnabledScenes();

        Debug.Log($"Building for iOS with {scenes.Length} scenes");
        Debug.Log($"Build path: {buildPath}");

        // Executar build
        BuildReport buildReport = BuildPipeline.BuildPlayer(
        scenes,
        buildPath,
        BuildTarget.iOS,
        BuildOptions.None
        );

        // Verificar resultado
        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded!");
            Debug.Log($"Build size: {buildReport.summary.totalSize} bytes");
        }
        else
        {
            Debug.LogError("Build failed!");
            throw new System.Exception("Build failed with result: " + buildReport.summary.result);
        }
    }

    private static string[] GetEnabledScenes()
    {
        var scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }
}