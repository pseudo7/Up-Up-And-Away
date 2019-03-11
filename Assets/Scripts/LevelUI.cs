using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    int index;

    void Awake()
    {
        index = transform.GetSiblingIndex();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = string.Format("Level {0}", index + 1);
    }

    public void UpdateLevelAndLoad()
    {
        LevelManager.Instance.LoadLevel((Level)index);
    }
}
