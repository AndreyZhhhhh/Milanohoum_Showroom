using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class LMGROUP : MonoBehaviour
{
    // Название метки для light maps
    private const string lightMapLabel = "LMGROUP"; // Убедитесь, что это соответствует метке, которую вы использовали

    // Список для хранения загруженных light maps
    private List<Texture2D> loadedLightMaps = new List<Texture2D>();
    private AsyncOperationHandle<IList<Texture2D>> loadHandle;

    void Start()
    {
        // Запускаем корутину для загрузки light maps
        StartCoroutine(LoadLightMaps());
    }

    private IEnumerator LoadLightMaps()
    {
        // Загружаем все light maps с меткой LMGROUP
        loadHandle = Addressables.LoadAssetsAsync<Texture2D>(lightMapLabel, null);

        // Ожидание завершения загрузки
        yield return loadHandle;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            // Сохраняем загруженные light maps в список
            loadedLightMaps.AddRange(loadHandle.Result);
            Debug.Log("Все light maps успешно загружены и кэшированы.");

            // Применение light maps к LightmapSettings
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
            Debug.LogError("Не удалось загрузить light maps!");
        }
    }

    private void OnDestroy()
    {
        // Не освобождаем активы, так как они нужны на протяжении всей игры
    }
}
