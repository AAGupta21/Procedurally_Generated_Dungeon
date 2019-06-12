using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointDestroyer : MonoBehaviour
{
    float Time2;

    private void OnEnable()
    {
        Time2 = 0f;
    }

    private void Update()
    {
        Time2 += Time.deltaTime;

        if(Time2 > GameManager.instance.WaitTimeBeforeDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnPoint")
        {
            Destroy(other.gameObject);
        }
    }
}