using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 1f;
    public float AgroRadius = 0f;
    public float Damage = 0f;
    public float MoveSpeed = 0f;
    public float ForceAmt = 0f;
    public bool Dead = false;
    public float DelayinAttacks = 0f;
    public float AttackRadius = 0f;
    public float ThreatLevel = 0f;

    public Vector3 SpawnPos;
}