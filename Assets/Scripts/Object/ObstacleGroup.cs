using UnityEngine;

public class ObstacleGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstacles;

    [SerializeField]
    private Transform nextGroup;

    private bool canMove;
    private bool ableToEnableHardObstacle = false;

    private void Awake()
    {
        ControlManager.Tap += Move;
        PlayerManager.CallGameOver += GameOver;
    }
    private void OnDestroy()
    {
        ControlManager.Tap -= Move;
        PlayerManager.CallGameOver -= GameOver;
    }
    private void Start()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i] = Instantiate(obstacles[i]);
            obstacles[i].transform.SetParent(transform);
            obstacles[i].transform.localPosition = Vector3.zero;
            obstacles[i].SetActive(false);
        }
        RandomObstacle();
    }
    private void Update()
    {
        if (!canMove) return;

        transform.position += PlayerManager.Instance.ObstacleSpeed * Time.smoothDeltaTime * Vector3.left;

        if (transform.position.x <= -10f)
        {
            ResetGroup();
        }
    }

    public void GameOver()
    {
        canMove = false;
    }
    private void Move()
    {
        if (canMove) return;
        canMove = true;
    }
    private void RandomObstacle()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
        int index = Random.Range(0, !ableToEnableHardObstacle ? 3 : obstacles.Length);
        obstacles[index].SetActive(true);
        foreach (Transform child in obstacles[index].transform)
        {
            if (child.TryGetComponent(out ScoreObject score))
            {
                score.ResetObject();
            }
            if (child.TryGetComponent(out ObstacleObject obs))
            {
                obs.ResetObject();
            }
        }
    }
    private void ResetGroup()
    {
        ableToEnableHardObstacle = true;

        RandomObstacle();

        transform.position = nextGroup.position + Vector3.right * 6;
    }
}