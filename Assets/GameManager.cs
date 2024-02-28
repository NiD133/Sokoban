using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelGenerator levelGenerator;

    private void Start()
    {
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
