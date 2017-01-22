using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickPowerupSound;

    [SerializeField]
    private AudioClip pickTrashSound;

    [SerializeField]
    private AudioClip tossGarbageSound;

    [SerializeField]
    private AudioClip attackSound;

    [SerializeField]
    private AudioClip randomComplainSound;

    private AudioManager AM;

    void Awake()
    {
        AM = AudioManager.Instance;
    }

    public void PickPowerup()
    {
        AM.PlaySound(pickPowerupSound);
    }

    public void PickTrash()
    {
        AM.PlaySound(pickTrashSound);
    }

    public void TossGarbage()
    {
        AM.PlaySound(tossGarbageSound);
    }

    public void Attack()
    {
        AM.PlaySound(attackSound);
    }

    public void RandomComplain()
    {
        AM.PlaySound(randomComplainSound);
    }
}
