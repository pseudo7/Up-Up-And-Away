using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlatformCamera : MonoBehaviour
{
    public static bool allowRotation;

    public Vector3 offset = new Vector3(0, 5, -15);

    Transform playerTransform;
    Camera mainCam;

    void Awake()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        mainCam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, offset + Vector3.up * playerTransform.position.y, .05f);

        if (allowRotation)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.position.y * Constants.PLATFORM_HEIGHT);
            mainCam.fieldOfView = 60 + Mathf.Abs(Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad)) * 25;
        }
    }
}
