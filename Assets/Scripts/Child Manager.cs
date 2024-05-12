using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    public List<GameObject> numberOfChildren = new List<GameObject>();
    public List<GameObject> numberOfProjectileChildren = new List<GameObject>();

    private void Awake()
    {
        int numberOfChildManager = FindObjectsOfType<GameController>().Length;
        if (numberOfChildManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList()
    { 
        numberOfChildren.Add(gameObject);
    }

    public void RemoveFirst()
    {
        if (numberOfChildren.Count > 0)
        {
            numberOfChildren[0].GetComponent<ChildController>()?.UsedAsProjectile();
            numberOfProjectileChildren.Add(numberOfChildren[0]);
            numberOfChildren.RemoveAt(0);
        }
    }

    public void ClearLists()
    { 
        numberOfChildren.Clear();
        numberOfProjectileChildren.Clear();
    }
}
