using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 rotationAxes = Vector3.up;

    void Update()
    {
        transform.Rotate(rotationAxes * Time.deltaTime * speed);
    }
}
