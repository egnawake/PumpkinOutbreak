using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;

    private Transform followTarget;
    private Rigidbody rb;
    private Animator animator;
    private Collider col;

    private bool dead = false;

    public void Setup(Transform followTarget)
    {
        this.followTarget = followTarget;
    }

    public void Defeat()
    {
        rb.velocity = Vector3.zero;
        dead = true;
        animator.SetTrigger("Death");
        col.enabled = false;
        StartCoroutine(Vanish());
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 toTarget = followTarget.position - transform.position;
        if (toTarget.magnitude > 0.2f)
        {
            rb.velocity = toTarget.normalized * speed;
        }
    }

    private IEnumerator Vanish()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
