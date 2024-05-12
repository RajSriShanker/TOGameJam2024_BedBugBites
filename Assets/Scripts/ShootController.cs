using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public int ammoCount;
    public Transform shootLocation;
    public Transform PivotLocation;

    public GameObject projectile;

    ChildManager childManager;
    SoundController soundController;

    // Start is called before the first frame update
    void Start()
    {
        childManager = GameObject.Find("Child Manager").GetComponent<ChildManager>();
        soundController = GameObject.Find("Sound Manager").GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoCount = childManager.numberOfChildren.Count;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            childManager.RemoveFirst();
        }
    }

    void FixedUpdate()
    {
        Aim();
    }

    void Shoot()
    {
        if (ammoCount < 0)
        {
            Debug.Log("No ammo");
            return;
        }

        if (ammoCount > 0)
        {
            soundController.PlayShootAudio();
            Instantiate(projectile, shootLocation.position, shootLocation.rotation);
        }
    }

    void Aim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(PivotLocation.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;

        if (angle > 45 && angle < 135)
        {
            PivotLocation.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (angle < -45 && angle > -135)
        {
            PivotLocation.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        else if (angle > 135 || angle < -135)
        {
            PivotLocation.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else if (angle < 45 && angle > -45)
        {
            PivotLocation.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }
}
