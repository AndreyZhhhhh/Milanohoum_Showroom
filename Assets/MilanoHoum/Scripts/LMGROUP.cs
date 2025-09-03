using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class LMGROUP : MonoBehaviour
{
    // �������� ����� ��� light maps
    private const string lightMapLabel = "LMGROUP"; // ���������, ��� ��� ������������� �����, ������� �� ������������

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
        // ��������� ��� light maps � ������ LMGROUP
        loadHandle = Addressables.LoadAssetsAsync<Texture2D>(lightMapLabel, null);

        // �������� ���������� ��������
        yield return loadHandle;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            // ��������� ����������� light maps � ������
            loadedLightMaps.AddRange(loadHandle.Result);
            Debug.Log("��� light maps ������� ��������� � ����������.");

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
            Debug.LogError("�� ������� ��������� light maps!");
        }
    }

    private void OnDestroy()
    {
        // �� ����������� ������, ��� ��� ��� ����� �� ���������� ���� ����
    }
}
