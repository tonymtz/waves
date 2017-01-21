using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 100f;

    [SerializeField]
    private float rotatingSpeed = 5f;

    private Rigidbody myRigidbody;

    private bool canAction;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float axisHorizontal = Input.GetAxis("Horizontal");
        float axisVertical = Input.GetAxis("Vertical");
        bool isActionButton = Input.GetButton("Action");

        Move(axisHorizontal, axisVertical);

        if (isActionButton)
        {
            Action();
        }
    }

    void Move(float axisHorizontal, float axisVertical)
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(axisHorizontal, 0f, axisVertical), rotatingSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (axisHorizontal != 0f)
        {
            myRigidbody.velocity = new Vector3(
                axisHorizontal * movementSpeed * Time.deltaTime,
                myRigidbody.velocity.y,
                myRigidbody.velocity.z
            );
        }

        if (axisVertical != 0f)
        {
            myRigidbody.velocity = new Vector3(
                myRigidbody.velocity.x,
                myRigidbody.velocity.y,
                axisVertical * movementSpeed * Time.deltaTime
            );
        }
    }

    void Action()
    {
        canAction = false;
        Invoke("HideWeapon", 0.5f);
    }

    private void HideWeapon()
    {
        canAction = true;
    }
}
