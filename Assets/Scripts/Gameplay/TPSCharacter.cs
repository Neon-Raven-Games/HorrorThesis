using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacter : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    [SerializeField] private float rotationSensitivity = 1.75f;

    private Camera mainCamera;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private static readonly int _SWalking = Animator.StringToHash("walking");

    private void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defaultCameraOffset = mainCamera.transform.localPosition;
    }

    private Vector3 defaultCameraOffset;
    private bool active = true;

    public void SetControlActive()
    {
        active = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetControlInactive()
    {
        active = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (!active) return;
        ApplyMovement();
        ApplyRotation();
        ApplyGravity();
        ApplyCameraCollisionCorrection();
    }

    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private float cameraCollisionOffset = 0.2f;

    private void ApplyCameraCollisionCorrection()
    {
        // Define the direction from the player to the camera
        Vector3 desiredCameraPosition = transform.position + transform.TransformDirection(defaultCameraOffset);
        Vector3 directionToCamera = desiredCameraPosition - transform.position;

        var origin = transform.position;
        origin.y = mainCamera.transform.position.y;
        // Raycast from the player to the desired camera position
        if (Physics.Raycast(transform.position, directionToCamera.normalized, out var hit,
                directionToCamera.magnitude + cameraCollisionOffset, collisionMask))
        {
            Vector3 hitPosition = hit.point - directionToCamera.normalized * cameraCollisionOffset;
            hitPosition.y = mainCamera.transform.position.y;
            mainCamera.transform.position =
                Vector3.Lerp(mainCamera.transform.position, hitPosition, Time.deltaTime * smoothSpeed);
        }
        else
        {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, defaultCameraOffset,
                Time.deltaTime * smoothSpeed);
        }
    }

    [SerializeField] private float runSpeed = 2f;

    private void ApplyMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal == 0 && vertical == 0)
        {
            anim.SetBool(_SWalking, false);
        }
        else
        {
            anim.SetBool(_SWalking, true);
        }

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        controller.Move(movement.normalized * runSpeed * Time.deltaTime);
    }


    private void ApplyRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSensitivity, 0);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}