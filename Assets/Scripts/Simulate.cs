// using System.Text.RegularExpressions;
// using UnityEngine;

// public class Simulate : MonoBehaviour
// {
//     public void letsGo()
//     {
//         // foreach (GameObject node in NodeSpawner.GetNodes)
//         // {

//         // }

//         GameObject[] startPoint = GameObject.FindGameObjectsWithTag(NodeTagChanger.getTag(2).ToString());

//         if (startPoint.Length > 1)
//         {
//             //Sends a popup
//             return;
//         }
//         GameObject formerStep;



//         Match match = Regex.Match(startPoint[0].name, @"^Node_(?<i>\d+)_(?<j>\d+)$");

//         if (match.Success)
//         {
//             int i = int.Parse(match.Groups["i"].Value);
//             int j = int.Parse(match.Groups["j"].Value);

//             Debug.Log($"Start Point: i={i}, j={j}");


//         }
//         else
//         {
//             Debug.LogError("Node name does not match the expected pattern.");
//         }


//         // foreach (GameObject node in NodeSpawner.GetNodes)
//         // {
//         //     if (node.CompareTag(NodeTagChanger.getTag(2).ToString()))
//         //     {

//         //     }  
//         // }
//     }
// }


using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;



public class Simulate : MonoBehaviour
{
    private HashSet<(int, int)> visited = new HashSet<(int, int)>();
    private List<GameObject> finalPath = new List<GameObject>();

    public void letsGo()
    {
        // 1. Find the starting point
        GameObject[] startPoint = GameObject.FindGameObjectsWithTag(NodeTagChanger.getTag(2).ToString());

        if (startPoint.Length == 0)
        {
            Debug.LogError("No Start Point found on the grid!");
            return;
        }
        if (startPoint.Length > 1)
        {
            Debug.LogError("Multiple Start Points detected! Please keep only one.");
            return;
        }

        // 2. Parse the coordinates of the starting point using your Regex
        Match match = Regex.Match(startPoint[0].name, @"^Node_(?<i>\d+)_(?<j>\d+)$");
        if (!match.Success)
        {
            Debug.LogError("Start Node name does not match the expected pattern.");
            return;
        }

        int startI = int.Parse(match.Groups["i"].Value);
        int startJ = int.Parse(match.Groups["j"].Value);

        // 3. Clear data from any previous simulations
        visited.Clear();
        finalPath.Clear();

        // 4. Run the Backtracking DFS
        GameObject[,] grid = NodeSpawner.GetNodes;
        if (grid == null)
        {
            Debug.LogError("Grid has not been spawned yet!");
            return;
        }

        if (DFS(startI, startJ, grid))
        {
            Debug.Log($"Path found! Total steps in correct route: {finalPath.Count}");

            // Optional: Highlight the successful path in the editor
            // foreach (GameObject pathNode in finalPath)
            // {
            //     // Avoid recoloring the exact start/end nodes if you want to keep them green/red
            //     if (pathNode.CompareTag("Route"))
            //     {
            //         pathNode.GetComponent<Renderer>().material.color = Color.yellow; 
            //     }
            // }

            MapData sceneSettings = MapData.getInstance();
            sceneSettings.setData(grid.GetLength(0), grid.GetLength(1), startPoint[0].name, finalPath[finalPath.Count - 1].name, finalPath.ConvertAll(node => node.name).ToArray());
            sceneSettings.nodeTags = new int[grid.GetLength(0), grid.GetLength(1)];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    sceneSettings.nodeTags[i, j] = GetTagId(grid[i, j].tag);
                }
            }

            _ = SceneUtility.LoadAndCleanSceneAsync(SceneUtility.ScenesTo.Dungeon);
        }
        else
        {
            Debug.LogWarning("No valid route exists between the Start Point and End Point.");
        }
    }

    private bool DFS(int i, int j, GameObject[,] grid)
    {
        int maxI = grid.GetLength(0);
        int maxJ = grid.GetLength(1);

        // Boundary check
        if (i < 0 || i >= maxI || j < 0 || j >= maxJ) return false;

        // Visited check to prevent infinite loops from cyclic false positives
        if (visited.Contains((i, j))) return false;

        GameObject currentNode = grid[i, j];
        string endTag = NodeTagChanger.getTag(3).ToString();   // "EndPoint"
        string routeTag = NodeTagChanger.getTag(4).ToString(); // "Route"
        string startTag = NodeTagChanger.getTag(2).ToString(); // "StartPoint"

        // Rule Check: We can only step on Start, Route, or End points
        if (!currentNode.CompareTag(startTag) && !currentNode.CompareTag(routeTag) && !currentNode.CompareTag(endTag))
        {
            return false;
        }

        // Mark current node as visited and add to potential path
        visited.Add((i, j));
        finalPath.Add(currentNode);

        // SUCCESS CONDITION: We reached the EndPoint!
        if (currentNode.CompareTag(endTag))
        {
            return true;
        }

        // 4 Cardinal Movements: Up, Down, Left, Right
        int[] di = { -1, 1, 0, 0 };
        int[] dj = { 0, 0, -1, 1 };

        for (int d = 0; d < 4; d++)
        {
            int nextI = i + di[d];
            int nextJ = j + dj[d];

            // Recurse into neighbor
            if (DFS(nextI, nextJ, grid))
            {
                return true; // Propagation of success up the call stack
            }
        }

        // BACKTRACKING: If none of the 4 directions found the goal, 
        // this node is a false positive (dead end). Remove it from our route.
        finalPath.RemoveAt(finalPath.Count - 1);
        return false;
    }

    private int GetTagId(string tagName)
    {
        if (tagName == NodeTagChanger.getTag(2).ToString())
        {
            return 2;
        }

        if (tagName == NodeTagChanger.getTag(3).ToString())
        {
            return 3;
        }

        if (tagName == NodeTagChanger.getTag(4).ToString())
        {
            return 4;
        }

        return 1;
    }
}