using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    private GameObject Player;
    private bool AttackingPlayer;
    
    private void OnEnable()
    {
        if (!gameObject.GetComponent<Enemy>().Dead)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            AttackingPlayer = false;
            StartCoroutine(ChasePlayer());
        }
        else
        {
            Debug.Log("Dead: " + gameObject.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, this.gameObject.GetComponent<Enemy>().AgroRadius);
    }

    IEnumerator ChasePlayer()
    {
        while(true)
        {
            if (!AttackingPlayer && Player!= null && Vector3.Distance(this.gameObject.transform.position, Player.transform.position) < this.gameObject.GetComponent<Enemy>().AgroRadius)
            {
                AttackingPlayer = true;
                StartCoroutine(AttackMove(Player.transform.position));
            }
            yield return null;
        }
    }

    IEnumerator AttackMove(Vector3 Pos)
    {
        yield return new WaitForSeconds(0.5f);
        {
            Vector3 direc = (Pos - transform.position).normalized;
            direc.z = this.gameObject.transform.position.z;
            GetComponent<Rigidbody>().AddForce(direc * gameObject.GetComponent<Enemy>().ForceAmt, ForceMode.Impulse);
            AttackingPlayer = false;
        }
    }
}