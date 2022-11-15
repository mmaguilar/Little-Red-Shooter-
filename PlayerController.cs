using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    public GameManager gm;

    public Rigidbody2D rigidbody2D; //reference to our rigidbody
    public InputActionAsset input; //reference to input action asset 
    public InputAction moveAction;
    public float moveSpeed;
    public Animator anim;
    public float MaxHealth;
    public float CurrentHealth;
    public int coins;
    public TMP_Text coinDisplay;
    public Image healthBar;
    public GameObject gameOverPanel;
    public InputAction pauseAction;
    public GameObject pauseScreen;
    public TMP_Text finalScore;


    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        gameOverPanel.SetActive(false);
        rigidbody2D = GetComponent<Rigidbody2D>(); //this gets the rigid body component from this game object
        if (rigidbody2D == null){ //if it didnt find a rigidbody component
            Debug.LogError("No rigidbody2D found on this game object!");
        }
        moveAction = input.FindAction("Move");
        pauseAction = input.FindAction("Pause");
        pauseAction.performed += OnPauseAction;

        input.Enable();
        
    }

    public void Update()
    {
        if(gm.paused) 
        {
            anim.speed = 0;
            return;
        }
        if(!gm.paused)
        {
            anim.speed = 1;
        }

        coinDisplay.text = "Coins: " + coins;
        healthBar.fillAmount = Mathf.InverseLerp(0, MaxHealth, CurrentHealth);
    }


    public void FixedUpdate()
    {
        if (gm.paused) return;

        //get the current position
        Vector2 newPosition = rigidbody2D.position; 

        //get our input 
        Vector2 direction = moveAction.ReadValue<Vector2>();

        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);

        direction = direction.normalized * Time.deltaTime * moveSpeed;

        newPosition += direction;

        //set the position to the new position
        rigidbody2D.MovePosition(newPosition);

    }

    //load scene for game over 
    
    IEnumerator gameOverSequence(){
        gameOverPanel.SetActive(true);
        finalScore.text = "Final Score: " + coins;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        BotController bc = collision.gameObject.GetComponent<BotController>();
        if(bc != null)
        {
            CurrentHealth -= 5;
            if(CurrentHealth <= 0)
            {
                //end game
                //Applications.LoadLevel(0);
                StartCoroutine(gameOverSequence());
            }
            
        }

        Pickup pu = collision.gameObject.GetComponent<Pickup>();

        if(pu != null)
        {                

            //change stats
            MaxHealth += pu.MaxHealth;
            CurrentHealth += pu.CurrentHealth;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);

            //speed
            if(pu.Speed > 0)
            {
                 moveSpeed *= pu.Speed;
            }

            //cash 
            coins += pu.coins;

            Destroy(collision.gameObject); 
        }
    }

    public void OnPauseAction(InputAction.CallbackContext obj)
    {
        if(gm.paused)
        {
            //unpause
            gm.paused = false;
            pauseScreen.SetActive(false);
        }else
        {
            //pause
            gm.paused = true;
            pauseScreen.SetActive(true);
        }
    }

     public void Reset()
    {
        //Debug.Log("Restart");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Debug.Log("Load Scene 0");
        SceneManager.LoadScene(0);
    }
}
