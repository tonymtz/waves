using UnityEngine;

public class WalkingGuy : RandomPerson
{
    [SerializeField]
    private float movementSpeed = 100f;

    [SerializeField]
    private float rotatingSpeed = 5f;

    private Rigidbody myRigidbody;

    private bool isWalking;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        InitializeTimer();
    }

    void Update()
    {
        Tick();
        Move();
        ChangeCourse();
    }

    protected override void Digest()
    {
        resetState();
        OnMoveEnterState();
    }

    private void Move()
    {
        if (!isWalking) { return; }

        myRigidbody.velocity = transform.forward * movementSpeed * Time.deltaTime;
    }

    private void ChangeCourse()
    {
        float sinOfTime = Mathf.Sin(Time.time) / 3;

        Vector3 startVector = transform.forward;
        Vector3 endVector = Quaternion.AngleAxis(sinOfTime, Vector3.up) * startVector;

        transform.rotation = Quaternion.LookRotation(endVector);
    }

    private void TossItem() { }

    private void OnMoveEnterState()
    {
        timeleft = 1f;
        isWalking = true;
    }

    private void OnChangeCourseEnterState() { }

    private void OnTossItemEnterState() { }

    private void resetState()
    {
        isWalking = false;
    }
}
