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
        jumpsLeft = 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    void Jump()
    {
        if (jumpsLeft-- > 0) playerRB.velocity = Vector3.up * jumpSpeed;
    }
}
