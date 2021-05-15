using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetsBundleManager : MonoBehaviour
{
    AssetBundle assetBundle;

    public GameObject gameObjectPrefab;
    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR
        var prefab = gameObjectPrefab;
#else
        var myLoadedAssetBundle
            = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, "AB/objectbundle"));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Cube");
#endif
        Instantiate(prefab);
        Debug.Log(Application.streamingAssetsPath);
        StartCoroutine(DownLoadAsset());
    }

    public IEnumerator DownLoadAsset()
    {
        string url = "https://bitly.com.vn/2gxj2a";
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "AB/scenes")))
        {
            Uri uri = new Uri(url);

            WebClient client = new WebClient();

            client.DownloadProgressChanged += Client_DownloadProgressChanged;

            client.DownloadFileAsync(uri, Application.persistentDataPath + "/AB/scenes");

            while (client.IsBusy)   // Wait until the file download is complete
                yield return null;
            Debug.Log("download");
        }


        assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, "AB/scenes"));
        var scene = assetBundle.GetAllScenePaths()[0];
        SceneManager.LoadSceneAsync(scene);
    }

    //Create your ProgressChanged "Listener"
    private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        //Show download progress
        Debug.Log("Download Progress: " + e.ProgressPercentage);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
