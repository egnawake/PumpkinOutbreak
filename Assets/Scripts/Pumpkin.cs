using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;

    private Transform followTarget;
    private Rigidbody rb;

    public void Setup(Transform followTarget)
    {
        this.followTarget = followTarget;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 toTarget = followTarget.position - transform.position;
        if (toTarget.magnitude > 0.2f)
        {
            rb.velocity = toTarget.normalized * speed;
        }
    }
}
