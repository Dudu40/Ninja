using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score;
    [SerializeField] private GameObject[] scoreNumbers;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore()
    {
        score++;
        

    }
}
