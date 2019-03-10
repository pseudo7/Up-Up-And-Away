using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    Dictionary<Level, LevelInfo> levelMap;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            levelMap = new Dictionary<Level, LevelInfo>
            {
                { Level.Level1, new LevelInfo(10, GetPlatformInfo(false, 10, 8, 18, 100, 200 )) },
                { Level.Level2, new LevelInfo(15, GetPlatformInfo(false, 15, 6, 15, 100, 225 )) },
                { Level.Level3, new LevelInfo(20, GetPlatformInfo(false, 20, 4, 12, 100, 250 )) }
            };
        }
    }

    public LevelInfo LoadLevel(Level levelName)
    {
        return levelMap[levelName];
    }

    PlatformInfo[] GetPlatformInfo(bool evenlyDistribute, int platformCount, int lowerSegmentCount, int upperSegmentCount, float lowerRotationSpeed, float upperRotationSpeed)
    {
        PlatformInfo[] platforms = new PlatformInfo[platformCount];

        float segmentCountStep = (upperSegmentCount - lowerSegmentCount) / platformCount;
        float rotationStep = (upperRotationSpeed - lowerRotationSpeed) / platformCount;

        for (int i = 0; i < platformCount; i++)
            platforms[i] = new PlatformInfo(evenlyDistribute, upperSegmentCount - (int)(i * segmentCountStep), lowerRotationSpeed + i * rotationStep);

        return platforms;
    }

    public void LoadNextLevel()
    {
        PlatformManager.currentLevel++;
        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public struct LevelInfo
{
    public int platformCount;
    public PlatformInfo[] platforms;

    public LevelInfo(int platformCount, PlatformInfo[] platforms)
    {
        this.platformCount = platformCount;
        this.platforms = platforms;
    }
}

public enum Level
{
    Level1, Level2, Level3
}