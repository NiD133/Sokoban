using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject mapParent;
    public GameObject wallPrefab;
    public GameObject boxPrefab;
    public GameObject targetPrefab;
    public GameObject playerPrefab;
    public GameObject groundPrefab;

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private int[,] levelMap1 = {
        {1, 1, 1, 1, 1, 1, 1},
        {1, 4, 3, 0, 0, 0, 1},
        {1, 0, 2, 2, 3, 0, 1},
        {1, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1}
    };

    public void GenerateLevel(int LevelNumber)
    {
        int[,] levelMap = levelMap1;
        Debug.Log($"Current level is {LevelNumber}");
        relocateCamera();

        for (int y = 0; y < levelMap.GetLength(0); y++)
        {
            for (int x = 0; x < levelMap.GetLength(1); x++)
            {
                Vector2 position = new Vector2(x, -y);

                if (levelMap[y, x] != -1)
                {
                    GameObject ground = Instantiate(groundPrefab, position, Quaternion.identity);
                    ground.transform.position = new Vector3(x, -y, 1);
                    instantiatedObjects.Add(ground);
                }

                if (levelMap[y, x] == 1) // Wall
                {
                    GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
                    instantiatedObjects.Add(wall);
                }
                else if (levelMap[y, x] == 2) // Box
                {
                    GameObject box = Instantiate(boxPrefab, position, Quaternion.identity);
                    instantiatedObjects.Add(box);
                }
                else if (levelMap[y, x] == 3) // TargetPoint
                {
                    GameObject target = Instantiate(targetPrefab, position, Quaternion.identity);
                    instantiatedObjects.Add(target);
                }
                else if (levelMap[y, x] == 4) // Player
                {
                    GameObject player = Instantiate(playerPrefab, position, Quaternion.identity);
                    instantiatedObjects.Add(player);
                }
            }
        }
    }

    public void ClearLevel()
    {
        foreach (GameObject obj in instantiatedObjects)
        {
            Destroy(obj);
        }
        instantiatedObjects.Clear();
    }

    private Vector3 relocateCamera()
    {
        int mapHeight = levelMap1.GetLength(0);
        int mapWidth = levelMap1.GetLength(1);
        float centerX = (mapWidth - 1) / 2f;
        float centerY = (mapHeight - 1) / 2f;
        return Camera.main.transform.position = new Vector3(centerX, -centerY, -10);
    }
}
