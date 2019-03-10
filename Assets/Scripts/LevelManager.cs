using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    Dictionary<LevelName, LevelInfo> levelMap;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            levelMap = new Dictionary<LevelName, LevelInfo>
            {
                { LevelName.Level1, new LevelInfo(10, GetPlatformInfo(false, 10, 6, 18, 100, 250 )) },
                { LevelName.Level2, new LevelInfo(10, GetPlatformInfo(true, 10, 4, 12, 100, 250 )) },
            };
        }
    }

    public LevelInfo LoadLevel(LevelName levelName)
    {
        return levelMap[levelName];
    }

    PlatformInfo[] GetPlatformInfo(bool evenlyDistribute, int platformCount, int lowerSegmentCount, int upperSegmentCount, float lowerRotationSpeed, float upperRotationSpeed)
    {
        PlatformInfo[] platforms = new PlatformInfo[platformCount];

        float segmentCountStep = (upperSegmentCount - lowerSegmentCount) / platformCount;
        float rotationStep = (upperRotationSpeed - lowerRotationSpeed) / platformCount;

        for (int i = 0; i < platformCount; i++)
            platforms[i] = new PlatformInfo(evenlyDistribute, (int)(upperSegmentCount - segmentCountStep), lowerRotationSpeed + rotationStep);

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

public enum LevelName
{
    Level1, Level2, Level3
}