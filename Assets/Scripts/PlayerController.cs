using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private Hammer hammerPrefab;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float invulnerabilityTime = 1.5f;
    [SerializeField] private Material frontSprite;
    [SerializeField] private Material backSprite;
    [SerializeField] private GameObject graphics;
    [SerializeField] private GameObject flipPivot;
    [SerializeField] private int maxLives = 3;

    private Rigidbody rb;
    private Animator animator;
    private MeshRenderer meshRenderer;

    private bool canAttack = true;
    private bool canBeHurt = true;
    private GameObject throwTargetObject;
    private GameObject hammerObject;
    private int lives;
    private bool dead = false;

    public void Hurt()
    {
        lives = lives - 1;
        Debug.Log(lives);
        if (lives == 0)
        {
            Defeat();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        meshRenderer = graphics.GetComponent<MeshRenderer>();

        lives = maxLives;
    }

    private void Update()
    {
        if (dead) return;

        bool attack = GetAttackInput();
        if (canAttack && attack)
        {
            ThrowHammer();
        }

        UpdateSprite();
    }

    private void FixedUpdate()
    {
        if (dead) return;

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

    private void UpdateSprite()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 scale = flipPivot.transform.localScale;
        if (horizontal > 0)
        {
            scale.x = -1;
        }
        else if (horizontal < 0)
        {
            scale.x = 1;
        }

        flipPivot.transform.localScale = scale;

        if (vertical > 0)
            meshRenderer.material = backSprite;
        else if (vertical < 0)
            meshRenderer.material = frontSprite;
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

    private void Defeat()
    {
        animator.SetTrigger("Death");
        dead = true;
    }

    private void AttackDone()
    {
        canAttack = true;
        animator.SetTrigger("HammerCaught");
        Destroy(hammerObject);
        Destroy(throwTargetObject);
    }
}
