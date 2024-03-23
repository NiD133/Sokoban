using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    private int LevelNumber = 1;

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        if (AreAllBoxesOnTargets())
        {
            LevelNumber++;
            levelGenerator.ClearLevel();
            levelGenerator.GenerateLevel(LevelNumber);
            
        }
    }

    public void StartLevel()
    {
        if (levelGenerator != null)
        {   
            levelGenerator.GenerateLevel(LevelNumber);
        }
        else
        {
            Debug.LogError("LevelGenerator not assigned!");
        }
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
