using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private Hammer hammerPrefab;

    private Rigidbody rb;

    private bool canAttack = true;
    private GameObject throwTargetObject;
    private GameObject hammerObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bool attack = GetAttackInput();
        if (canAttack && attack)
        {
            canAttack = false;

            // Create throw target
            throwTargetObject = new GameObject();
            throwTargetObject.transform.position = transform.position + Vector3.right * 5.0f;

            Hammer hammer = Instantiate(hammerPrefab, transform.position, Quaternion.identity);
            hammerObject = hammer.gameObject;
            hammer.Setup(throwTargetObject.transform, transform, AttackDone);
        }

        Vector3 vel = GetVelocity();
        Move(vel);
    }

    private Vector3 GetVelocity()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector3(horizontal, 0, vertical).normalized * speed * Time.deltaTime;
    }

    private bool GetAttackInput()
    {
        return Input.GetButtonDown("Fire1");
    }

    private void Move(Vector3 velocity)
    {
        transform.position = transform.position + velocity;
    }

    private void AttackDone()
    {
        canAttack = true;
        Destroy(hammerObject);
        Destroy(throwTargetObject);
    }
}
