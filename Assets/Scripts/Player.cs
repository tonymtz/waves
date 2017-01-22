using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 100f;

    [SerializeField]
    private float rotatingSpeed = 5f;

    [SerializeField]
    private PlayController WC;

    private Rigidbody myRigidbody;

    private bool isActioning;

    private GameObject itemSelected;

    private int garbageCollected;

    private float speedAdded;

    [SerializeField]
    private float backpackCapacity;

    [SerializeField]
    private Animator myAnimator;

    private PlayerSound myPlayerSound;

    public int GarbageCollected
    {
        get
        {
            return garbageCollected;
        }
    }

    public float SpeedAdded
    {
        get
        {
            return speedAdded;
        }
    }

    public float BackpackCapacity
    {
        get
        {
            return backpackCapacity;
        }
    }

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myPlayerSound = GetComponent<PlayerSound>();
    }

    private void Update()
    {
        if (!WC.HasGameStarted) { return; }

        if (isActioning) { return; }

        float axisHorizontal = Input.GetAxis("Horizontal");
        float axisVertical = Input.GetAxis("Vertical");
        bool isActionButton = Input.GetButtonDown("Action");

        if (isActionButton)
        {
            Action();
        }
        else
        {
            Move(axisHorizontal, axisVertical);
        }
    }

    private void Move(float axisHorizontal, float axisVertical)
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(axisHorizontal, 0f, axisVertical), rotatingSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        bool isMoving = false;
        float movementSpeedNet = movementSpeed + (speedAdded - garbageCollected) * 25;

        if (axisHorizontal != 0f)
        {
            myRigidbody.velocity = new Vector3(
                axisHorizontal * movementSpeedNet * Time.deltaTime,
                myRigidbody.velocity.y,
                myRigidbody.velocity.z
            );
            isMoving = true;
        }

        if (axisVertical != 0f)
        {
            myRigidbody.velocity = new Vector3(
                myRigidbody.velocity.x,
                myRigidbody.velocity.y,
                axisVertical * movementSpeedNet * Time.deltaTime
            );
            isMoving = true;
        }

        myAnimator.SetBool("IsRunning", isMoving);
    }

    private void Action()
    {
        myRigidbody.velocity = Vector3.zero;
        myAnimator.SetBool("IsRunning", false);
        myAnimator.SetTrigger("StartAttack");

        isActioning = true;
        Invoke("ActionTeardown", 0.25f);

        if (itemSelected && garbageCollected < backpackCapacity)
        {
            Destroy(itemSelected);
            WC.TrashInWorld--;
            garbageCollected++;
            myPlayerSound.PickTrash();
        }
        else
        {
            myPlayerSound.Attack();
        }
    }

    private void ActionTeardown()
    {
        isActioning = false;
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
            myPlayerSound.TossGarbage();
        }
        else if (other.gameObject.tag == "LyingGuy")
        {
            other.GetComponent<LyingGuy>().Complain();
            myPlayerSound.RandomComplain();
        }
        else if (other.gameObject.tag == "Coin")
        {
            WC.HappinessAdded++;
            Destroy(other.gameObject);
            myPlayerSound.PickPowerup();
        }
        else if (other.gameObject.tag == "Flipflops")
        {
            speedAdded++;
            Destroy(other.gameObject);
            myPlayerSound.PickPowerup();
        }
        else if (other.gameObject.tag == "Backpack")
        {
            backpackCapacity++;
            Destroy(other.gameObject);
            myPlayerSound.PickPowerup();
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

    public void GameOver()
    {
        isActioning = true;
        myAnimator.SetTrigger("StartLose");
    }
}
