using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyKnight : MonoBehaviour
{
    [SerializeField] private BoxCollider WeaponCollider = null;

    private GameObject Player = null;

    private bool AttackingPlayer;
    private bool ChasingPlayer;
    
    private void OnEnable()
    {
        WeaponCollider.enabled = false;

        if (!gameObject.GetComponent<Enemy>().Dead && GameObject.FindGameObjectWithTag("Player") != null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            AttackingPlayer = ChasingPlayer = false;
            StartCoroutine(DefaultState());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, GetComponent<Enemy>().AgroRadius);
        Gizmos.DrawWireSphere(transform.position, GetComponent<Enemy>().AttackRadius);
    }

    IEnumerator DefaultState()
    {
        while(!gameObject.GetComponent<Enemy>().Dead)
        {
            if(Vector3.Distance(this.gameObject.transform.position, Player.transform.position) < this.gameObject.GetComponent<Enemy>().AgroRadius && !AttackingPlayer && !ChasingPlayer)
            {
                ChasingPlayer = true;
                StartCoroutine(ChasePlayer());
            }

            yield return null;
        }
    }

    IEnumerator ChasePlayer()
    {
        while (ChasingPlayer && !AttackingPlayer)
        {
            Vector3 Dir = (Player.transform.position - transform.position).normalized;
            Dir.z = 0f;
            transform.position += Dir * GetComponent<Enemy>().MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, Player.transform.position) < GetComponent<Enemy>().AttackRadius)
            {
                ChasingPlayer = false;
                AttackingPlayer = true;
                StartCoroutine(AttackPlayer());
            }

            if (Vector3.Distance(transform.position, Player.transform.position) > GetComponent<Enemy>().AgroRadius)
                ChasingPlayer = false;

            yield return null;
        }

        if (Vector3.Distance(transform.position, Player.transform.position) > GetComponent<Enemy>().AgroRadius)
            ChasingPlayer = false;

        yield return null;
    }

    IEnumerator AttackPlayer()
    {
        Vector3 target = Player.transform.position;
        yield return new WaitForSeconds(GetComponent<Enemy>().DelayinAttacks);
        {
            WeaponCollider.enabled = true;

            WeaponCollider.center = (target - transform.position).normalized * this.gameObject.GetComponent<Enemy>().AttackRadius * 0.7f;

            AttackingPlayer = false;

            StartCoroutine(DisableWeapon());
        }
    }

    IEnumerator DisableWeapon()
    {
        yield return new WaitForSeconds(0.1f);

        WeaponCollider.enabled = false;
    }
}