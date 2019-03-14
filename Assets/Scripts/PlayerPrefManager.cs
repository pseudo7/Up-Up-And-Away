using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager
{
    public static int IsLevelUnlocked
    {
        get
        {
            if (!PlayerPrefs.HasKey(Constants.LEVEL_UNLOCK_KEY))
            {
                PlayerPrefs.SetInt(Constants.LEVEL_UNLOCK_KEY, 0);
                PlayerPrefs.Save();
            }
            return PlayerPrefs.GetInt(Constants.LEVEL_UNLOCK_KEY);
        }
        set
        {
            if (value > IsLevelUnlocked)
            {
                PlayerPrefs.SetInt(Constants.LEVEL_UNLOCK_KEY, value);
                PlayerPrefs.Save();
            }
        }
    }

    public static float GetLevelTime(int level)
    {
        if (!PlayerPrefs.HasKey(Constants.LEVEL_TIME_KEY))
        {
            LevelTimings levelTimings = new LevelTimings { LevelTime = new List<float> { float.PositiveInfinity } };

            string json = JsonUtility.ToJson(levelTimings);
            Debug.LogFormat("<color=red>{0}</color>", json);
            PlayerPrefs.SetString(Constants.LEVEL_TIME_KEY, json);
            PlayerPrefs.Save();
        }
        string loadedData = PlayerPrefs.GetString(Constants.LEVEL_TIME_KEY);
        Debug.LogFormat("<color=black>{0}</color>", loadedData);

        List<float> timings = JsonUtility.FromJson<LevelTimings>(loadedData).LevelTime;
        if (level > timings.Count - 1)
            return float.PositiveInfinity;
        return timings[level];
    }

    public static void SetLevelTime(int level, float timing)
    {
        string loadedData = PlayerPrefs.GetString(Constants.LEVEL_TIME_KEY);
        LevelTimings levelTimings = JsonUtility.FromJson<LevelTimings>(loadedData);
        Debug.LogFormat("<color=orange>{0}</color>", loadedData);

        if (level > levelTimings.LevelTime.Count - 1)
            levelTimings.LevelTime.Insert(level, timing);
        else if (timing < levelTimings.LevelTime[level])
            levelTimings.LevelTime[level] = timing;

        string json = JsonUtility.ToJson(levelTimings);
        Debug.LogFormat("<color=orange>{0}</color>", json);

        PlayerPrefs.SetString(Constants.LEVEL_TIME_KEY, json);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class LevelTimings
{
    public List<float> LevelTime = new List<float>();

    public override string ToString()
    {
        string returnString = string.Empty;

        for (int i = 0; i < LevelTime.Count; i++)
            returnString += i + " " + LevelTime[i] + "\n";

        return returnString;
    }
}
