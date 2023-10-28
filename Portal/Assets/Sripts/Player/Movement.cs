using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 3f;

    [SerializeField] private Transform orientation;

    Vector3 velocity;
    bool isGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float distance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = orientation.right * x + orientation.forward * z;
        move.y = 0f; // Устанавливаем значение по оси Y равным 0, чтобы ограничить движение по этой оси

        move = Vector3.ClampMagnitude(move, 1f); // Ограничиваем длину вектора до 1, чтобы движение было одинаковой скорости во всех направлениях

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
