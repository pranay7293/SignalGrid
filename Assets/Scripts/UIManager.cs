using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panel")]
    public TextMeshProUGUI moveCounterText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI toggleCountText;
    public GameObject winPanel;
    public GameObject peekPanel;
    public Button peekButton;
    public Image[] peekSquares;
    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.black;

    private int moveCount = 0;
    private float elapsedTime = 0f;
    private bool timerRunning = false;
    private bool peekUsed = false;
    private bool isInPeekMode = false;
    private bool allowMoveCounting = false;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateMoveCounter();
        winPanel.SetActive(false);
    }

    private void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void IncrementMoveCount()
    {
        if (!allowMoveCounting) return;

        moveCount++;
        UpdateMoveCounter();
    }

    public void EnableMoveTimeCounting()
    {
        allowMoveCounting = true;
        timerRunning = true;
    }

    private void UpdateMoveCounter()
    {
        if (moveCounterText != null)
            moveCounterText.text = "Moves: " + moveCount;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    public void ShowWinScreen()
    {
        winPanel.SetActive(true);
        timerRunning = false; 
    }

    public void ShowToggleFeedback(int count)
    {
        toggleCountText.text = "Toggled: " + count;
    }

    public void OnClickPeek()
    {
        if (peekUsed) return;
        peekUsed = true;
        isInPeekMode = true;
        peekButton.interactable = false;
    }

    public bool IsPeekMode() => isInPeekMode;

    public void HandlePeekTile(Vector2Int center)
    {
        if (!isInPeekMode) return;
        isInPeekMode = false;

        ToggleRuleType rule = SignalGridManager.Instance.GetRuleForTile(center);
        Vector2Int[] directions = new Vector2Int[9]
        {
            new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1),
            new Vector2Int(-1,  0), new Vector2Int(0,  0), new Vector2Int(1,  0),
            new Vector2Int(-1,  1), new Vector2Int(0,  1), new Vector2Int(1,  1)
        };

        for (int i = 0; i < 9; i++)
        {
            Vector2Int pos = center + directions[i];
            bool toggles = TogglePatternGenerator.GetAffectedTiles(center, rule, 5).Contains(pos) || i == 4;
            peekSquares[i].color = toggles ? activeColor : inactiveColor;
        }

        StartCoroutine(ShowPeekGridTemp());
    }

    private IEnumerator ShowPeekGridTemp()
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < peekSquares.Length; i++)
        {
            peekSquares[i].color = inactiveColor;
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
