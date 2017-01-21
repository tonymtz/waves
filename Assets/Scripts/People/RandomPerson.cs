using UnityEngine;

public abstract class RandomPerson : MonoBehaviour
{
    [SerializeField]
    protected GameObject item;

    [SerializeField]
    protected bool isTimerPaused = true;

    [SerializeField]
    protected float digestTime = 1f;

    protected float timeleft;

    protected void InitializeTimer()
    {
        timeleft = digestTime;
        isTimerPaused = false;
    }

    protected void Tick()
    {
        if (isTimerPaused) { return; }

        timeleft -= Time.deltaTime;

        if (timeleft <= 0)
        {
            timeleft = digestTime;
            Digest();
        }
    }

    protected virtual void Digest()
    {
        Debug.LogWarning("Override this");
    }
}
