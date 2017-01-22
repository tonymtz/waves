using UnityEngine;
using UnityEngine.UI;

public class Happymeter : MonoBehaviour
{
    [SerializeField]
    private WaveController WC;

    [SerializeField]
    private int maxHappiness = 20;

    private Slider mySlider;

    private void Awake()
    {
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        mySlider.value = maxHappiness - WC.TrashInWorld;
    }
}
