using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlane : MonoBehaviour
{
    static Transform playerTransform;

    Vector3 startPos = new Vector3(0, -4.5f, -1.5f);

    void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.PLAYER_TAG))
            if (playerTransform.position != startPos)
                playerTransform.position = Vector3.MoveTowards(playerTransform.position, startPos, .1f);
    }
}
