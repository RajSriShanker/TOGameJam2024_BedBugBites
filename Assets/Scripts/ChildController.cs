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

    // Start is called before the first frame update
    void Start()
    {
        deathLocation = GameObject.Find("Death Location").transform;
        childManager = GameObject.Find("Child Manager").GetComponent<ChildManager>();
        localScale = transform.localScale;
        isUsedAsProjectile = false;
        isPickedUp = false;

    }

    void Update()
    {

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
        EnsureGameObjectIsInList();
        index = childManager.numberOfChildren.IndexOf(gameObject);
        addtionalXOffset = index * 0.5f;
        float adjustedXOffset = followXOffset + addtionalXOffset;

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
        transform.position = deathLocation.position;
        isPickedUp = false;
        target = null;
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
