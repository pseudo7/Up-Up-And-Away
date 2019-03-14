using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i <= PlayerPrefManager.IsLevelUnlocked; i++)
        {
            float timing = PlayerPrefManager.GetLevelTime(i);
            transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
            transform.GetChild(i).GetChild(4).GetComponent<Text>().text = timing.ToString("00#.00");
        }
    }
}
