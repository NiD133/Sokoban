using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add test

public class LevelGenerator : MonoBehaviour
{
    public GameObject mapParent;
    public GameObject wallPrefab;
    public GameObject boxPrefab;
    public GameObject targetPrefab;
    public GameObject playerPrefab;
    public GameObject groundPrefab;

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private int[,] levelMap = {
        {1, 1, 1, 1, 1, 1, 1},
        {1, 4, 3, 0, 0, 0, 1},
        {1, 0, 2, 2, 3, 0, 1},
        {1, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1}
    };

    public void GenerateLevel()
    {
        ClearLevel();
        relocateCamera();
        for (int y = 0; y < levelMap.GetLength(0); y++)
        {
            for (int x = 0; x < levelMap.GetLength(1); x++)
            {
                Vector2 position = new Vector2(x, -y); 
                Instantiate(groundPrefab, position, Quaternion.identity).transform.position = new Vector3(x, -y, 1);
                if (levelMap[y, x] == 1) // Wall
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else if (levelMap[y, x] == 2) // Box
                {
                    Instantiate(boxPrefab, position, Quaternion.identity);
                }
                else if (levelMap[y, x] == 3) // TargetPoint
                {
                    Instantiate(targetPrefab, position, Quaternion.identity);
                }
                else if (levelMap[y, x] == 4) // Player
                {
                    Instantiate(playerPrefab, position, Quaternion.identity);
                }
            }
        }
    }

    private Vector3 relocateCamera()
    {
        int mapHeight = levelMap.GetLength(0);
        int mapWidth = levelMap.GetLength(1);
        float centerX = (mapWidth - 1) / 2f;
        float centerY = (mapHeight - 1) / 2f;
        return Camera.main.transform.position = new Vector3(centerX, -centerY, -10);
    }

    private void ClearLevel()
    {
        foreach (GameObject obj in instantiatedObjects)
        {
            Destroy(obj);
        }
        instantiatedObjects.Clear();
    }
}
