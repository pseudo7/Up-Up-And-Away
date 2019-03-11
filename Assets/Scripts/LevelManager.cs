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
            DontDestroyOnLoad(gameObject);
            levelMap = new Dictionary<Level, LevelInfo>
            {
                { Level.Level1, new LevelInfo(10, GetPlatformInfo(false, false, 10, 8, 18, 100, 200 )) },
                { Level.Level2, new LevelInfo(10, GetPlatformInfo(false, true, 10, 8, 18, 100, 200 )) },
                { Level.Level3, new LevelInfo(15, GetPlatformInfo(false, false, 15, 6, 15, 100, 225 )) },
                { Level.Level4, new LevelInfo(15, GetPlatformInfo(false, true, 15, 6, 15, 100, 225 )) },
                { Level.Level5, new LevelInfo(20, GetPlatformInfo(false, false, 20, 4, 12, 100, 250 )) },
                { Level.Level6, new LevelInfo(20, GetPlatformInfo(false, true, 20, 4, 12, 100, 250 )) },
            };
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1) SceneManager.LoadScene(0);
            else if (SceneManager.GetActiveScene().buildIndex == 0) Application.Quit();
        }
    }

    public LevelInfo GetLevelInfo(Level levelName)
    {
        return levelMap[levelName];
    }

    public void LoadLevel(Level level)
    {
        int levelsLength = System.Enum.GetValues(typeof(Level)).Length;
        if ((int)level > levelsLength)
            return;
        if ((int)level == levelsLength)
        {
            SceneManager.LoadScene(0);
            return;
        }
        else
        {
            PlatformManager.currentLevel = level;
            SceneManager.LoadScene(1);
        }
    }

    public void LoadNextLevel()
    {
        PlatformManager.currentLevel++;
        LoadLevel(PlatformManager.currentLevel);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    PlatformInfo[] GetPlatformInfo(bool evenlyDistribute, bool mixRotation, int platformCount, int lowerSegmentCount, int upperSegmentCount, float lowerRotationSpeed, float upperRotationSpeed)
    {
        PlatformInfo[] platforms = new PlatformInfo[platformCount];

        float segmentCountStep = (upperSegmentCount - lowerSegmentCount) / platformCount;
        float rotationStep = (upperRotationSpeed - lowerRotationSpeed) / platformCount;

        for (int i = 0; i < platformCount; i++)
            platforms[i] = new PlatformInfo(evenlyDistribute, upperSegmentCount - (int)(i * segmentCountStep),
                (lowerRotationSpeed + i * rotationStep) * (mixRotation && i % 2 == 0 ? 1 : -1));

        return platforms;
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
    Level1, Level2, Level3, Level4, Level5, Level6
}