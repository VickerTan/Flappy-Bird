using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public float cameraOrthoSize = 128f;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        cam.orthographicSize = cameraOrthoSize;
    }
}
