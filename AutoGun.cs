using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : MonoBehaviour
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
        if(gm.paused) return;

        shotTimer += Time.deltaTime;
        if(shotTimer >= shotInterval)
        {
            shotTimer = 0;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, mask);

            if(hits.Length <= 0) return;

            Transform target = null;
            float distance = float.MaxValue;

            for(int i = 0; i < hits.Length; i++)
            {
                float d = Vector3.Distance(hits[i].gameObject.transform.position, 
                transform.position);

                if (d < distance)
                {
                    target = hits[i].gameObject.transform;
                    distance = d;
                }

            }

            //spawn projectile 
            Projectile p = Instantiate(projectilePrefab, transform.position, 
            Quaternion.identity).GetComponent<Projectile>();
        
            //set projectile direction 
            p.SetDirection(target.position - transform.position);

            //set projectile damage  
            p.SetDamage(5);

            p.gm = gm;
        }
    }
}
