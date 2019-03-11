using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i <= PlayerPrefManager.IsLevelUnlocked; i++)
            transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
    }
}
