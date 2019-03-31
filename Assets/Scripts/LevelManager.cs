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
            Application.targetFrameRate = 60;
            Instance = this;
            DontDestroyOnLoad(gameObject);
            levelMap = new Dictionary<Level, LevelInfo>
            {
                { Level.Level01, new LevelInfo(10, false, false, GetPlatformInfo(false, false, 10, 8, 18, 100, 200 )) },
                { Level.Level02, new LevelInfo(10, false, false, GetPlatformInfo(false, true, 10, 8, 18, 125, 200 )) },
                { Level.Level03, new LevelInfo(10, true, false, GetPlatformInfo(false, true, 10, 8, 18, 100, 200 )) },
                { Level.Level04, new LevelInfo(10, false, true, GetPlatformInfo(false, false, 10, 8, 18, 100, 200 )) },

                { Level.Level05, new LevelInfo(15, false, false, GetPlatformInfo(false, false, 15, 6, 15, 100, 225 )) },
                { Level.Level06, new LevelInfo(15, false, false, GetPlatformInfo(false, true, 15, 6, 15, 125, 225 )) },
                { Level.Level07, new LevelInfo(15, true, false, GetPlatformInfo(false, true, 15, 6, 15, 100, 225 )) },
                { Level.Level08, new LevelInfo(15, false, true, GetPlatformInfo(false, true, 15, 6, 15, 100, 225 )) },

                { Level.Level09, new LevelInfo(20, false, false, GetPlatformInfo(false, false, 20, 4, 12, 100, 250 )) },
                { Level.Level10, new LevelInfo(20, false, false, GetPlatformInfo(false, true, 20, 4, 12, 125, 250 )) },
                { Level.Level11, new LevelInfo(20, true, false, GetPlatformInfo(false, true, 20, 4, 12, 100, 250 )) },
                { Level.Level12, new LevelInfo(20, false, true, GetPlatformInfo(false, false, 20, 4, 12, 100, 250 )) },

                { Level.Level13, new LevelInfo(25, false, false, GetPlatformInfo(false, false, 20, 4, 12, 125, 250 )) },
                { Level.Level14, new LevelInfo(25, false, false, GetPlatformInfo(false, true, 20, 4, 12, 150, 250 )) },
                { Level.Level15, new LevelInfo(25, true, false, GetPlatformInfo(false, true, 20, 4, 12, 150, 250 )) },
                { Level.Level16, new LevelInfo(25, false, true, GetPlatformInfo(false, false, 20, 4, 12, 150, 250 )) },



                { Level.Level17, new LevelInfo(10, false, false, GetPlatformInfo(false, false, 10, 8, 18, 100, 200 )) },
                { Level.Level18, new LevelInfo(10, false, false, GetPlatformInfo(false, true, 10, 8, 18, 125, 200 )) },
                { Level.Level19, new LevelInfo(10, true, false, GetPlatformInfo(false, true, 10, 8, 18, 100, 200 )) },
                { Level.Level20, new LevelInfo(10, true, false, GetPlatformInfo(false, true, 10, 8, 18, 100, 200 )) },

                { Level.Level21, new LevelInfo(15, false, false, GetPlatformInfo(false, false, 15, 6, 15, 100, 225 )) },
                { Level.Level22, new LevelInfo(15, false, false, GetPlatformInfo(false, true, 15, 6, 15, 125, 225 )) },
                { Level.Level23, new LevelInfo(15, true, false, GetPlatformInfo(false, true, 15, 6, 15, 100, 225 )) },
                { Level.Level24, new LevelInfo(15, true, false, GetPlatformInfo(false, true, 15, 6, 15, 100, 225 )) },

                { Level.Level25, new LevelInfo(20, false, false, GetPlatformInfo(false, false, 20, 4, 12, 100, 250 )) },
                { Level.Level26, new LevelInfo(20, false, false, GetPlatformInfo(false, true, 20, 4, 12, 125, 250 )) },
                { Level.Level27, new LevelInfo(20, true, false, GetPlatformInfo(false, true, 20, 4, 12, 100, 250 )) },
                { Level.Level28, new LevelInfo(20, true, false, GetPlatformInfo(false, true, 20, 4, 12, 100, 250 )) },

                { Level.Level29, new LevelInfo(25, false, false, GetPlatformInfo(false, false, 20, 4, 12, 125, 250 )) },
                { Level.Level30, new LevelInfo(25, false, false, GetPlatformInfo(false, true, 20, 4, 12, 150, 250 )) },
                { Level.Level31, new LevelInfo(25, true, false, GetPlatformInfo(false, true, 20, 4, 12, 150, 250 )) },
                { Level.Level32, new LevelInfo(25, true, false, GetPlatformInfo(false, true, 20, 4, 12, 150, 250 )) },
            };
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1) SceneManager.LoadScene(0);
            else if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SettingsManager settingsManager = FindObjectOfType<SettingsManager>();
                if (settingsManager && settingsManager.settingsPanel.activeInHierarchy)
                    settingsManager.settingsPanel.SetActive(false);
                else { Debug.Log("QUIT"); Application.Quit(); }
            }
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
        int currentLevel = (int)PlatformManager.currentLevel;
        PlayerPrefManager.SetLevelTime(currentLevel, Time.timeSinceLevelLoad);

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
                (lowerRotationSpeed + i * rotationStep) * (mixRotation && i % 2 == 0 ? -1 : 1));

        return platforms;
    }
}

public struct LevelInfo
{
    public int platformCount;
    public bool allowCameraRotation;
    public bool allowCameraRevolution;
    public PlatformInfo[] platforms;

    public LevelInfo(int platformCount, bool allowCameraRotation, bool allowCameraRevolution, PlatformInfo[] platforms)
    {
        this.platformCount = platformCount;
        this.allowCameraRotation = allowCameraRotation;
        this.allowCameraRevolution = allowCameraRevolution;
        this.platforms = platforms;
    }
}

public enum Level
{
    Level01, Level02, Level03, Level04, Level05, Level06, Level07, Level08, Level09, Level10, Level11, Level12,
    Level13, Level14, Level15, Level16, Level17, Level18, Level19, Level20, Level21, Level22, Level23, Level24,
    Level25, Level26, Level27, Level28, Level29, Level30, Level31, Level32, Level33, Level34, Level35, Level36
}