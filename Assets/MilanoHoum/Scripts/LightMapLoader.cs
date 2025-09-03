using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LightMapLoader : MonoBehaviour
{
    // ��� ������, � ������� ��������� light maps
    private const string lightMapGroupKey = "Lightmaps"; // ���������, ��� ��� ��� ������ ������������� ������

    // ������ ��� �������� ����������� light maps
    private List<Texture2D> loadedLightMaps = new List<Texture2D>();
    private AsyncOperationHandle<IList<Texture2D>> loadHandle;

    void Start()
    {
        // ��������� �������� ��� �������� light maps
        StartCoroutine(LoadLightMaps());
    }

    private IEnumerator LoadLightMaps()
    {
        // ��������� ��� light maps �� ������
        loadHandle = Addressables.LoadAssetsAsync<Texture2D>(lightMapGroupKey, null);

        // �������� ���������� ��������
        yield return loadHandle;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            // ��������� ����������� light maps � ������
            loadedLightMaps.AddRange(loadHandle.Result);
            Debug.Log("all light maps are loaded and cached.");

            // ���������� light maps � LightmapSettings
            LightmapData[] lightmapsData = new LightmapData[loadedLightMaps.Count];
            for (int i = 0; i < loadedLightMaps.Count; i++)
            {
                lightmapsData[i] = new LightmapData();
                lightmapsData[i].lightmapColor = loadedLightMaps[i];
            }
            LightmapSettings.lightmaps = lightmapsData;
        }
        else
        {
            Debug.LogError("can't load lightmaps");
        }
    }

    private void OnDestroy()
    {
        // �� ����������� ������, ��� ��� ��� ����� �� ���������� ���� ����
    }
}

