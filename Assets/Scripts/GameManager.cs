using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    [Header("UI References")]
    public TMP_Text scoreText;
    public GameObject gameOverUI;

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
            // เช็คการกดปุ่มด้วยระบบใหม่ -> ทั้งคีย์บอร์ดและเมาส์
            bool isKeyboardPressed = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;
            bool isMousePressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

            if (isKeyboardPressed || isMousePressed)
            {
                RestartGame();
            }
            return;
        }

        // เพิ่มคะแนนตามเวลา
        score += 10 * Time.deltaTime;

        // อัปเดต UI
        if (scoreText != null)
            scoreText.text = "Score: " + (int)score;
    }

    public void GameOver()
    {
        IsGameOver = true;

        // หยุดเวลาในเกม
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