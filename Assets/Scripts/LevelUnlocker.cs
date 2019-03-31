using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int childCount = transform.GetChild(i).childCount;

            for (int j = 0; j < childCount; j++)
            {
                if (i * childCount + j > PlayerPrefManager.IsLevelUnlocked) continue;

                float timing = PlayerPrefManager.GetLevelTime(i * childCount + j);
                transform.GetChild(i).GetChild(j).GetChild(3).gameObject.SetActive(false);
                transform.GetChild(i).GetChild(j).GetChild(4).GetComponent<Text>().text = timing.ToString("00#.00");
            }
        }
    }
}
