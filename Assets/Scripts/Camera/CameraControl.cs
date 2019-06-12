using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject pl;
    public Vector3 CameraOffset = Vector3.back * 9f;

    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        Camera.main.transform.position = CameraOffset;
        Camera.main.transform.rotation = pl.transform.rotation;
    }
}