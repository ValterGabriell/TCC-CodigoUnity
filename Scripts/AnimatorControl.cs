using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    public Animator Animator;
    public GameManager GameManager;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (GameManager.isWalking)
        {
            Animator.SetBool("IsWalk", true);
        }
        else 
        {
            Animator.SetBool("IsWalk", false);
        }
    }

}
