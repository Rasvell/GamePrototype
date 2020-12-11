using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            animator.SetBool("NeedOpen", true);
        }
    }

    public void DoorIsOpen()
    {
        animator.enabled = false;
    }
}
