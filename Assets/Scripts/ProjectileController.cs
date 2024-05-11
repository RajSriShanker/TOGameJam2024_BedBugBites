using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed;

    Rigidbody2D projectileRB;

    // Start is called before the first frame update
    void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        projectileRB.velocity = transform.up * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FlipSprite()
    {
        // Flip the sprite to face the opposite direction
        transform.Rotate(180f, 0f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Child" || other.tag != "Projectile")
        {
            projectileRB.velocity = -projectileRB.velocity;
            FlipSprite();
        }

        return;
    }
}
