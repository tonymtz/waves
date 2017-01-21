using UnityEngine;

public class LyingGuy : RandomPerson
{
    [SerializeField]
    private bool isComplaining;

    void Awake()
    {
        isTimerPaused = true;
    }

    void Update()
    {

    }

    protected override void Digest()
    {
        isComplaining = false;
    }

    public void Complain()
    {
        isComplaining = true;
    }
}
