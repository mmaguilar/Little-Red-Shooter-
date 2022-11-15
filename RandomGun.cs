using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shotTimer;
    public float shotInterval;

    public float range;

    public LayerMask mask;

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
            
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, mask);

            if(hits.Length <= 0) return;

            Transform target = hits[Random.Range(0, hits.Length)].transform;

            //spawn projectile 
            Projectile p = Instantiate(projectilePrefab, transform.position, 
            Quaternion.identity).GetComponent<Projectile>();

            //aim at target 
            p.SetDirection(target.position - transform.position);

            //set projectile damage 
            p.SetDamage(10);

            p.gm = gm;

        }
    
    }
}
