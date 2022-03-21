using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            coinCount++;
            GetComponent<Animator>().SetBool("collected", true);
            AudioManager.instance.Play("Coin");
            //Add it to UI
            
        }
    }
}
