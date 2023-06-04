using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanel : MonoBehaviour, IPanel
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        PlayerManager.ScoreUpdate += UpdateScore;
        PlayerManager.CallGameOver += HideScore;
        ControlManager.Tap += ShowScore;
    }
    private void OnDestroy()
    {
        PlayerManager.ScoreUpdate -= UpdateScore;
        PlayerManager.CallGameOver -= HideScore;
        ControlManager.Tap -= ShowScore;
    }
    private void Start()
    {
        HideScore();
    }

    private void UpdateScore(int score)
    {
        scoreText.SetText(score.ToString());

        scoreText.gameObject.LeanScale(Vector2.one * 1.1f, .15f).
            setEase(LeanTweenType.linear).
            setLoopPingPong(1);
    }
    private void HideScore()
    {
        scoreText.gameObject.SetActive(false);
    }
    private void ShowScore()
    {
        scoreText.gameObject.SetActive(true);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}