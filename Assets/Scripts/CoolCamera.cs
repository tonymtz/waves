using UnityEngine;
using Tobii.EyeTracking;

public class CoolCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float damping = 1;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Vector3 modifier;

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        GazePoint gazePoint = EyeTracking.GetGazePoint();
        if (gazePoint.IsValid)
        {
            float gazeHorizontalAxis = (gazePoint.Screen.x * 2 / Screen.currentResolution.width) - 1;
            float gazeVerticalAxis = (gazePoint.Screen.y * 2 / Screen.currentResolution.height) - 1;

            modifier = new Vector3(gazeHorizontalAxis * -1, gazeVerticalAxis * -1f, 0f) * 5;
        }
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset + modifier;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.position = position;

        transform.LookAt(target.transform.position);
    }

    public void CameraLook(Vector2 look)
    {
        modifier = new Vector3(look.x, look.y, 0f);
    }
}