using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameSpeed { get; private set; }
    public float gameSpeedIncrease = 0.1f;
    public float initalGameSped = 5f;
    
    private void Awake()
    {
        if(Instance == null)
            Instance= this;
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
            Instance= null; 
    }

    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        gameSpeed= initalGameSped;
    }
    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }
}
