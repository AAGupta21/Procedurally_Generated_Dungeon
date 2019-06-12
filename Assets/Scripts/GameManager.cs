using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool PlayerSpawned = false;
    public bool ClosedRoomCheckActive = false;
    public bool LevelCreated = false;
    public bool EnemySpawned = false;
    public int Room_Cnt = 10;
    public int Curr_Room_Cnt = 0;
    public float WaitTimeBeforeDestroy = 2f;
    public int VictoryRoomIndex = 0;
    public GameObject ActiveWeapon = null;

    public float DifficultyLevel = 7;
    public GameObject EnemyParent;
    public GameObject[] EnemyList;
    public int StatPointsAgi = 1;
    public int StatPointsStr = 1;
    public int StatPointsVit = 1;
    public int StatPointsVig = 1;

    [HideInInspector] public List<GameObject> RoomArray = new List<GameObject>();
    [HideInInspector] public List<GameObject> EnemyArray = new List<GameObject>();
    public GameObject Player;

    private GameObject ActiveRoom;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (!EnemySpawned)
        {
            if (LevelCreated)
            {
                Transform Parent = GameObject.FindGameObjectWithTag("EnemyList").transform;

                foreach (GameObject Room in RoomArray)
                {
                    GameObject enemy = Instantiate(EnemyParent, Room.transform.position, Quaternion.identity);
                    enemy.SetActive(false);
                    enemy.transform.SetParent(Parent);
                    float currThreatLevel = 0;

                    while (currThreatLevel < DifficultyLevel)
                    {
                        GameObject e = Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], enemy.transform.position + new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0f), Quaternion.identity);    //Need to change this.
                        e.transform.SetParent(enemy.transform);
                        e.GetComponent<Enemy>().SpawnPos = transform.position;
                        currThreatLevel += e.GetComponent<Enemy>().ThreatLevel;
                    }

                    EnemyArray.Add(enemy);
                }

                EnemySpawned = true;
                StartCoroutine(SpawningPlayer());
            }

            yield return null;
        }
    }

    IEnumerator SpawningPlayer()
    {
        while (!PlayerSpawned)
        {
            if(EnemySpawned)
            {
                RoomArray.Insert(0, GameObject.FindGameObjectWithTag("BaseRoom"));
                EnemyArray.Insert(0, GameObject.Instantiate(EnemyParent, RoomArray[0].transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("EnemyList").transform));
                
                GameObject P = Instantiate(Player, Vector3.back * 0.5f, Player.transform.rotation);
                
                PlayerSpawned = true;
                
                ActiveRoom = RoomArray[0];
                EnemyArray[RetRoomArrayIndex(RoomArray[0])].SetActive(true);
            }

            yield return null;
        }
    }

    public void RoomTriggerActive(GameObject Room)
    {
        if(Room != ActiveRoom)
        {
            EnemyArray[RetRoomArrayIndex(ActiveRoom)].SetActive(false);
            EnemyArray[RetRoomArrayIndex(Room)].SetActive(true);
            ActiveRoom = Room;
        }
    }
    
    int RetRoomArrayIndex(GameObject Val)
    {
        int index = 0;
        foreach(GameObject g in RoomArray)
        {
            if(Val == g)
            {
                break;
            }

            index++;
        }

        return index;
    }
}