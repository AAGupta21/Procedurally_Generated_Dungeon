using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedRoomChecker : MonoBehaviour
{
    private RoomTemplate temp;
    private Transform Room_Parent;

    private void Start()
    {
        temp = GameObject.FindGameObjectsWithTag("Room")[0].GetComponent<RoomTemplate>();
        Room_Parent = temp.GetComponentInParent<Transform>();
        Invoke("SetUpClosedRoom", GameManager.instance.WaitTimeBeforeDestroy + 0.1f);
    }

    private void SetUpClosedRoom()
    {
        GameManager.instance.ClosedRoomCheckActive = true;
        GameObject[] SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach(GameObject Spoint in SpawnPoints)
        {
            Instantiate(temp.ClosedRoom, Spoint.transform.position, Quaternion.identity, Room_Parent);
            Destroy(Spoint);
        }

        Invoke("RemoveSpawner", 0.1f);
    }

    private void RemoveSpawner()
    {
        GameObject[] SpawnPointDestroyer = GameObject.FindGameObjectsWithTag("SpawnPointDestroyer");

        foreach(GameObject Dpoint in SpawnPointDestroyer)
        {
            Destroy(Dpoint, 0.1f);
        }

        GameManager.instance.LevelCreated = true;
    }
}