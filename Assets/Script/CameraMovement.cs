using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform trCamera;
    public float offsetY = 45f;
    public float offsetZ = -40f;

    Vector3 cameraPos;

    private void LateUpdate()
    {
        cameraPos.x = player.position.x;
        cameraPos.y = player.position.y + offsetY;
        cameraPos.z = player.position.z + offsetZ;

        trCamera.position = cameraPos;

    }
}
