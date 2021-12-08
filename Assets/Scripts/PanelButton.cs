using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    public delegate void press(int _buttonNum);
    public event press onButtonPressed;

    public GameObject floor;
    private Elevator elevator;
    private Animator animator;
    private AudioSource audioSource;

    private int buttonNum;

    public void ChooseFloor()
    {
        onButtonPressed?.Invoke(buttonNum);
        animator.SetTrigger("Press");
        audioSource.Play();
    }   

    private void Start()
    {
        floor = gameObject.transform.parent.parent.GetComponent<ElevatorPanel>().floors.transform.GetChild(buttonNum).gameObject;
        elevator = gameObject.transform.parent.parent.parent.gameObject.GetComponent<Elevator>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        buttonNum = gameObject.transform.GetSiblingIndex();
    }
}
