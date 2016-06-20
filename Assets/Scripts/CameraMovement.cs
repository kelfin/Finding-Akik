using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public Transform target;
    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x + 4f , 0, -10);
    }
}
