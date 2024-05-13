using UnityEngine;

public class ChildController : MonoBehaviour
{
    public bool isPickedUp;
    public float followXOffset = 1f;
    public float addtionalXOffset = 1f;
    public float followYOffset = 1f;
    public float followSpeed = 0.5f;

    public bool isUsedAsProjectile;

    private Vector3 localScale;

    Transform target;
    Transform deathLocation;
    
    ChildManager childManager;
    [SerializeField] int index;

    SpriteRenderer spriteRenderer;
    Collider2D collider2D;
    Animator childAnimator;

    // Start is called before the first frame update
    void Start()
    {
        deathLocation = GameObject.Find("Death Location").transform;
        childManager = GameObject.Find("Child Manager").GetComponent<ChildManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        childAnimator = GetComponent<Animator>();
        localScale = transform.localScale;
        isUsedAsProjectile = false;
        isPickedUp = false;

    }

    void Update()
    {
        //if the child is moving left or right, set animator bool to true
        if (transform.position.x > transform.position.x + 0.1f || transform.position.x < transform.position.x - 0.1f)
        {
            childAnimator.SetBool("isMoving", true);
        }
        else
        {
            childAnimator.SetBool("isMoving", false);
        }

        //if the child is moving upwards, set animator bool to true
        if (transform.position.y > transform.position.y + 0.1f || transform.position.y < transform.position.y - 0.1f)
        {
            childAnimator.SetBool("isJumping", true);
        }
        else
        {
            childAnimator.SetBool("isJumping", false);
        }
    }

    private void LateUpdate()
    {
        if (isPickedUp && !isUsedAsProjectile)
        {
            Follow();
        }
    }

    void EnsureGameObjectIsInList()
    {
        if (!childManager.numberOfChildren.Contains(gameObject))
        {
            childManager.numberOfChildren.Add(gameObject);
        }
    }

    void Follow()
    {
        if (target == null)
        {
            return;
        }

        EnsureGameObjectIsInList();
        index = childManager.numberOfChildren.IndexOf(gameObject);
        float indexAdjustement = index * addtionalXOffset;
        float adjustedXOffset = followXOffset + indexAdjustement;

        if (target.localScale.x < 0)
        {
            Vector3 targetPosition = new Vector3(target.position.x + adjustedXOffset, target.position.y + followYOffset, target.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }

        //if the target is moving to the left
        if (target.localScale.x > 0)
        {
            Vector3 targetPosition = new Vector3(target.position.x - adjustedXOffset, target.position.y + followYOffset, target.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
    }

     public void UsedAsProjectile()
    { 
        isUsedAsProjectile = true;
        //transform.position = deathLocation.position;
        isPickedUp = false;
        spriteRenderer.enabled = false;
        collider2D.enabled = false; 
    }

    public void Follower()
    {
        isPickedUp = true;
        isUsedAsProjectile = false;
        Follow();
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPickedUp)
        {
            Follower();
            target = collision.gameObject.transform;
        }
    }
}
