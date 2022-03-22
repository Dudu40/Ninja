using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Rigidbody contains functions to apply physical movement
    private Rigidbody2D __rigidbody2D = null;
    public Rigidbody2D Rigidbody2D
    {
        get
        {
            if (this.__rigidbody2D == null)
                this.__rigidbody2D = this.GetComponent<Rigidbody2D>();
            return this.__rigidbody2D;
        }
    }

    // Start / End
    public Transform transformStart = null;
    public Transform transformEnd = null;

    // Speed
    public float moveSpeed = 1.0f;
    private float movePercentage = 0.0f;

    // Wait time
    public float waitTime = 0.0f;
    private float waitTimeLeft = 0.0f;

    // Loop behavior ?
    public bool loop = true;

    // Status
    private bool moveDirection = true;      // true: start > end

    // Update is called once per frame
    void Update()
    {
        if (this.waitTimeLeft > 0.0f)
        {
            this.waitTimeLeft -= Time.deltaTime;
            //if (this.waitTimeLeft <= 0.0f)
            //    this.moveDirection = !this.moveDirection;
        }
        else
        {
            // Goes towards end
            if (this.moveDirection == true)
            {
                // Move
                this.movePercentage = Mathf.Clamp01(this.movePercentage + moveSpeed * Time.deltaTime);

                // Reached end position
                if (this.loop == true)
                {
                    // Wait a bit
                    if (this.movePercentage == 1.0f)
                    {
                        // Wait
                        this.waitTimeLeft = this.waitTime;

                        // Invert direction
                        this.moveDirection = !this.moveDirection;
                    }
                }
            }
            // Goes towards start
            else
            {
                // Move
                this.movePercentage = Mathf.Clamp01(this.movePercentage - moveSpeed * Time.deltaTime);

                // Reached start position
                if (this.loop == true)
                {
                    // Wait a bit
                    if (this.movePercentage == 0.0f)
                    {
                        // Wait
                        this.waitTimeLeft = this.waitTime;

                        // Invert direction
                        this.moveDirection = !this.moveDirection;
                    }
                }
            }

            //// Update
            //this.transform.position = (this.transformStart.position + this.movePercentage * (this.transformEnd.position - this.transformStart.position));
        }
    }

    void FixedUpdate()
    {
        if (this.Rigidbody2D != null)
        {
            //// Log
            

            // Move
            this.Rigidbody2D.MovePosition(Vector2.Lerp(this.transformStart.position, this.transformEnd.position, this.movePercentage));
        }
    }

    //    void OnDrawGizmos()
    //    {
    //        Gizmos.DrawWireSphere(this.transformStart.position, 0.5f);
    //        Gizmos.DrawLine(this.transformStart.position, this.transformEnd.position);
    //        Gizmos.DrawWireSphere(this.transformEnd.position, 0.5f);
    //    }
}
