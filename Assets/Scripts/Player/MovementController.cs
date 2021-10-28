using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private GameManager gameManager;

    public CharacterController characterController;

    public float speed = 10;
    public float gravity = -9.81f;

    public Transform groundTrigger;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    private bool isGrounded;
    private Vector3 velocity;

    void Start ()
    {
        gameManager = GameManager.instance;
    }
	
	void Update ()
    {
        if (gameManager.GetGameState() == GameState.Stop) { return; }

        isGrounded = Physics.CheckSphere(groundTrigger.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
