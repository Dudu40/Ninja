using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Player player = null;
    public GameObject inventory = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.coinCount++;
            inventory.GetComponent<Text>().text = player.coinCount.ToString();
            GetComponent<Animator>().SetBool("collected", true);
            AudioManager.instance.Play("Coin");
            
        }
    }
}
