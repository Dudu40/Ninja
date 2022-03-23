using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
  public Player player = null;
  public  List<Image> hearts = new List<Image>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    async void Update()
    {
      if(player.currentHealth==2){
        this.hearts[2].gameObject.SetActive(false);
      }
      if(player.currentHealth==1){
        this.hearts[2].gameObject.SetActive(false);
        this.hearts[1].gameObject.SetActive(false);
      }
      if(player.currentHealth==0){
        this.hearts[2].gameObject.SetActive(false);
        this.hearts[1].gameObject.SetActive(false);
        this.hearts[0].gameObject.SetActive(false);
      }
    }   
}
