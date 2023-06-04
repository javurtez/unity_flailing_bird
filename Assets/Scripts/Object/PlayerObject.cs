using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2D;
    [SerializeField]
    private float force;

    private bool isGameOver = false;

    private void Awake()
    {
        ControlManager.Tap += Flail;
        PlayerManager.CallGameOver += GameOver;
    }
    private void OnDestroy()
    {
        ControlManager.Tap -= Flail;
        PlayerManager.CallGameOver -= GameOver;
    }

    private void GameOver()
    {
        isGameOver = true;

        LeanTween.rotateAround(gameObject, Vector3.forward, 360f, 1f).setLoopClamp();

        rb2D.constraints = RigidbodyConstraints2D.None;
    }
    private void Flail()
    {
        if (isGameOver) return;
        if (!rb2D.simulated)
        {
            rb2D.simulated = true;
        }

        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(Vector2.up * force, ForceMode2D.Force);

        AudioManager.Instance.PlayFlap();
    }
}