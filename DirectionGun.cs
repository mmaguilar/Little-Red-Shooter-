using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shotTimer;
    public float shotInterval;

    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<PlayerController>().gm;
        shotTimer = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.paused) return;

        shotTimer += Time.deltaTime;
        if(shotTimer >= shotInterval)
        {
            shotTimer = 0;
            //spawn projectile 
            Projectile p = Instantiate(projectilePrefab, transform.position, 
            Quaternion.identity).GetComponent<Projectile>();

            //set projectile direction 
            p.SetDirection(Vector2.up);

            //set projectile damage  
            p.SetDamage(10);

            p.gm = gm;

        }
    }
}
