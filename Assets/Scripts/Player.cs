using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int currentHeightIndex;

    public float jumpSpeed = 5;

    Rigidbody playerRB;
    int jumpsLeft = 2;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Replenish")) jumpsLeft = 2;
        else if (other.CompareTag("Finish")) LevelManager.Instance.LoadNextLevel();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Jump();
    }

    void Jump()
    {
        if (jumpsLeft-- > 0) playerRB.velocity = Vector3.up * (jumpsLeft == 0 ? jumpSpeed : jumpSpeed * .75f);
    }
}
