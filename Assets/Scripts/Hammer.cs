using System;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 15.0f;
    [SerializeField] private float minSpeed = 10.0f;
    [SerializeField] private float acceleration = -0.2f;
    [SerializeField] private float minRange = 2f;
    [SerializeField] private float maxRange = 4f;

    private Transform targetTransform;
    private Transform returnTransform;
    private Action doneCallback;
    private bool targetReached;

    private float speed;

    public void Setup(Transform targetTransform, Transform returnTransform, Action doneCallback)
    {
        this.targetTransform = targetTransform;
        this.returnTransform = returnTransform;
        this.doneCallback = doneCallback;

        Vector3 toTarget = this.targetTransform.position - transform.position;
        float mag = toTarget.magnitude;
        mag = Mathf.Clamp(mag, minRange, maxRange);
        toTarget = toTarget.normalized * mag;
        this.targetTransform.position = transform.position + toTarget;

        speed = initialSpeed;
    }

    private void Update()
    {
        Move();
        UpdateSpeed();
        UpdateTarget();
    }

    private void Move()
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Vector3 velocity = direction * speed * Time.deltaTime;

        transform.position = transform.position + velocity;
    }

    private void UpdateSpeed()
    {
        float newSpeed = speed + acceleration;
        speed = Math.Max(minSpeed, newSpeed);
    }

    private void UpdateTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
        if (distanceToTarget < 0.05f)
        {
            if (!targetReached)
            {
                targetReached = true;
                targetTransform = returnTransform;
                acceleration = acceleration * -1;
            }
            else
            {
                doneCallback.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Pumpkin pumpkin = collider.GetComponent<Pumpkin>();
        if (pumpkin != null)
        {
            pumpkin.Defeat();
        }
    }
}
