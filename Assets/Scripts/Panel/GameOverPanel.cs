using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour, IPanel
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject highScoreIndicator;

    [SerializeField]
    private RectTransform panel;

    private void Awake()
    {
        PlayerManager.ScoreUpdate += UpdateScore;
        PlayerManager.CallGameOver += Open;
    }
    private void OnDestroy()
    {
        PlayerManager.ScoreUpdate -= UpdateScore;
        PlayerManager.CallGameOver -= Open;
    }
    private void Start()
    {
        Close();

        scoreText.SetText("0");
    }

    private void UpdateScore(int score)
    {
        scoreText.SetText(score.ToString());
    }

    public void OnRestart()
    {
        AudioManager.Instance.PlayClick();
        SceneManager.LoadScene(0);
    }

    public void Open()
    {
        gameObject.SetActive(true);

        panel.LeanMoveY(0, .8f).setEase(LeanTweenType.easeOutBounce).setOnComplete(() =>
        {
            if (PlayerPrefs.GetString("GOTTENHIGHSCORE", "NO") == "YES")
            {
                highScoreIndicator.SetActive(true);
                highScoreIndicator.LeanScale(Vector2.one, .4f);

                AudioManager.Instance.PlayHighScore();
            }
        });
    }
    public void Close()
    {
        gameObject.SetActive(false);
        highScoreIndicator.SetActive(false);

        highScoreIndicator.transform.localScale = Vector2.zero;

        panel.anchoredPosition = new Vector2(0, -700);
    }
}