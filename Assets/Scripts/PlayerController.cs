using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public float groundCheckRadius;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Vector3 localScale;

    [SerializeField] private float knockBackForce;
    [SerializeField] private float knockBackTime;
    [SerializeField] private bool isKnockBacked;
    private Transform enemyKnockDirection;

    private Collider2D playerCollider;
    private Rigidbody2D playerRB;

    SimpleFlash simpleFlash;
    SoundController soundController;

    void Start()
    {
        soundController = GameObject.Find("Sound Manager").GetComponent<SoundController>();

        localScale = transform.localScale;
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        simpleFlash = GetComponent<SimpleFlash>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        FlipSprite();
    }

    void Move()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector2(moveDirection * moveSpeed, playerRB.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            soundController.PlayJumpAudio();
        }
    }

    void FlipSprite()
    {
        if (playerRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        else if (playerRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    bool isGrounded()
    { 
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    IEnumerator KnockBack()
    {
        isKnockBacked = true;
        Vector2 direction = (transform.position - enemyKnockDirection.position).normalized;
        playerRB.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        simpleFlash.Flash();
        yield return new WaitForSeconds(knockBackTime);
        isKnockBacked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Projectile") && !isKnockBacked)
        {
            enemyKnockDirection = collision.transform;
            StartCoroutine(KnockBack());
        }
    }

}
