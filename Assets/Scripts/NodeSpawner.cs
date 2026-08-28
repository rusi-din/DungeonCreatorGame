using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    public GameObject nodePrefab;
    public TMPro.TMP_InputField xValueInput;
    public TMPro.TMP_InputField yValueInput;

    public Vector3 spawnOffset = new Vector3(0, 0, 0);

    private static GameObject[,] nodes;

    public static GameObject[,] GetNodes { get { return nodes; } }

    void Start()
    {
        MapData sceneSettings = MapData.getInstance();
        if (sceneSettings.x > 0 && sceneSettings.y > 0)
        {
            SpawnNode(sceneSettings.x, sceneSettings.y);
            if (sceneSettings.nodeTags != null && sceneSettings.nodeTags.GetLength(0) == sceneSettings.x && sceneSettings.nodeTags.GetLength(1) == sceneSettings.y)
            {
                for (int i = 0; i < sceneSettings.x; i++)
                {
                    for (int j = 0; j < sceneSettings.y; j++)
                    {
                        GameObject node = nodes[i, j];
                        NodeTagChanger.NodeTags tag = NodeTagChanger.getTag(sceneSettings.nodeTags[i, j]);
                        node.GetComponent<Node>().SetTagAndColor(tag);
                    }
                }
            }
        }
        else
        {
            nodes = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnNode()
    {
        SpawnNode(int.Parse(xValueInput.text), int.Parse(yValueInput.text));

    }

    public void SpawnNode(int x, int y)
    {
        if (nodes != null)
        {
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    Destroy(nodes[i, j]);
                }
            }
        }
        nodes = new GameObject[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject newNode = Instantiate(nodePrefab, new Vector3(i * 4, 0, j * 4) + spawnOffset, Quaternion.identity);

                newNode.tag = "node";
                newNode.name = $"Node_{i}_{j}";

                nodes[i, j] = newNode;

            }
        }
        MapData sceneSettings = MapData.getInstance();
        sceneSettings.x = x;
        sceneSettings.y = y;
    }

    public static GameObject getNode(int x, int y)
    {
        return nodes[x, y];
    }

    public static void setNode(int x, int y, GameObject node)
    {
        nodes[x, y] = node;
    }
}
