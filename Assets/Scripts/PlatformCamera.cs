using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCamera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 5, -15);

    Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, offset + Vector3.up * playerTransform.position.y, .05f);
    }
}
