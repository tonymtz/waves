using UnityEngine;

public class WaveController : MonoBehaviour
{
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

    private int trashInWorld;

    private float timeleft;

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

    private void Awake()
    {
        isTimerPaused = false;
        trashInWorld = 0;
    }

    private void Update()
    {
        if (isTimerPaused) { return; }

        timeleft -= Time.deltaTime;

        if (timeleft <= 0)
        {
            timeleft = digestTime;
            Digest();
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
    }
}
