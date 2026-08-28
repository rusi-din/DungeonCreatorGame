using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneUtility
{
    public enum ScenesTo
    {
        Dungeon,
        Dashboard,
        Creator,
        Explorer,
    }

    public static async Task LoadAndCleanSceneAsync(ScenesTo sceneName)
    {
        // 1. Load the scene asynchronously
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Single);

        // Wait until loading finishes
        while (!loadOperation.isDone)
        {
            await Task.Yield();
        }

        // 2. Clear out unreferenced assets from RAM
        AsyncOperation unloadOperation = Resources.UnloadUnusedAssets();
        while (!unloadOperation.isDone)
        {
            await Task.Yield();
        }

        // 3. Force garbage collection
        GC.Collect();

        Debug.Log($"Successfully transitioned to {sceneName} and cleared RAM.");
    }
}