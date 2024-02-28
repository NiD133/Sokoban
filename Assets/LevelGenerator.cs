using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add test

public class LevelGenerator : MonoBehaviour
{
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
        for (int y = 0; y < levelMap.GetLength(0); y++)
        {
            for (int x = 0; x < levelMap.GetLength(1); x++)
            {
                Vector2 position = new Vector2(x, -y); 
                Instantiate(groundPrefab, position, Quaternion.identity).transform.position = new Vector3(x, -y, 10); // 将地面的z位置设置为10或其他更高的值，以确保它位于其他物体下方
                if (levelMap[y, x] == 1) // Wall
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else if (levelMap[y, x] == 2) // Box
                {
                    Debug.Log($"Creating a box at position ({x}, {y})");
                    Debug.Log($"Call stack:\n{UnityEngine.StackTraceUtility.ExtractStackTrace()}");
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

    private void ClearLevel()
    {
        foreach (GameObject obj in instantiatedObjects)
        {
            Destroy(obj);
        }
        instantiatedObjects.Clear(); // 清空列表
    }
}
