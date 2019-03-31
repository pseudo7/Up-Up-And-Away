using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroller : MonoBehaviour
{
    [SerializeField] float speed = 5;
    RawImage bgImg;
    float delta;

    void Start()
    {
        bgImg = GetComponent<RawImage>();
    }

    void Update()
    {
        bgImg.uvRect = new Rect(delta += speed * Time.deltaTime, 0, 1, 1);
    }
}
