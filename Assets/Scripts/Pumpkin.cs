using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    [SerializeField] private float chaseSpeed = 4.0f;
    [SerializeField] private float attackMovementSpeed = 1.0f;
    [SerializeField] private float attackCooldown = 2.0f;
    [SerializeField] private Collider attackHitbox;
    [SerializeField] private AlertTrigger hurtbox;

    private Transform followTarget;
    private Rigidbody rb;
    private Animator animator;
    private Collider col;

    private bool canAttack = true;

    private PumpkinState state = PumpkinState.Chasing;

    public void Setup(Transform followTarget)
    {
        this.followTarget = followTarget;

        attackHitbox.enabled = false;
    }

    public void Defeat()
    {
        rb.velocity = Vector3.zero;
        state = PumpkinState.Dead;
        animator.SetTrigger("Death");
        col.enabled = false;
        hurtbox.enabled = false;
        StartCoroutine(Vanish());
    }

    public void EndAttack()
    {
        state = PumpkinState.Chasing;
        StartCoroutine(TickAttackCooldown());
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();

        hurtbox.callback = OnHurtboxTrigger;
    }

    private void Update()
    {
        if (state == PumpkinState.Dead)
            return;

        if (state == PumpkinState.Chasing)
        {
            Vector3 toTarget = followTarget.position - transform.position;
            if (toTarget.magnitude <= 1f)
            {
                if (canAttack)
                {
                    state = PumpkinState.Attacking;
                    StartAttack();
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    state = PumpkinState.Stopped;
                }
            }
            else if (state == PumpkinState.Stopped)
            {
                state = PumpkinState.Chasing;
            }
        }
        else if (state == PumpkinState.Stopped)
        {
            Vector3 toTarget = followTarget.position - transform.position;
            if (canAttack)
            {
                state = PumpkinState.Attacking;
                StartAttack();
            }
            else if (toTarget.magnitude > 1f)
            {
                state = PumpkinState.Chasing;
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == PumpkinState.Chasing)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 toTarget = followTarget.position - transform.position;
        rb.velocity = toTarget.normalized * chaseSpeed;
    }

    private void StartAttack()
    {
        Vector3 target = followTarget.position;
        Vector3 attackDirection = target - transform.position;
        canAttack = false;
        animator.SetTrigger("Attack");
        rb.velocity = attackDirection.normalized * attackMovementSpeed;
    }

    private IEnumerator Vanish()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    private IEnumerator TickAttackCooldown()
    {
        float attackTimer = 0;
        while (attackTimer < attackCooldown)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }
        canAttack = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Hurt();
        }
    }

    private void OnHurtboxTrigger(Collider collider)
    {
        Defeat();
    }
}
