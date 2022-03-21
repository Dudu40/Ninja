using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject buttonPressed;
    [SerializeField] GameObject buttonNormal;

    [SerializeField] public bool isPressed;

    private void Start()
    {
        //set switch to off sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = buttonNormal.GetComponent<SpriteRenderer>().sprite;
        isPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider != null && !collider.CompareTag("PlayerAttackPosition"))
        {
            //AudioManager.instance.Play("Switch");
            gameObject.GetComponent<SpriteRenderer>().sprite = buttonPressed.GetComponent<SpriteRenderer>().sprite;
            isPressed = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider != null )
        {
            //AudioManager.instance.Play("Switch");
            gameObject.GetComponent<SpriteRenderer>().sprite = buttonNormal.GetComponent<SpriteRenderer>().sprite;
            isPressed = false;
        }
            
    }
   
}
