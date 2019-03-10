using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Text levelText;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void UpdateLevelText(string levelName)
    {
        levelText.text = levelName;
    }
}
