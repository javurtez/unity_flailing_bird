using UnityEngine;

public class GroundObject : MonoBehaviour, IObstacle
{
    [SerializeField]
    private GroundObject otherGroundObject;

    private SpriteRenderer spriteRenderer;
    private bool canMove = true;

    private void Awake()
    {
        PlayerManager.CallGameOver += GameOver;
    }
    private void OnDestroy()
    {
        PlayerManager.CallGameOver -= GameOver;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!canMove) return;

        transform.position += PlayerManager.Instance.ObstacleSpeed * Time.smoothDeltaTime * Vector3.left;

        if (transform.position.x <= -7.5f)
        {
            transform.position = new Vector3(otherGroundObject.transform.position.x + spriteRenderer.size.x, transform.position.y);
        }
    }

    private void GameOver()
    {
        canMove = false;
    }
    public void Hit()
    {
        PlayerManager.Instance.GameOver();
        AudioManager.Instance.PlayHit();

        Debug.Log("HIT");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canMove) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            // Game Over
            Hit();
        }
    }
}