using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour
{
    public Camera zoomCamera;
    public Transform magnifierSprite;

    void Update()
    {
        Vector3 pos = magnifierSprite.position;
        zoomCamera.transform.position = new Vector3(pos.x, pos.y, zoomCamera.transform.position.z);
    }
}
