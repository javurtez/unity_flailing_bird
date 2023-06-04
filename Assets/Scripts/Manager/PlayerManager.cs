using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private float obstacleSpeed = 1;

    public float ObstacleSpeed => obstacleSpeed;

    public delegate void ValueHandler(int value);
    public static event ValueHandler ScoreUpdate;

    public delegate void CallHandler();
    public static event CallHandler CallGameOver;

    private static PlayerManager instance;

    public static PlayerManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayerPrefs.SetString("GOTTENHIGHSCORE", "NO");
    }

    public void AddScore()
    {
        score++;

        if (score % 8 == 0)
        {
            AddSpeed();
        }

        int maxScore = PlayerPrefs.GetInt("SCORE", 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("SCORE", score);
            PlayerPrefs.SetString("GOTTENHIGHSCORE", "YES");
        }

        ScoreUpdate?.Invoke(score);
    }
    public void AddSpeed()
    {
        obstacleSpeed = Mathf.Clamp(obstacleSpeed + .1f, 0.9f, 3f);
    }

    public void GameOver()
    {
        CallGameOver?.Invoke();
        AudioManager.Instance.PlayGameover();
    }
}