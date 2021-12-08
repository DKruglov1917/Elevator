using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public Camera cam;

    public float hitDistance = 5;

    public void DoRaycast()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitDistance) && hit.collider.tag == "CallButton")
            hit.transform.gameObject.GetComponent<CallButton>().CallElevator();

        else if (Physics.Raycast(ray, out hit, hitDistance) && hit.collider.tag == "PanelButton")
            hit.transform.gameObject.GetComponent<PanelButton>().ChooseFloor();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DoRaycast();
    }
}
