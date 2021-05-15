using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundle : MonoBehaviour
{
    [MenuItem("Build Bundle/Build Asset Bundle")]
    public static void BuildAB()
    {
        string assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
            BuildAssetBundleOptions.ChunkBasedCompression,
            EditorUserBuildSettings.activeBuildTarget);

    }
}
