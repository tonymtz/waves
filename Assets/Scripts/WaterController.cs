using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField]
    private float tideSpeed;

    [SerializeField]
    private float maxHeight;

    [SerializeField]
    private bool isHighTide;

    private float baseHeight;

    // Use this for initialization
    void Awake()
    {
        baseHeight = transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 newPosition = isHighTide ? IncreaseTideHight(transform.position) : DecreaseTideHight(transform.position);

        transform.position = Vector3.Lerp(transform.position, newPosition, tideSpeed * Time.deltaTime);
    }

    private Vector3 IncreaseTideHight(Vector3 currentPosition)
    {
        if (transform.position.y >= maxHeight) { return currentPosition; }

        Vector3 newPosition = currentPosition;
        newPosition.y += 0.5f;

        return newPosition;
    }

    private Vector3 DecreaseTideHight(Vector3 currentPosition)
    {
        if (transform.position.y <= baseHeight) { return currentPosition; }

        Vector3 newPosition = currentPosition;
        newPosition.y -= 0.5f;

        return newPosition;
    }
}
