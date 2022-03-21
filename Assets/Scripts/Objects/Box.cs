using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    //[SerializeField]
    //private ParticleSystem boxDust;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(rb.velocity.x != 0)
        //{
        //    CreateBoxDust();
        //}
    }

    //public void CreateBoxDust()
    //{
     //   boxDust.Play();
    //}
}
