using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour

{
    public Camera cam;
    //private CinemachineFreeLook vcam;
    public bool isAiminghead = false;

    public PlayerController playerController;

    public Vector3 target;
    public Vector3 cameraturn;

#pragma warning disable IDE0052 // Remove unread private members
    GameObject head;
#pragma warning restore IDE0052 // Remove unread private members

    // Start is called before the first frame update
    void Start () {
        // playerController = GetComponent<playerController> ();
        cam = Camera.main;
        head = GetComponent<GameObject> ();

    }

    // Update is called once per frame
    void Update () {
        //var cforward = cam.transform.forward;
        //var cright = cam.transform.right;
        //var cup = cam.transform.up;

        isAiminghead = playerController.isAiming;

        //if aiming, rotate head
        // if (isAiminghead == true) {
        //    target = new Vector3 (cforward.x, cforward.z, 0.0f);
        //this.transform.LookAt (target, Vector3.up);
        // } else {

    }

}