using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.instance.PlayerSpawned)
        {
            gameObject.GetComponent<CameraControl>().enabled = true;
            gameObject.GetComponent<PlayerCamera>().enabled = false;
        }
    }
}