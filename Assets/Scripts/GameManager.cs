using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameObject player;
    private bool playerIsDead = false;
    public float restartDelay = 2f;
    //public Vector2 lastCheckPointPos;
    private int nextSceneToLoad;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void Respawn()
    {
        if(!playerIsDead)
        {
            playerIsDead = true;  
            //Respawn
            Invoke("Restart", restartDelay);
        }
        
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }

   public void LoadLevel()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }
}
