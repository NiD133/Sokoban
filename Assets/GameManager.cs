using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelGenerator levelGenerator; // 确保在Unity编辑器中将这个引用拖拽到这里

    private void Start()
    {
        // 确保在游戏开始时生成关卡
        StartLevel();
    }

    private void Update()
    {
        if (AreAllBoxesOnTargets())
        {
            ReloadLevel();
        }
    }

    public void StartLevel()
    {
        if (levelGenerator != null)
        {
            levelGenerator.GenerateLevel();
        }
        else
        {
            Debug.LogError("LevelGenerator not assigned!");
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool AreAllBoxesOnTargets()
    {
        Box[] boxes = FindObjectsOfType<Box>();

        foreach (var box in boxes)
        {
            if (!box.isOnTarget)
            {
                return false;
            }
        }
        return true;
    }
}
