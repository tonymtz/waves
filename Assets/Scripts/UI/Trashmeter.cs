using UnityEngine;
using UnityEngine.UI;

public class Trashmeter : MonoBehaviour
{
    [SerializeField]
    private Player PLAYER;

    private Slider mySlider;

    private void Awake()
    {
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        mySlider.value = PLAYER.GarbageCollected;
    }
}
