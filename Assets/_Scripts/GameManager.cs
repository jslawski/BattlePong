using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject ballPrefab;
    private GameObject particleSignalPrefab;

    private float ballSpawnXPosition = 5f;

    public List<PlayerPaddle> playerPaddles;

    private float powerUpTime = 90f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this.SetupPlayerPaddles();

        this.LoadResources();

        Invoke("StartGame", 3.0f);
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
        this.particleSignalPrefab = Resources.Load<GameObject>("Prefabs/ParticleSignal");
    }

    private void StartGame()
    {
        this.SpawnBall(Player.Player1);
        this.SpawnBall(Player.Player2);
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

    public void RespawnBall(Player owningPlayer)
    {
        StartCoroutine(this.RespawnBallAfterDelay(owningPlayer));
    }

    public IEnumerator RespawnBallAfterDelay(Player owningPlayer)
    {
        yield return null;

        bool ownerBallExists = false;

        //Make sure that no other balls exist in the scene that belong to the player
        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < allBalls.Length; i++)
        {
            Ball curBall = allBalls[i].GetComponent<Ball>();
            if (curBall != null && curBall.owningPlayer == owningPlayer)
            {
                ownerBallExists = true;
            }
        }

        if (ownerBallExists == false)
        {
            yield return new WaitForSeconds(3.0f);
            this.SpawnBall(owningPlayer);
        }
    }

    public void SpawnParticleSignal(Vector3 spawnPoint, Player owningPlayer, Player targetPlayer)
    {
        GameObject particleSignalInstance = Instantiate(this.particleSignalPrefab, spawnPoint, new Quaternion()) as GameObject;
        ParticleSignal particleSignalComponent = particleSignalInstance.GetComponent<ParticleSignal>();
        particleSignalComponent.SetupParticleSignal(this.playerPaddles[(int)targetPlayer].gameObject, owningPlayer);
    }

    //Power-Up Functions
    #region Power-Up Functions
    public void ChangeSpeedOfPlayer(Player targetPlayer, float speedChange)
    {
        StartCoroutine(this.ChangeSpeedOfPlayerTimed(targetPlayer, speedChange));
    }

    private IEnumerator ChangeSpeedOfPlayerTimed(Player targetPlayer, float speedChange)
    {
        this.playerPaddles[(int)targetPlayer].moveSpeed *= speedChange;       

        yield return new WaitForSeconds(this.powerUpTime);

        this.playerPaddles[(int)targetPlayer].moveSpeed /= speedChange;
    }

    public void ReverseControlsOfPlayer(Player targetPlayer)
    {
        if (this.playerPaddles[(int)targetPlayer].moveSpeed < 0)
        {
            return;
        }

        StartCoroutine(this.ReverseControlsOfPlayerTimed(targetPlayer));
    }

    private IEnumerator ReverseControlsOfPlayerTimed(Player targetPlayer)
    {
        this.playerPaddles[(int)targetPlayer].moveSpeed *= -1;

        yield return new WaitForSeconds(this.powerUpTime);

        this.playerPaddles[(int)targetPlayer].moveSpeed *= -1;
    }

    public void ChangeSizeOfPlayer(Player targetPlayer, float newSize)
    {
        if (this.playerPaddles[(int)targetPlayer].transform.localScale.y == newSize)
        {
            return;
        }

        StartCoroutine(this.ChangeSizeOfPlayerTimed(targetPlayer, newSize));
    }

    private IEnumerator ChangeSizeOfPlayerTimed(Player targetPlayer, float newSize)
    {        
        this.playerPaddles[(int)targetPlayer].transform.localScale = new Vector3(0.5f, newSize, 1.0f);

        yield return new WaitForSeconds(this.powerUpTime);

        this.playerPaddles[(int)targetPlayer].transform.localScale = new Vector3(0.5f, 2.0f, 1.0f);
    }

    public void DuplicateBall(Player owningPlayer, Vector3 spawnPosition)
    {
        GameObject ballInstance = Instantiate(this.ballPrefab, spawnPosition, new Quaternion()) as GameObject;
        Ball ballComponent = ballInstance.GetComponent<Ball>();
        ballComponent.SetupBall(owningPlayer);
    }

    #endregion    
}
