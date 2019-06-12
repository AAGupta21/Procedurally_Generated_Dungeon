using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    [SerializeField] private GameObject Bullet = null;
    [SerializeField] private float BulletMoveSpeed = 10f;
    [SerializeField] private float DelayBeforeBulletDestroy = 1f;

    private bool IsAttacking;

    private void OnEnable()
    {
        if (!gameObject.GetComponent<Enemy>().Dead)
        {
            IsAttacking = false;
            StartCoroutine(Activated());
        }
        else
        {
            Debug.Log("Dead: " + gameObject.name);
        }
    }

    IEnumerator Activated()
    {
        while(!GetComponent<Enemy>().Dead)
        {
            if (!IsAttacking)
            {
                StartCoroutine(AttackMove());
                IsAttacking = true;
            }

            yield return null;
        }
    }

    IEnumerator AttackMove()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<Enemy>().DelayinAttacks);
        Attack();
    }
    
    void Attack()
    {
        float x_Len = gameObject.GetComponent<SphereCollider>().radius;
        float y_Len = gameObject.GetComponent<SphereCollider>().radius;   //Need to update when art is added.
        
        GameObject B1 = Instantiate(Bullet, transform.position + new Vector3(x_Len, y_Len, 0f), Quaternion.identity);
        GameObject B2 = Instantiate(Bullet, transform.position + new Vector3(-x_Len, y_Len, 0f), Quaternion.identity);
        GameObject B3 = Instantiate(Bullet, transform.position + new Vector3(-x_Len, -y_Len, 0f), Quaternion.identity);
        GameObject B4 = Instantiate(Bullet, transform.position + new Vector3(x_Len, -y_Len, 0f), Quaternion.identity);
        
        StartCoroutine(BulletMove(B1,new Vector3(1, 1, 0)));
        StartCoroutine(BulletMove(B2, new Vector3(-1, 1, 0)));
        StartCoroutine(BulletMove(B3, new Vector3(-1, -1, 0)));
        StartCoroutine(BulletMove(B4, new Vector3(1, -1, 0)));
        
        Destroy(B1, DelayBeforeBulletDestroy);
        Destroy(B2, DelayBeforeBulletDestroy);
        Destroy(B3, DelayBeforeBulletDestroy);
        Destroy(B4, DelayBeforeBulletDestroy);

        Invoke("ReadyNextAttack", DelayBeforeBulletDestroy);
    }

    IEnumerator BulletMove(GameObject Bullet, Vector3 dir)
    {
        while(Bullet!= null)
        {
            Bullet.transform.position += (dir * BulletMoveSpeed * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }

    void ReadyNextAttack()
    {
        IsAttacking = false;
    }
}