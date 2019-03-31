using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    int parentIndex;
    int parentChildCount;
    int index;

    void Awake()
    {
        parentIndex = transform.parent.GetSiblingIndex();
        parentChildCount = transform.parent.childCount;
        index = transform.GetSiblingIndex();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = string.Format("Level {0}", ((parentChildCount * parentIndex) + index + 1).ToString("0#"));
    }

    public void UpdateLevelAndLoad()
    {
        LevelManager.Instance.LoadLevel((Level)(parentChildCount * parentIndex) + index);
    }
}
