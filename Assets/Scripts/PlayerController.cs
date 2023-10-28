using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private Hammer hammerPrefab;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Animator animator;

    private bool canAttack = true;
    private GameObject throwTargetObject;
    private GameObject hammerObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool attack = GetAttackInput();
        if (canAttack && attack)
        {
            ThrowHammer();
        }
    }

    private void FixedUpdate()
    {
        Vector3 vel = GetVelocity();
        Move(vel);
        animator.SetBool("Moving", rb.velocity.magnitude > 0.0001f);
    }

    private Vector3 GetVelocity()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector3(horizontal, 0, vertical).normalized * speed;
    }

    private bool GetAttackInput()
    {
        return Input.GetButtonDown("Fire1");
    }

    private void ThrowHammer()
    {
        canAttack = false;

        // Create throw target
        throwTargetObject = new GameObject();
        throwTargetObject.transform.position = GetMouseTarget();

        Hammer hammer = Instantiate(hammerPrefab, transform.position, Quaternion.identity);
        hammerObject = hammer.gameObject;
        hammer.Setup(throwTargetObject.transform, transform, AttackDone);
    }

    private void Move(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    private Vector3 GetMouseTarget()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000f, groundMask))
        {
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            return target;
        }

        return transform.position;
    }

    private void AttackDone()
    {
        canAttack = true;
        animator.SetTrigger("HammerCaught");
        Destroy(hammerObject);
        Destroy(throwTargetObject);
    }
}
