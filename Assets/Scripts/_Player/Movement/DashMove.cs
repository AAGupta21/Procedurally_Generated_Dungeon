using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    [SerializeField] private float DashDistance = 1f;
    [SerializeField] private float DashTime = 0.3f;

    private bool IsDashing = false;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W) && !IsDashing)
        {
            Dash(transform.up.normalized);
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S) && !IsDashing)
        {
            Dash(-transform.up.normalized);
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.D) && !IsDashing)
        {
            Dash(transform.right.normalized);
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.A) && !IsDashing)
        {
            Dash(-transform.right.normalized);
        }

        if(Input.GetKey(KeyCode.Space) && !IsDashing)
        {
            ShortDash(-transform.up.normalized);
        }
    }

    private void Dash(Vector3 Direc)
    {
        IsDashing = true;
        StartCoroutine(Dashing());
        GetComponent<Rigidbody>().velocity = (DashDistance / DashTime) * Direc; 
    }

    private void ShortDash(Vector3 Direc)
    {
        IsDashing = true;
        StartCoroutine(ShortDashing());
        GetComponent<Rigidbody>().velocity = (DashDistance / DashTime) * Direc;
    }

    IEnumerator Dashing()
    {
        yield return new WaitForSeconds(DashTime);
        IsDashing = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    IEnumerator ShortDashing()
    {
        yield return new WaitForSeconds(DashTime / 2);
        IsDashing = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}