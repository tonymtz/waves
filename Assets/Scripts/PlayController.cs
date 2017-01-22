using UnityEngine;

public class PlayController : MonoBehaviour
{
    [SerializeField]
    private Level[] levels;

    private int currentLevel;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    public class Level
    {
        public int trashToThrow;
        public int timeBetweenWaves;
        public int wavesBeforeNextLvl;
    }
}
