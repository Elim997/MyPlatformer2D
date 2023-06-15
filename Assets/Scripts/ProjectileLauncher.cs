using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{

    public Transform launchPoint;
    public GameObject projectilePrefab;

    public void FirePorjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        Vector3 originScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(originScale.x * transform.localScale.x > 0 ? 1 : -1, originScale.y, originScale.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
