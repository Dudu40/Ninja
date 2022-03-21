using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject switchOn;
    [SerializeField] GameObject switchOff;

    [SerializeField] private GameObject doorGameObject;
    private IDoor door;

    [SerializeField] public bool isOn;

    private void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
    }

    private void Start()
    {
        //set switch to off sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
        isOn = false;
    }

    public void ToggleOn()
    {
        //Debug.Log("SWITCH");
        AudioManager.instance.Play("Switch");
        if(!isOn)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;
            door.OpenDoor();
            isOn = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;
            door.CloseDoor();
        }     
    }
}
