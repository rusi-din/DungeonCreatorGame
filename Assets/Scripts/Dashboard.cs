using UnityEngine;

public class Dashboard : MonoBehaviour
{
    public void createNewDungeon()
    {
        MapData.getInstance().clearData();
        _ = SceneUtility.LoadAndCleanSceneAsync(SceneUtility.ScenesTo.Creator);
    }
}
