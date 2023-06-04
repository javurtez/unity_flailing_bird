using Unity.VisualScripting;
using UnityEngine;

public class ObstacleObject : MonoBehaviour, IObstacle, IObject
{
    private bool canTrigger = true;

    private void Awake()
    {
        PlayerManager.CallGameOver += GameOver;
    }
    private void OnDestroy()
    {
        PlayerManager.CallGameOver -= GameOver;
    }

    public void ResetObject()
    {
        canTrigger = true;
    }
    private void GameOver()
    {
        canTrigger = false;
    }
    public void Hit()
    {
        PlayerManager.Instance.GameOver();
        AudioManager.Instance.PlayHit();

        canTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTrigger) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            // Game Over
            Hit();
        }
    }
}