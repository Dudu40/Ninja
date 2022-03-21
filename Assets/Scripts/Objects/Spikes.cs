using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
         gameObject.GetComponent<Animator>().SetBool("trigger", true);    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<Animator>().SetBool("trigger", false);
    }
}
