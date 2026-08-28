using UnityEngine;

public class SetTheScene : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject roadPrefab;
    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject playerPrefab;

    void Start()
    {
        create();
    }

    void create()
    {
        MapData sceneSettings = MapData.getInstance();
        Debug.Log($"Scene Settings: x={sceneSettings.x}, y={sceneSettings.y}, startPoint={sceneSettings.startPoint}, endPoint={sceneSettings.endPoint}, route=[{string.Join(", ", sceneSettings.route)}]");

        NodeSpawner spawner = new NodeSpawner();
        spawner.nodePrefab = nodePrefab;
        spawner.spawnOffset = new Vector3(0, 2, 0);
        spawner.SpawnNode(sceneSettings.x, sceneSettings.y);

        Destroy(NodeSpawner.getNode(int.Parse(sceneSettings.startPoint.Split('_')[1]), int.Parse(sceneSettings.startPoint.Split('_')[2])));
        GameObject startNode = Instantiate(startPrefab, new Vector3(int.Parse(sceneSettings.startPoint.Split('_')[1]) * 4, 0.15f, int.Parse(sceneSettings.startPoint.Split('_')[2]) * 4), Quaternion.identity);
        startNode.name = sceneSettings.startPoint;
        startNode.tag = NodeTagChanger.getTag(2).ToString();
        NodeSpawner.setNode(int.Parse(sceneSettings.startPoint.Split('_')[1]), int.Parse(sceneSettings.startPoint.Split('_')[2]), startNode);

        Destroy(NodeSpawner.getNode(int.Parse(sceneSettings.endPoint.Split('_')[1]), int.Parse(sceneSettings.endPoint.Split('_')[2])));
        GameObject endNode = Instantiate(endPrefab, new Vector3(int.Parse(sceneSettings.endPoint.Split('_')[1]) * 4, 0.15f, int.Parse(sceneSettings.endPoint.Split('_')[2]) * 4), Quaternion.identity);
        endNode.name = sceneSettings.endPoint;
        endNode.tag = NodeTagChanger.getTag(3).ToString();
        NodeSpawner.setNode(int.Parse(sceneSettings.endPoint.Split('_')[1]), int.Parse(sceneSettings.endPoint.Split('_')[2]), endNode);

        string startPoint = sceneSettings.startPoint;
        string endPoint = sceneSettings.endPoint;

        for (int i = 0; i < sceneSettings.route.Length; i++)
        {
            if (sceneSettings.route[i] == startPoint || sceneSettings.route[i] == endPoint)
            {
                continue;
            }

            Destroy(NodeSpawner.getNode(int.Parse(sceneSettings.route[i].Split('_')[1]), int.Parse(sceneSettings.route[i].Split('_')[2])));
            GameObject road = Instantiate(roadPrefab, new Vector3(int.Parse(sceneSettings.route[i].Split('_')[1]) * 4, 0.15f, int.Parse(sceneSettings.route[i].Split('_')[2]) * 4), Quaternion.identity);
            road.name = sceneSettings.route[i];
            road.tag = NodeTagChanger.getTag(4).ToString();
            NodeSpawner.setNode(int.Parse(sceneSettings.route[i].Split('_')[1]), int.Parse(sceneSettings.route[i].Split('_')[2]), road);
        }

        GameObject player = Instantiate(playerPrefab, startNode.transform.position + new Vector3(0.25f, 0.70f, 0), Quaternion.identity);
        Camera.main.transform.position = startNode.transform.position + new Vector3(0.25f, 0.70f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goBack()
    {
        _ = SceneUtility.LoadAndCleanSceneAsync(SceneUtility.ScenesTo.Creator);
    }
}
