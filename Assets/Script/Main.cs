using UnityEngine;
using UnityEngine.UIElements;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float speed = 100f;

    [SerializeField]
    private GameObject endButton;

    [SerializeField]
    private GameObject endText;

    [SerializeField]
    private GameObject startButton;

    private GameObject storageStartButtonPosition;

    private IEntityGame playerManager;
    private IEntityGame enemyManager;
    private float enemySpawnTimer = 0f;
    private float enemySpawnInterval;

    private Vector3 moveDirection;
    private bool isGameOver = false;
    private bool isStartGame = false;

    private readonly Vector3 spawnEnemyDefault = Helper.ReturnNewVector3(0.07f, 2.8f, 0f);
    private readonly Vector3 spawnPlayerDefault = Helper.ReturnNewVector3(-0.04f, 0.21f, 0f);
    private readonly Vector3 spawnEndButtonDefault = Helper.ReturnNewVector3(-0.5f, -1f, 0f);
    private readonly Vector3 spawnEndTextDefault = Helper.ReturnNewVector3(2.09f, 5f, 0f);
    private readonly Vector3 spawnStartButtonDefault = Helper.ReturnNewVector3(0f, 0f, 0f);


    private void SpawnDefault(IEntityGame player, IEntityGame enemy)
    {
        if (!this.isStartGame) return;
        player.Spawn(this.spawnPlayerDefault);
        enemy.Spawn(this.spawnEnemyDefault);
    }

    private void SpawnStartEntity()
    {
        this.playerManager = new PlayerModel(this.playerPrefab);
        this.enemyManager = new EnemyModel(this.enemyPrefab);
        this.SpawnDefault(this.playerManager, this.enemyManager);
        this.enemySpawnInterval = Random.Range(1f, 2f);
    }

    private void SpawnEnemy()
    {
        if (this.isGameOver || !this.isStartGame) return;

        this.enemySpawnTimer += Time.deltaTime;
        if (this.enemySpawnTimer >= this.enemySpawnInterval)
        {
            this.enemyManager.Spawn(Helper.ViewportToWorld(0.07f, Random.Range(0f, 1f)));
            this.enemySpawnTimer = 0f;
            this.enemySpawnInterval = Random.Range(1f, 2f);
        }
    }

    private bool IsEndGame(float distance = 0.7f)
    {
        return Vector3.Distance(this.playerManager.Instance.transform.position, this.enemyManager.Instance.transform.position) <= distance;
    }

    private void StopGame()
    {
        this.isGameOver = true;
        Time.timeScale = 0f;
    }

    private void DisplayEndItems()
    {
        if (this.endButton != null)
        {
            Helper.SpawnStaticFab(this.endButton, this.spawnEndButtonDefault);
        }

        if (this.endText != null)
        {
            Helper.SpawnStaticFab(this.endText, this.spawnEndTextDefault);
        }
    }

    private void EndGame()
    {
        this.StopGame();
        this.playerManager.Instance.SetActive(false);
        this.enemyManager.Instance.SetActive(false);
        this.DisplayEndItems();
    }

    private void PlayingGame()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        this.moveDirection = this.speed * Time.deltaTime * Helper.ReturnNewVector3(moveX, moveY);
        this.playerManager.Move(this.moveDirection);
    }

    private void ClickToStart()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !this.isStartGame)
        {
            this.isStartGame = true;
            Time.timeScale = 1f;
            this.SpawnStartEntity();
            this.storageStartButtonPosition.SetActive(false);
        }
        else return;
    }

    public void Start()
    {
        Time.timeScale = 0f;
        if (this.startButton != null && !this.isStartGame)
        {
            this.storageStartButtonPosition = Helper.SpawnStaticFab(this.startButton, this.spawnStartButtonDefault);
        }
    }

    public void Update()
    {
        this.ClickToStart();
        if (!this.isStartGame) return;

        this.SpawnEnemy();

        if (!this.isGameOver)
        {
            this.PlayingGame();
        }

        if (this.playerManager.Instance != null && this.enemyManager.Instance != null && !this.isGameOver)
        {
            Debug.Log("Distance: " + Vector3.Distance(this.playerManager.Instance.transform.position, this.enemyManager.Instance.transform.position));
            if (this.IsEndGame(2f))
            {
                this.EndGame();
            }
        }
    }
}