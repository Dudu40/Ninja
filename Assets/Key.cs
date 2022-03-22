using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<Player>().hasKey = true;
            GameObject.Destroy(this.gameObject);
            AudioManager.instance.Play("Coin");
            //Add it to UI

        }
        if(collision.CompareTag("Key"))
        {
            Debug.Log("oue");
        }
    }
}
