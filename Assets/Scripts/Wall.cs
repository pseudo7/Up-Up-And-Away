using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.PLAYER_TAG)) LevelManager.Instance.RestartLevel();
    }
}
