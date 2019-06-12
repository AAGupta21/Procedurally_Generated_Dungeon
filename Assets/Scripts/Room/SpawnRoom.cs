using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public int OpeningDirection;
    public Transform Parent_Room_Class;

    private RoomTemplate temp;

    private int Rand;

    private bool Spawned = false;

    private void Start()
    {
        temp = GameObject.FindGameObjectsWithTag("Room")[0].GetComponent<RoomTemplate>();
        Parent_Room_Class = temp.transform;
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if(!Spawned && (GameManager.instance.Curr_Room_Cnt < GameManager.instance.Room_Cnt))
        {
            if (OpeningDirection == 1)
            {
                Rand = Random.Range(0, temp.GetComponent<RoomTemplate>().DDRooms.Length);
                GameObject g = Instantiate(temp.DDRooms[Rand], transform.position, Quaternion.identity, Parent_Room_Class);
                if(g == null)
                {
                    Debug.Log("issue in g");
                }
                GameManager.instance.RoomArray.Add(g);
            }

            if (OpeningDirection == 2)
            {
                Rand = Random.Range(0, temp.GetComponent<RoomTemplate>().LDRooms.Length);
                GameObject g = Instantiate(temp.LDRooms[Rand], transform.position, Quaternion.identity, Parent_Room_Class);
                GameManager.instance.RoomArray.Add(g);
            }

            if (OpeningDirection == 3)
            {
                Rand = Random.Range(0, temp.GetComponent<RoomTemplate>().UDRooms.Length);
                GameObject g = Instantiate(temp.UDRooms[Rand], transform.position, Quaternion.identity, Parent_Room_Class);
                GameManager.instance.RoomArray.Add(g);
            }

            if (OpeningDirection == 4)
            {
                Rand = Random.Range(0, temp.GetComponent<RoomTemplate>().RDRooms.Length);
                GameObject g = Instantiate(temp.RDRooms[Rand], transform.position, Quaternion.identity, Parent_Room_Class);
                GameManager.instance.RoomArray.Add(g);
            }

            GameManager.instance.Curr_Room_Cnt++;
            Spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpawnPoint" && GameManager.instance.ClosedRoomCheckActive)
        {
            Debug.Log("Spawn Point Collision");
        }
    }
}