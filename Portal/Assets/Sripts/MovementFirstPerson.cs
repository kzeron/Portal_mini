using UnityEngine;

public class MovementFirstPerson : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;

    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float mouseSensitivityX = 10.0f;
    [SerializeField] private float mouseSensitivityY = 10.0f;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private Vector3 moveDirection;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayerMask;
    private bool isGround;

    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        // Rotate camera based on mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        transform.Rotate(Vector3.up, mouseX);

        Vector3 cameraRotation = cam.rotation.eulerAngles;
        cameraRotation.x -= mouseY;
        cameraRotation.z = 0f;

        if (cameraRotation.x > 180f)
        {
            cameraRotation.x -= 360f;
        }

        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90f, 90f);

        cam.rotation = Quaternion.Euler(cameraRotation);
    }
}
