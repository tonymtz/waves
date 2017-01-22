using UnityEngine;
using UnityEngine.UI;

public class WavemeterUI : MonoBehaviour
{
    [SerializeField]
    private PlayController WC;

    private Slider mySlider;

    void Awake()
    {
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        mySlider.value = WC.Timeleft * 100 / WC.DigestTime;
    }
}
