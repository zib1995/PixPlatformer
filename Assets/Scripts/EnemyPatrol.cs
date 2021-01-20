using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject firstBorder;
    [SerializeField] private GameObject secondBorder;
    private GameObject leftBorder;
    private GameObject rightBorder;

    public bool isRightDirection = true;
    public float speed = 1f;

    private Vector3 direction;

    //Components:
    private Rigidbody2D rigidbody2d;
    private GroundDetection groundDetection;
    private SpriteRenderer spriteRenderer;
    private TouchDamage touchDamage;


    void Start()
    {
        groundDetection = GetComponent<GroundDetection>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        touchDamage = GetComponent<TouchDamage>();

        //BoundaryDefinition
        leftBorder = firstBorder;
        rightBorder = secondBorder;
        if (firstBorder.transform.position.x > secondBorder.transform.position.x)
        {
            leftBorder = secondBorder;
            rightBorder = firstBorder;
        }
    }

    void Update()
    {
        //Navigation

        if (touchDamage.Direction.x > 0)
        {
            isRightDirection = true;
            touchDamage.ResetDirection();
        }
        if (touchDamage.Direction.x < 0)
        {
            isRightDirection = false;
            touchDamage.ResetDirection();
        }

        if (transform.position.x > rightBorder.transform.position.x)
            isRightDirection = false;
        if (transform.position.x < leftBorder.transform.position.x)
            isRightDirection = true;


        //Movement
        direction = Vector3.zero;

        direction = isRightDirection ? Vector3.right : Vector3.left;

        direction *= speed;
        direction.y = rigidbody2d.velocity.y;
        rigidbody2d.velocity = direction;


        //Spriting
        if (isRightDirection)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
