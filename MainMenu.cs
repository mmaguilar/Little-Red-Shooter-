using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
   
    public void startGame()
    {
        Debug.Log("start, running scene 1");
        SceneManager.LoadScene(1);
    }

}
