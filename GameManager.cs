using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public GameObject botPrefab;
    float timer;
    public float spawnInterval;

    public GameObject lawn;
    public float lawnSize;

    public float playerMaxX;
    public float playerMaxY;
    public float playerMinX;
    public float playerMinY;

    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) return;

        timer += Time.deltaTime;
        if(timer >= spawnInterval)
        {
            timer = 0;

            //spawn bot
            BotController bc = Instantiate(botPrefab,
            (Random.insideUnitCircle.normalized * 10f) + (Vector2)player.position, 
            Quaternion.identity).GetComponent<BotController>();
            
            bc.SetPlayer(player);
            bc.gm = this;
            

        }

        if(player.transform.position.x > playerMaxX)
        {
            Debug.Log("East");
            lawn.transform.position = lawn.transform.position + (Vector3.right * lawnSize);
            playerMaxX += lawnSize;
            playerMinX += lawnSize;
        }else if(player.transform.position.x < playerMinX)
        {
            Debug.Log("West");
            lawn.transform.position = lawn.transform.position + (Vector3.left * lawnSize);
            playerMaxX -= lawnSize;
            playerMinX -= lawnSize;
        }
        
        if(player.transform.position.y > playerMaxY)
        {
            lawn.transform.position = lawn.transform.position + (Vector3.up * lawnSize);
            playerMaxY += lawnSize;
            playerMinY += lawnSize;
        }else if(player.transform.position.y < playerMinY)
        {
            lawn.transform.position = lawn.transform.position + (Vector3.down * lawnSize);
            playerMaxY -= lawnSize;
            playerMinY -= lawnSize;
        }
    }

}
