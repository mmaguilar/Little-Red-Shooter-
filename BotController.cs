using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public GameManager gm;

    public float speed;

    public Transform player;

    public float health;

    private Rigidbody2D rb;

    public GameObject[] pickups;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null){
            Debug.LogError("No Rigidbody2D found");
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if(gm.paused) 
        {
            anim.speed = 0;
            return;
        }

        if (!gm.paused){
            anim.speed = 1;
        }
        
        Vector2 newPosition = rb.position;
        Vector2 direction = (Vector2)player.position - rb.position;
        direction = direction * Time.deltaTime * speed;

        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
        newPosition += direction;

        rb.MovePosition(newPosition);
    }


    public void SetPlayer(Transform targetPlayer)
    {
        player = targetPlayer;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            //spawnPickUp
            int index = Random.Range(0, pickups.Length);
            Instantiate(pickups[index], transform.position + (Vector3.forward * 1.0f), Quaternion.identity);
            
            Destroy(gameObject);

        }
    }


}
