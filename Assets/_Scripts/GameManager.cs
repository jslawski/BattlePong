using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject ballPrefab;

    private float ballSpawnXPosition = 5f;

    public List<PlayerPaddle> playerPaddles;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this.SetupPlayerPaddles();

        this.LoadResources();
    }

    private void SetupPlayerPaddles()
    {
        this.playerPaddles = new List<PlayerPaddle>();
        this.playerPaddles.Add(GameObject.Find("Player1").GetComponent<PlayerPaddle>());
        this.playerPaddles.Add(GameObject.Find("Player2").GetComponent<PlayerPaddle>());
    }

    private void LoadResources()
    {
        this.ballPrefab = Resources.Load<GameObject>("Prefabs/Ball");
    }

    public void SpawnBall(Player owningPlayer)
    {
        Vector3 spawnPosition = Vector3.zero;

        switch (owningPlayer)
        {
            case Player.Player1:
                spawnPosition = new Vector3(-this.ballSpawnXPosition, 0.0f, 0.0f);
                break;
            case Player.Player2:
                spawnPosition = new Vector3(this.ballSpawnXPosition, 0.0f, 0.0f);
                break;
            default:
                Debug.LogError("Unknown player ID: " + owningPlayer + " Unable to spawn new ball.");
                break;
        }

        GameObject ballInstance = Instantiate(this.ballPrefab, spawnPosition, new Quaternion()) as GameObject;
        Ball ballComponent = ballInstance.GetComponent<Ball>();
        ballComponent.SetupBall(owningPlayer);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            this.SpawnBall(Player.Player1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            this.SpawnBall(Player.Player2);
        }
    }
}
