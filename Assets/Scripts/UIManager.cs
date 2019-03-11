using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Text levelText;
    [SerializeField] Text timeText;

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    void Update()
    {
        UpdateLevelTime();
    }

    public void UpdateLevelText(Level levelName)
    {
        levelText.text = string.Format("Level {0}", (int)(levelName + 1));
    }

    void UpdateLevelTime()
    {
        timeText.text = Time.timeSinceLevelLoad.ToString("00#.00");
    }
}
