using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int currentHeightIndex;

    public float jumpSpeed = 5;
    public Material ballMatHigh;
    public Material ballMatLow;

    Rigidbody playerRB;
    int jumpsLeft = 2;

    void Start()
    {
        Debug.Log("Quality: " + PlayerPrefManager.Quality);
        if (PlayerPrefManager.Quality == 0)
        {
            GetComponent<MeshRenderer>().material = ballMatLow;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            Camera.main.allowMSAA = false;
        }
        else
        {
            GetComponent<MeshRenderer>().material = ballMatHigh;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            Camera.main.allowMSAA = true;
        }
        playerRB = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
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
