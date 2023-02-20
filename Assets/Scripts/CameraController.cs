using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 50, -10);

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
