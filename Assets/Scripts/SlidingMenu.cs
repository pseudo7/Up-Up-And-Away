using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingMenu : MonoBehaviour
{
    [SerializeField] Transform menuParent;
    [SerializeField] Transform menuDotParent;
    [SerializeField] float slidingSpeed = 1500f;

    static Vector3 lastPosition;
    static int currentIndex;

    float limit;
    float width;
    bool isSliding;

    void Start()
    {
        width = menuParent.GetChild(0).GetComponent<RectTransform>().rect.width;
        limit = (menuParent.childCount - 1) * -width;

        foreach (Transform child in menuParent)
            child.localPosition = Vector3.right * child.GetSiblingIndex() * width;

        menuParent.localPosition = lastPosition;
        UpdateMenuDot();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftArrow) && menuParent.localPosition.x > limit)
            Slide(-1);
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (int)menuParent.localPosition.x < 0)
            Slide(1);
#else
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            float deltaX;
            if (Mathf.Abs(deltaX = touch.deltaPosition.x) > Mathf.Abs(touch.deltaPosition.y))
                if (deltaX < 0 && menuParent.localPosition.x > limit)
                    Slide(-1);
                else if (deltaX > 0 && (int)menuParent.localPosition.x < 0)
                    Slide(1);
        }
#endif
    }

    private void Slide(float deltaX)
    {
        if (!isSliding)
            StartCoroutine(Sliding(deltaX < 0));
    }

    IEnumerator Sliding(bool slideLeft)
    {
        isSliding = true;
        Vector3 nextPos = slideLeft ? Vector3.left : Vector3.right;
        nextPos *= width;
        nextPos += menuParent.localPosition;

        currentIndex += slideLeft ? 1 : -1;

        while (slideLeft ? menuParent.localPosition.x > nextPos.x : menuParent.localPosition.x < nextPos.x)
        {
            menuParent.localPosition = Vector3.MoveTowards(menuParent.localPosition, nextPos, Time.deltaTime * slidingSpeed);
            yield return new WaitForEndOfFrame();
        }

        lastPosition = menuParent.localPosition;
        UpdateMenuDot();
        isSliding = false;
    }

    void UpdateMenuDot()
    {
        foreach (Transform child in menuDotParent)
            child.GetComponent<Image>().color = new Color(.3f, .3f, .3f);
        menuDotParent.GetChild(currentIndex).GetComponent<Image>().color = Color.white;
    }
}