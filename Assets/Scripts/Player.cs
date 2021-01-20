using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }

    public float speed = 3f;
    public float jumpForce = 10f;
    public float shotForce = 8f;

    public bool immortality = true;
    public float minimal_height = -5f;

    private Vector3 start_position = new Vector3(0, 0, 0);
    private Vector3 direction;
    private Vector3 lastDirection;

    public bool isJumping;

    public GameObject Bullet;

    //Components:
    public Rigidbody2D rigidbody2d;
    public GroundDetection groundDetection;
    [SerializeField] private Transform bulletSpawn;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Trying to create multiple instances of a singleton " + transform.gameObject.name);
        }
    }

    void Start()
    {
        start_position = transform.position;
        //groundDetection = GetComponent<GroundDetection>();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        direction = Vector3.zero;
        lastDirection = Vector3.left;

        isJumping = false;
    }

    //void FixedUpdate()
    void Update()
    {
        isJumping = isJumping && !groundDetection.isGrounded;

        direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right;
        if (direction != Vector3.zero)
            lastDirection = direction;
        direction *= speed;
        direction.y = rigidbody2d.velocity.y;
        rigidbody2d.velocity = direction;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundDetection.isGrounded)
            {
                rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;

                if (animator != null)
                    animator.SetTrigger("StartJump");
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
            Shot();


        CheckFail();


        if (direction.x > 0)
            spriteRenderer.flipX = true;
        if (direction.x < 0)
            spriteRenderer.flipX = false;
        //

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(direction.x));
            animator.SetBool("IsGrounded", groundDetection.isGrounded);
        }

        if (!isJumping && !groundDetection.isGrounded)
            animator.SetTrigger("StartFall");
    }

    void CheckFail()
    {
        if (transform.position.y < minimal_height)
        {
            if (immortality)
            {
                transform.position = start_position;
                rigidbody2d.velocity = new Vector2(0, 0);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Shot()
    {
        GameObject bullet;
        bullet = Instantiate(Bullet, bulletSpawn.position, Quaternion.identity);

        Bullet bulletComponent = bullet.GetComponent<Bullet>();

        bulletComponent.SetImpulse(lastDirection, shotForce, gameObject);
    }
}
