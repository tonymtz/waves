using UnityEngine;

public class LyingGuy : RandomPerson
{
    private bool isComplaining;

    private Animator myAnimator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Complain()
    {
        isComplaining = true;
        myAnimator.SetTrigger("Upset");
        Invoke("Teardown", 1.24f);
    }

    private void Teardown()
    {
        isComplaining = false;
    }
}
