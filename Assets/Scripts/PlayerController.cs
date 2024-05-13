using BarthaSzabolcs.Tutorial_SpriteFlash;
using DG.Tweening.Core.Easing;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

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

    private Rigidbody2D playerRB;

    SimpleFlash simpleFlash;
    SoundController soundController;
    Animator playerAnimator;
    GameController gameManager;

    void Start()
    {
        soundController = GameObject.Find("Sound Manager").GetComponent<SoundController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameController>();

        localScale = transform.localScale;
        playerRB = GetComponent<Rigidbody2D>();
        simpleFlash = GetComponent<SimpleFlash>();
        playerAnimator = GetComponent<Animator>();

        playerAnimator.SetBool("isRestarted", false);
    }

    void Update()
    {
        Move();
        Jump();

        if (playerRB.velocity.y < 0)
        {
            playerAnimator.SetBool("isFalling", true);
        }
        else
        {
            playerAnimator.SetBool("isFalling", false);
        }

        if (gameManager.isRestarting)
        {
            Death();
        }
        else
        {
            playerAnimator.SetBool("isRestarted", false);
        }
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
            playerAnimator.SetBool("IsJumping", true);
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
        playerAnimator.SetBool("IsJumping", false);
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    IEnumerator KnockBack()
    {
        soundController.PlayDazedAudio();
        isKnockBacked = true;
        Vector2 direction = (transform.position - enemyKnockDirection.position).normalized;
        playerRB.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        simpleFlash.Flash();
        playerAnimator.SetBool("isHurt", true);
        yield return new WaitForSeconds(knockBackTime);
        isKnockBacked = false;
        playerAnimator.SetBool("isHurt", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Projectile") && !isKnockBacked)
        {
            enemyKnockDirection = collision.transform;
            StartCoroutine(KnockBack());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isBurned();
        }
    }

    public void isBurned()
    { 
        gameManager.RestartCalling();
    }


    public void Death()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        playerAnimator.SetBool("isRestarted", true);
    }

}
