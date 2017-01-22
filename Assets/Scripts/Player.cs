using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 100f;

    [SerializeField]
    private float rotatingSpeed = 5f;

    [SerializeField]
    private WaveController WC;

    private Rigidbody myRigidbody;

    private bool canAction;

    private GameObject itemSelected;

    private int garbageCollected;

    public int GarbageCollected
    {
        get
        {
            return garbageCollected;
        }
    }

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float axisHorizontal = Input.GetAxis("Horizontal");
        float axisVertical = Input.GetAxis("Vertical");
        bool isActionButton = Input.GetButtonDown("Action");

        Move(axisHorizontal, axisVertical);

        if (isActionButton)
        {
            Action();
        }
    }

    private void Move(float axisHorizontal, float axisVertical)
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

    private void Action()
    {
        canAction = false;
        Invoke("ActionTeardown", 0.5f);

        if (itemSelected && garbageCollected < 11)
        {
            Destroy(itemSelected);
            WC.TrashInWorld--;
            garbageCollected++;
        }
    }

    private void ActionTeardown()
    {
        canAction = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) // Trash
        {
            if (itemSelected == null)
            {
                itemSelected = other.gameObject;
            }
        }
        else if (other.gameObject.tag == "TrashCan")
        {
            garbageCollected = 0;
        }
        else if (other.gameObject.tag == "LyingGuy")
        {
            other.GetComponent<LyingGuy>().Complain();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 && // Trash
            other.gameObject == itemSelected)
        {
            itemSelected = null;
        }
    }
}
