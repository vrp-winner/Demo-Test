using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 10f;

    [Header("MGround Check Settings")]
    [SerializeField] private Transform grondCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isInverted = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(grondCheck.position, groundCheckRadius, groundLayer);
    }

    // Wow Factor: Shape Shifting & Gravity Flip
    public void OnGravityShift(InputAction.CallbackContext context)
    {
        if (context.started) // ทำงานตอนคลิก
        {
            ToggleGravity();
        }
    }

    private void ToggleGravity()
    {
        isInverted = !isInverted;

        // กลับค่า Gravity ของ Rigidbody
        rb.gravityScale *= -1;

        // กลับด้าน Sprite
        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        transform.localScale = newScale;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started || !IsGrounded()) return;
        string keyPressed = context.control.name;
        if (!isInverted && keyPressed == "w")
        {
            PerformJump(1f); // กระโดดขึ้น
        }
        else if (isInverted && keyPressed == "s")
        {
            PerformJump(-1f); // กระโดดลง
        }
    }

    // แยกฟังก์ชันกระโดดออกมาเพื่อให้โค้ด Clean
    private void PerformJump(float directionMultiplier)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * directionMultiplier);
    }

    // Ground Check Gizmos
    private void OnDrawGizmosSelected()
    {
        if (grondCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(grondCheck.position, groundCheckRadius);
        }
    }

    // Ground Check
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ถ้าชนสิ่งกีดขวาง -> Game Over
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // เรียกใช้ GameManager ให้จบเกม
            GameManager.Instance.GameOver();

            // (Optional) อาจจะสั่งปิดตัวละคร หรือเล่นเสียงระเบิดตรงนี้
            // gameObject.SetActive(false); 
        }
    }
}