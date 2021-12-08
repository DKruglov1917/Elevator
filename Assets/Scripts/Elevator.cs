using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public delegate void elevator();
    public event elevator onDoors, onMove, onStop;

    public List<CallButton> callButtons;
    public List<PanelButton> panelButtons;
    public ElevatorPanel elevatorPanel;
    private Photocell photocell;
    private Animator animator;

    public float speed = 1;
    private bool wasCalled, areDoorsClosed, closing, autoClosing;
    private Vector3 targetPosition;


    public void ChooseFloor(int _floor)
    {
        targetPosition = elevatorPanel.floors.transform.GetChild(_floor).GetChild(0).position;
        if (!photocell.stop)
            wasCalled = true;
    }

    private void Awake()
    {
        AddButtons();
    }

    private void Start()
    {
        photocell = transform.GetChild(3).GetChild(0).GetComponent<Photocell>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        areDoorsClosed = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        EmergencyOpenDoors();
    }

    private void AddButtons()
    {
        for (int i = 0; i < elevatorPanel.transform.GetChild(0).childCount; i++)
        {
            panelButtons.Add(elevatorPanel.transform.GetChild(0).GetChild(i).GetComponent<PanelButton>());
            panelButtons[i].onButtonPressed += ChooseFloor;
        }
        for (int i = 0; i < elevatorPanel.transform.GetChild(0).childCount; i++)
        {
            callButtons.Add(elevatorPanel.floors.transform.GetChild(i).GetChild(1).GetChild(1).GetComponent<CallButton>());
            callButtons[i].onElevatorCalled += ChooseFloor;
        }
    }

    private void Move()
    { 
        if (wasCalled && areDoorsClosed)
        {
            onMove?.Invoke();
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
        if (transform.position == targetPosition && wasCalled)
        {
            wasCalled = false;
            if(areDoorsClosed)
                OpenDoors();
        }
    }

    private void OpenDoors()
    {
        onDoors?.Invoke();
        onStop?.Invoke();
        StartCoroutine("AutoClosingDoors");
        animator.SetBool("Open", true);
        areDoorsClosed = false;
    }    
    
    private void EmergencyOpenDoors()
    {
        if (photocell.stop && closing)
        {
            onDoors?.Invoke();
            StopCoroutine("CloseDoors");
            StartCoroutine("AutoClosingDoors");
            animator.SetBool("Open", true);
            areDoorsClosed = false;
            closing = false;
        }
    }

    IEnumerator CloseDoors()
    {
        if (!areDoorsClosed && !closing)
        {
            closing = true;

            onDoors?.Invoke();
            animator.SetBool("Open", false);
            yield return new WaitForSeconds(2f);
            areDoorsClosed = true;

            closing = false;
        }
    }

    IEnumerator AutoClosingDoors()
    {        
        if (!autoClosing)
        {
            autoClosing = true;

            yield return new WaitForSeconds(3);

            if (photocell.stop)
            {
                autoClosing = false;
                StartCoroutine("AutoClosingDoors");
            }
            else
            {
                StartCoroutine("CloseDoors");
            }

            autoClosing = false;
        }
    }
}
