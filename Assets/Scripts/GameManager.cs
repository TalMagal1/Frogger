// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance { get; private set; }
//     [SerializeField] private GameObject playerPrefab;
//     [SerializeField] private GameObject startButton;
//     private GameObject playerObject;
//
//     //[SerializeField] private GameObject stopButton;
//     [SerializeField] private Vector3 playerStartPosition = new Vector3(0, -4.5f, -1f);
//     private PlayerController _playerController;
//     private int _highScore;
//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
//     void Start()
//     {
//         startButton.SetActive(true);
//         //StartGame();
//     }
//     public void StartGame()
//     {
//         startButton.SetActive(false);
//         SpawnPlayer();
//     }
//  
//     private void SpawnPlayer()
//     {
//          playerObject = Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
//         _playerController = playerObject.GetComponent<PlayerController>();
//     }
//     public void EndGame()
//     {
//         Debug.Log("Game Ended");
//         // Add any game-ending logic here, such as:
//         // - Saving high scores
//         // - Showing a game over screen
//         Application.Quit();
//     }
//
//     public void HandlePlayerCollision(float upwardProgress)
//     {   
//         var currentScore = Mathf.FloorToInt(upwardProgress);
//         UIManager.Instance.ShowGameOver();
//         if (currentScore > _highScore)
//         {
//             _highScore = currentScore;
//             
//             //StartCoroutine(AskForPlayerName());
//             // TODO: Handle player UI for finish the game or continue
//         }
//         else
//         {
//             EndGame();
//         }
//     }
//     public void RestartGame()
//     {
//         // This will reload the current scene to restart the game
//         Destroy(playerObject);
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//         StartGame();
//     }
// }
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject startButton;
    private bool shouldReload = false;
    private GameObject playerObject;

    [SerializeField] private Vector3 playerStartPosition = new Vector3(0, -4.5f, -1f);
    private PlayerController _playerController;
    private int _highScore;

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

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (shouldReload)
        {
            StartGame(); // Start the game after the new scene has loaded
        }
        else
        {
            UIManager.Instance.ShowMainMenu();
        }
    }

    void Start()
    {
        startButton.SetActive(true);
    }
   

    public void StartGame()
    {
        startButton.SetActive(false);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (playerObject != null)
        {
            Destroy(playerObject); // Ensure the old player is destroyed before spawning a new one
        }
        
        playerObject = Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
        _playerController = playerObject.GetComponent<PlayerController>();
        Camera.main.GetComponent<CameraController>().SetPlayer(playerObject);

    }

    public void EndGame()
    {
        Debug.Log("Game Ended");
        // Add any game-ending logic here, such as:
        // - Saving high scores
        // - Showing a game over screen
        Application.Quit();
    }

    public void HandlePlayerCollision(float upwardProgress)
    {
        var currentScore = Mathf.FloorToInt(upwardProgress);
        UIManager.Instance.ShowGameOver();
        if (currentScore > _highScore)
        {
            _highScore = currentScore;
        }
        else
        {
            EndGame();
        }
    }

    public void RestartGame()
    {
        UIManager.Instance.RestartScore();
        PlayerController.Instance.RestartScore();      
        Destroy(playerObject); // Destroy the current player before restarting
        shouldReload = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the 
    }

    public void ReturnToMainMenu()
    {
        UIManager.Instance.RestartScore();
        PlayerController.Instance.RestartScore();      
        Destroy(playerObject); // Destroy the current player before restarting
        shouldReload = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    
    
}