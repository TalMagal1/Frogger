using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveDistance = 1f;
    //private UIManager _uiManager;
    public static PlayerController Instance { get; private set; }

    public float UpwardProgress { get; set; }

    public void RestartScore()
    {
        UpwardProgress = 0;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        HandleInput();
        HandlePlayerLocationForWinnigGame();
    }

    private void Start()
    {
        //_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void HandlePlayerLocationForWinnigGame()
    {
        if (transform.position.y >= 82f)
        {
            //_uiManager.HandleGameWon();
            UIManager.Instance.HandleGameWon();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.up);
            //_uiManager.updateScoreForward();
            UIManager.Instance.updateScoreForward();
            UpwardProgress += moveDistance;  
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.position.y - 1 >= -4.5)
            {
                Move(Vector3.down);
                //_uiManager.updateScoreBackwards();
                UIManager.Instance.updateScoreBackwards();

                UpwardProgress -= moveDistance;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x -1 >= -6)
                Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x + 1 <= 5)
                Move(Vector3.right);
        }
    }
    private void Move(Vector3 direction)
    {
        transform.position += direction * moveDistance;
    }
    
}
