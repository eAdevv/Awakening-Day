using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    CharacterController controller;
    Vector3 velocity;

    private bool isGrounded;
    private bool isCrouching;

    public Transform groundChecker;
    public float distance = 0.3f;

    public float walkingSpeed;
    public float runningSpeed;
    public float crouchingMoveSpeed;
    public float jumpHeight;
    public float gravity;
  
    public LayerMask mask;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        #region Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementVector = transform.right * horizontal + transform.forward * vertical;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(movementVector * runningSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(movementVector * walkingSpeed * Time.deltaTime);
        }
        #endregion

        #region Jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity); 
        

        #endregion

        #region Gravity
        isGrounded = Physics.CheckSphere(groundChecker.position, distance, mask);

        if (isGrounded && velocity.y < 0) velocity.y = 0f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        #endregion
  
    }  

}
