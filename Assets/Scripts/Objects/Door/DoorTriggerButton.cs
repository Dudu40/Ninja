using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{
    [SerializeField] private GameObject doorGameObject;
    private IDoor door;
    private float timer;

    private void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                door.CloseDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider != null && !collider.CompareTag("PlayerAttackPosition"))
        {
            door.OpenDoor();
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider != null)
        {
            timer = 0.25f;
        }
    }
}
