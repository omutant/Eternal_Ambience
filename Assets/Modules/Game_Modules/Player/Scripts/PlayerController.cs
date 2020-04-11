using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
// Zils 

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float angled;
    public float runspeed = 10;
    public float sneakspeed = 1;
    public float normalspeed = 3;

    public CharacterController characterController;

    // Jump variables
    [SerializeField]
    [Range(6, 10)]
    private float jumpPower = 7.8f;
    private float jumpGravity = 6f;
    private int maxJumps = 1;
    private int jumpsLeft = 1;
    private float jumpSpeed = 0.0f;
    private float jumpForceCurveSpeed = 0.5f;

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

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        vcam = cam.GetComponent<CinemachineFreeLook>();
    }



    void Update()
    {
        if (!Sprint() && !Sneak())
            speed = normalspeed;



        //Movement 
        //make move different when grounded than when jumping or in air.

        //get controller input
        inX = Input.GetAxisRaw("Horizontal");
        inZ = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Fire2"))
        {
            isAiming = true;
            Cursor.visible = true;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
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
        moveDirection = Vector3.SmoothDamp(moveDirectionCur, moveDirectionNew, ref moveDirectionNew, .001f);

        //rot2 = Vector3.SmoothDamp (rotcurrent, rotDirection, ref rotDirection, smoothTime);

        //if inputs on both x and z axis, slow down so diagonal movement isnt faster
        if ((inX != 0) && (inZ != 0))
        {
            angled = 0.68f;
        }
        else angled = 1f;

        if ((inX != 0) || (inZ != 0))
        {

            moveDirection = new Vector3(moveDirection.x * speed * angled, moveDirection.y, moveDirection.z * speed * angled);

            //if rightclick isnt true
            rotDirection = new Vector3(moveDirection.x, 0.0f, moveDirection.z); //Rotate to face direction
            if (isAiming)
            {
                rotDirection = new Vector3(cforward.x, 0.0f, cforward.z);

            }

            //smoothly change rotatecurrent to rotDirection
            rot2 = Vector3.SmoothDamp(rotcurrent, rotDirection, ref rotDirection, smoothTime);
            transform.forward = rot2; //update player rotation based on transition
            rotcurrent = rot2; //update current rotation
        }
        Jump();

        // Move the controller
        moveDirectionCur = moveDirection;

        characterController.Move(moveDirectionCur * Time.deltaTime);
        //moveDirection = (cforward * inZ) + (cright * inX);

    }

    #region walkModifiers
    float speedScale = 5f;
    bool Sprint()
    {
        if (Input.GetButton("Run"))
        {
            if (speed < runspeed)
                speed += speedScale * Time.deltaTime;
            else
                speed = runspeed;
            return true;
        }
        return false;
    }

    bool Sneak()
    {
        if (Input.GetButton("Sneak"))
        {
            speed = sneakspeed;
            return true;
        }
        return false;
    }

    #endregion walkModifiers

    #region JumpLogic
    void Jump()
    {
        ApplyGravity();
        StartCoroutine("JumpLogic");
        moveDirection.y = jumpSpeed;
    }
    void ApplyGravity()
    {
        if (!characterController.isGrounded && jumpSpeed > -jumpGravity)
            jumpSpeed -= jumpGravity * Time.deltaTime;
        if (jumpSpeed < 0)
            jumpSpeed -= 5 * Time.deltaTime;
        if (characterController.isGrounded)
            jumpSpeed = 0;
    }
    float jumpForce = 0;
    IEnumerator JumpLogic()
    {
        if (jumpsLeft > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpForce = jumpPower;
                jumpSpeed += jumpPower / 5;
            }
            if (Input.GetButtonUp("Jump"))
            {
                jumpsLeft--;
                if (jumpSpeed > 0)
                    jumpSpeed = 0;
            }

            while (Input.GetButton("Jump") && jumpForce > 5)
            {
                jumpForce -= (Time.deltaTime * jumpPower) * jumpForceCurveSpeed;
                jumpSpeed += jumpForce * Time.deltaTime;
                //jumpSpeed = (Mathf.Cos(Time.time * 500) * 50);
                //jumpSpeed += jumpAcceleration * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        if (characterController.isGrounded)
            jumpsLeft = maxJumps;
        yield return null;
    }
    #endregion JumpLogic
}