using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallButton : MonoBehaviour
{
    public delegate void call(int _floor);
    public event call onElevatorCalled;

    private Animator animator;
    private AudioSource audioSource;

    private int floorNum;

    public void CallElevator()
    {
        onElevatorCalled?.Invoke(floorNum);
        animator.SetTrigger("Press");
        audioSource.Play();
    }   

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        floorNum = gameObject.transform.parent.parent.GetSiblingIndex();
    }
}
