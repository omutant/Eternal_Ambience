using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
// Zils 

public class PlayerController : MonoBehaviour {

    public float speed;
    public float angled;
    public float runspeed = 5;
    public float sneakspeed = 1;
    public float normalspeed = 3;

    public CharacterController characterController;

    public float jumpSpeed = 8.0f;
    public float maxJumpPower = 10.0f;
    public float jumptimer = 0f;
    //private float jumpPower = 0f;

    public float gravity = 20.0f;
    private float fallspeed = 0.0f;
#pragma warning disable IDE0052 // Remove unread private members
    private bool isGrounded = false;
#pragma warning restore IDE0052 // Remove unread private members

    private Vector3 moveDirectionCur = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 moveDirectionNew = Vector3.zero;

    private Vector3 rotDirection = Vector3.zero;
    private Vector3 rotcurrent = Vector3.zero;
    private static Vector3 rot2 = Vector3.zero;
    private readonly float smoothTime = 0.05f; //For rotation smoothing

    private Camera cam;
#pragma warning disable IDE0052 // Remove unread private members
    private CinemachineFreeLook vcam;
#pragma warning restore IDE0052 // Remove unread private members
                               //public CinemachineImpulseSource impulseSource;
    public bool isAiming = false;

    private float inX;
    private float inZ;

    public Texture2D cursorTexture;
    private readonly CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    //animation tools

    void Start () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController> ();
        cam = Camera.main;
        vcam = cam.GetComponent<CinemachineFreeLook> ();
        //vcam.m_CommonLens = true;

        //animation

    }

    void Update () {
        //end game
        if (Input.GetKey ("escape")) {
            Application.Quit ();
        }

        if (Input.GetButton ("Run")) {
            speed = runspeed;
        } else if (Input.GetButton ("Sneak")) {
            speed = sneakspeed;
        } else { speed = normalspeed; }

        //Movement 
        //make move different when grounded than when jumping or in air.

        //get controller input
        inX = Input.GetAxisRaw ("Horizontal");
        inZ = Input.GetAxisRaw ("Vertical");

        if (Input.GetButton ("Fire2")) {
            isAiming = true;
            Cursor.visible = true;
            Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
        } else {
            isAiming = false;
            Cursor.visible = false;
        }

        //Camera Y direction 

        var cforward = cam.transform.forward;
        var cright = cam.transform.right;
        //var cup = cam.transform.up;

        //Smooth movement speeds - TODO

        //move towards relative forward
        moveDirectionNew = (cforward * inZ) + (cright * inX);
        moveDirection = Vector3.SmoothDamp (moveDirectionCur, moveDirectionNew, ref moveDirectionNew, .001f);

        //rot2 = Vector3.SmoothDamp (rotcurrent, rotDirection, ref rotDirection, smoothTime);

        //if inputs on both x and z axis, slow down so diagonal movement isnt faster
        if ((inX != 0) && (inZ != 0)) {
            angled = 0.68f;
        } else angled = 1f;

        if ((inX != 0) || (inZ != 0)) {

            moveDirection = new Vector3 (moveDirection.x * speed * angled, moveDirection.y + fallspeed, moveDirection.z * speed * angled);

            //if rightclick isnt true
            rotDirection = new Vector3 (moveDirection.x, 0.0f, moveDirection.z); //Rotate to face direction
            if (isAiming) {
                rotDirection = new Vector3 (cforward.x, 0.0f, cforward.z);

            }

            //smoothly change rotatecurrent to rotDirection
            rot2 = Vector3.SmoothDamp (rotcurrent, rotDirection, ref rotDirection, smoothTime);
            transform.forward = rot2; //update player rotation based on transition
            rotcurrent = rot2; //update current rotation
        }
        //}
        if (characterController.isGrounded) {
            // We are grounded, so recalculate
            isGrounded = true;
            fallspeed = 0.0f;

            //jump
            if (Input.GetButton ("Jump")) { moveDirection.y = jumpSpeed; jumptimer = maxJumpPower; }
        }

        fallspeed -= (gravity * Time.deltaTime);
        moveDirection.y += fallspeed + jumptimer;
        if (jumptimer > 0) { jumptimer--; }

        // Move the controller
        moveDirectionCur = moveDirection;

        characterController.Move (moveDirectionCur * Time.deltaTime);
        //moveDirection = (cforward * inZ) + (cright * inX);

    }
}