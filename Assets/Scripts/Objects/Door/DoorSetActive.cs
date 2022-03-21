using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour, IDoor
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    public void OpenDoor()
    {
        animator.SetBool("isOpen", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("isOpen", false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider != null)
        {
            animator.SetBool("isLocked", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider != null)
        {
            animator.SetBool("isLocked", false);
        }
    }
}
