using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.RoomTriggerActive(gameObject.transform.parent.gameObject.transform.parent.gameObject);
        }
    }
}
