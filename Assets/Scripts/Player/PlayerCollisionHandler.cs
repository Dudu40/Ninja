using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private bool invincible;
    private int currentHealth;
   
    public PlayerData playerData;
    private PlayerStateMachine stateMachine;

    private Rigidbody2D RB;

    void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentHealth = this.GetComponent<Player>().currentHealth;
    }

    private void AddForceUp()
    {
        RB.AddForce(new Vector2(0,10), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.CompareTag("Traps"))
            {
                if(currentHealth != 1)
                {
                    AddForceUp();
                }
                
                GetComponent<Player>().TakeDamage();
                StartCoroutine(Invulnerability());
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {

            }
        }
    }

    IEnumerator Invulnerability()
    {
        invincible = true;
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(playerData.invincibilityTime);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        invincible = false;
    }
}
