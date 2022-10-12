using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;

    [SerializeField]
    private float walkSpeed = 12;
    [SerializeField]
    private float runSpeed = 18;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 1.8f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private Stamina stamina;

    private bool isGrounded;

    private Vector3 velocity;
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float localSpeed = walkSpeed;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isMoving = !(x < float.Epsilon && z < float.Epsilon);

        if (isMoving){
            FindObjectOfType<AudioManager>().Play("PlayerRun");
        } else if (!isMoving){
            //FindObjectOfType<AudioManager>().Stop("PlayerRun");
        }
          
        // Sprinting logic
        if (isMoving && Input.GetKey(KeyCode.LeftShift) && !stamina.IsExhausted)
        {
            localSpeed = runSpeed;
            stamina.ConsumeStamina();
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * localSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
