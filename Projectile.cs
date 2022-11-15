using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameManager gm;

    public Rigidbody2D rb2d;
    public float speed;
    public float damage;
    public float lifetime;

    private Vector2 savedVelocity;

    // Start is called before the first frame update
    void Awake()
    { 
        rb2d = GetComponent<Rigidbody2D>(); //this gets the rigid body component from this game object
        if (rb2d == null){ //if it didnt find a rigidbody component
            Debug.LogError("No rigidbody2D found on this game object!");
        }
        
    }

    
    void Update()
    {
        if (gm.paused) 
        {
            rb2d.velocity = Vector2.zero;
        }else
        {
            rb2d.velocity = savedVelocity;
            lifetime -= Time.deltaTime;
            if(lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
        

    }

    public void SetDirection(Vector2 dir)
    {
        rb2d.velocity = (dir.normalized * speed);
        savedVelocity = (dir.normalized * speed);

    }

    public void SetDamage(float dam)
    {
        damage = dam;

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //try to do damage
        BotController bc = collision.gameObject.GetComponent<BotController>();
        if(bc != null)
        {
            bc.TakeDamage(damage);
        }

        //destroy yourself 
        Destroy(gameObject);



    }
}
