using UnityEngine;

public class ScoreObject : MonoBehaviour, IObject
{
    private bool canTrigger = true;

    public void ResetObject()
    {
        canTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTrigger) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            canTrigger = false;
            AudioManager.Instance.PlayCoin();
            PlayerManager.Instance.AddScore();
        }
    }
}