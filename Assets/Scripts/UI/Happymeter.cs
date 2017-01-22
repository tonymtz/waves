using UnityEngine;
using UnityEngine.UI;

public class Happymeter : MonoBehaviour
{
    [SerializeField]
    private PlayController WC;

    [SerializeField]
    private int maxHappiness = 20;

    private Slider mySlider;

    private void Awake()
    {
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        int newValue = maxHappiness - WC.TrashInWorld + WC.HappinessAdded;
        mySlider.value = newValue;

        if (newValue <= 0)
        {
            WC.GameOver();
        }
    }
}
