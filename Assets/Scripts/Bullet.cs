using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float lifeTime = 3;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private TouchDamage touchDamage;

    public float Force
    {
        get { return force; }
        set { force = value; }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void SetImpulse(Vector2 direction, float force, GameObject parent)
    {
        touchDamage.Parent = parent;
        rigidbody2d.AddForce(direction * force, ForceMode2D.Impulse);

        if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        StartCoroutine(StartLife());
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }
}
