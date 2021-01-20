using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private bool destroyAfterTouch = false;
    [SerializeField] private GameObject parent;

    //public string collisionTag;

    Vector3 direction;

    private Health health;
    private Animator animator;

    public Vector3 Direction
    {
        get { return direction; }
    }

    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //Debug.Log(collision.gameObject.name);

        //if (collision.gameObject.CompareTag(collisionTag)) { }

        health = collision.gameObject.GetComponent<Health>();

        if (health)
        {
            direction = collision.transform.position - transform.position;
            //direction.x

            if (animator != null)
                animator.SetTrigger("Damage");

            SetDamage();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == parent)
        {
            Debug.Log("Enter to parent object.");
            return;
        }

        health = collision.gameObject.GetComponent<Health>();

        if (health)
        {
            direction = collision.transform.position - transform.position;
            //direction.x

            if (animator != null)
                animator.SetTrigger("Damage");

            SetDamage();
        }

        if (destroyAfterTouch)
            Destroy(gameObject);
    }

    public void SetDamage()
    {
        health.TakeHit(damage);
        health = null;
    }

    public void ResetDirection()
    {
        direction = Vector3.zero;
    }
}
