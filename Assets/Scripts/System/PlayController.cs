using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayController : MonoBehaviour
{
    [SerializeField]
    private Level[] levels;

    [SerializeField]
    private Player PLAYER;

    private int currentLevel = -1;

    private int wavesCounter = 0;

    [SerializeField]
    private WaterController water;

    [SerializeField]
    private GameObject[] trashCollection;

    [SerializeField]
    private int trashWaveSize = 5;

    [SerializeField]
    protected bool isTimerPaused = true;

    [SerializeField]
    private float digestTime = 30f;

    [SerializeField]
    private int happinessAdded;

    private int trashInWorld;

    private float timeleft;

    private bool isGameOver;

    private bool hasGameStarted;

    public int TrashInWorld
    {
        get
        {
            return trashInWorld;
        }

        set
        {
            trashInWorld = value;
        }
    }

    public float Timeleft
    {
        get
        {
            return timeleft;
        }
    }

    public float DigestTime
    {
        get
        {
            return digestTime;
        }
    }

    public bool HasGameStarted
    {
        get
        {
            return hasGameStarted;
        }
    }

    public int HappinessAdded
    {
        get
        {
            return happinessAdded;
        }

        set
        {
            happinessAdded = value;
        }
    }

    private void Awake()
    {
        isTimerPaused = false;
        trashInWorld = 0;
        FinishRound();
    }

    private void Update()
    {
        if (!hasGameStarted)
        {
            if (Input.GetButtonDown("Action"))
            {
                hasGameStarted = true;
            }

            return;
        }

        if (isGameOver)
        {
            if (Input.GetButtonDown("Action"))
            {
                RestartScene();
            }
            return;
        }

        if (isTimerPaused) { return; }

        timeleft -= Time.deltaTime;

        if (timeleft <= 0)
        {
            Digest();
            timeleft = digestTime;
        }
    }

    private void Digest()
    {
        water.IsHighTide = true;
        Invoke("SpawnTrash", 3f);
        Invoke("ResetWater", 3.5f);
    }

    private void SpawnTrash()
    {
        for (int idx = 0; idx < trashWaveSize; idx += 1)
        {
            Vector3 randomPoint = new Vector3(Random.Range(48, 95), 4f, Random.Range(99, 124));
            GameObject newTrash = Instantiate(trashCollection[Random.Range(0, trashCollection.Length)]);
            newTrash.transform.position = randomPoint;
        }

        trashInWorld += trashWaveSize;
    }

    private void ResetWater()
    {
        water.IsHighTide = false;
        wavesCounter++;

        if (wavesCounter >= levels[currentLevel].wavesBeforeNextLvl)
        {
            FinishRound();
        }
    }

    private void FinishRound()
    {
        currentLevel++;

        if (currentLevel >= levels.Length)
        {
            currentLevel = levels.Length - 1;
        }

        Level myLevel = levels[currentLevel];

        trashWaveSize = myLevel.trashToThrow;
        digestTime = myLevel.timeBetweenWaves;
        wavesCounter = 0;
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        if (isGameOver) { return; }
        isGameOver = true;
        PLAYER.GameOver();
    }

    [System.Serializable]
    public class Level
    {
        public int trashToThrow;
        public int timeBetweenWaves;
        public int wavesBeforeNextLvl;
    }
}
