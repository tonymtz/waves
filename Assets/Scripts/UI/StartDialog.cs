using UnityEngine;

public class StartDialog : MonoBehaviour
{
    [SerializeField]
    private PlayController WC;

    private void LateUpdate()
    {
        if (WC.HasGameStarted)
        {
            gameObject.SetActive(false);
        }
    }
}
