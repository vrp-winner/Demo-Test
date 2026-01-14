using UnityEngine;
using TMPro; // หรือ TMPro ถ้าใช้ TextMeshPro
using UnityEngine.SceneManagement; // สำหรับโหลดฉากใหม่
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    [Header("UI References")]
    public TMP_Text scoreText;       // ลาก Text คะแนนมาใส่ (หรือ TMP_Text)
    public GameObject gameOverUI; // ลาก Panel หน้าจบเกมมาใส่

    public bool IsGameOver { get; private set; }
    private float score;

    void Awake()
    {
        // Setup Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (IsGameOver)
        {
            // 2. เช็คการกดปุ่มด้วยระบบใหม่ (เช็คทั้งคีย์บอร์ดและเมาส์)
            bool isKeyboardPressed = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;
            bool isMousePressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

            if (isKeyboardPressed || isMousePressed)
            {
                RestartGame();
            }
            return;
        }

        // เพิ่มคะแนนตามเวลา (ยิ่งรอดนานยิ่งได้เยอะ)
        score += 10 * Time.deltaTime;

        // อัปเดต UI (แปลงเป็น int เพื่อตัดทศนิยม)
        if (scoreText != null)
            scoreText.text = "Score: " + (int)score;
    }

    public void GameOver()
    {
        IsGameOver = true;

        // หยุดเวลาในเกม (ทำให้ทุกอย่างหยุดนิ่ง)
        Time.timeScale = 0f;

        // เปิดหน้าต่าง Game Over
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        // คืนค่าเวลากลับมาเดินปกติ
        Time.timeScale = 1f;

        // โหลด Scene ปัจจุบันใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}