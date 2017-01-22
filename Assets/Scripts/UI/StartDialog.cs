using UnityEngine;

public class StartDialog : MonoBehaviour
{
    [SerializeField]
    private WaveController WC;

    private void LateUpdate()
    {
        if (WC.HasGameStarted)
        {
            gameObject.SetActive(false);
        }
    }
}
