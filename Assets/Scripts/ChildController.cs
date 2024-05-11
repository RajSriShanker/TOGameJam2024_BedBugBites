using UnityEngine;

public class ChildController : MonoBehaviour
{
    public bool isPickedUp;
    public float followXOffset = 1f;
    public float followYOffset = 1f;
    public float addtionalXOffset = 1f;
    public float followSpeed = 0.5f;

    public bool isUsedAsProjectile;

    private Vector3 localScale;

    Transform target;

    BoxCollider2D childCollider;
    Rigidbody2D childRB;
    [SerializeField] ChildManager childManager;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        isUsedAsProjectile = false;
        isPickedUp = false;
        childCollider = GetComponent<BoxCollider2D>();
        childRB = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        index = childManager.numberOfChildren.IndexOf(gameObject);
        followXOffset = index * addtionalXOffset;
    }

    private void LateUpdate()
    {
        if (isPickedUp && !isUsedAsProjectile)
        {
            Follow();
        }
    }

    void Follow()
    {
        //If the target is moving to the right
        if (target.localScale.x < 0)
        {
            Vector3 targetPosition = new Vector3(target.position.x + addtionalXOffset, target.position.y + followYOffset, target.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }

        //if the target is moving to the left
        if (target.localScale.x > 0)
        {
            Vector3 targetPosition = new Vector3(target.position.x - addtionalXOffset, target.position.y + followYOffset, target.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            isUsedAsProjectile = false;
            childManager.AddToList();
            target = collision.gameObject.transform;
        }
    }
}
