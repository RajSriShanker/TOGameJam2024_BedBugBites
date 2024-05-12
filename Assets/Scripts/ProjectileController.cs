using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed;

    Rigidbody2D projectileRB;

    ChildManager childManager;

    Transform deathLocation;

    SoundController soundController;

    // Start is called before the first frame update
    void Start()
    {
        childManager = GameObject.Find("Child Manager").GetComponent<ChildManager>();
        soundController = GameObject.Find("Sound Manager").GetComponent<SoundController>();
        deathLocation = GameObject.Find("Death Location").transform;

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
        if (other.tag == "Toaster" && childManager.numberOfProjectileChildren.Count > 0)
        {
            var childController = childManager.numberOfProjectileChildren[0].GetComponent<ChildController>();
            if (childController != null)
            {
                childController.Follower();
            }
            childManager.numberOfProjectileChildren.RemoveAt(0);
            DisableProjectile();
            return;
        }

        if (other.tag == "Ground" || other.tag == "Platform")
        {
            projectileRB.velocity = -projectileRB.velocity;
            FlipSprite();
            soundController.PlayJumpAudio();
        }
    }

    void DisableProjectile()
    {
        soundController.PlayLoadAudio();
        transform.position = deathLocation.position;
        projectileRB.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
