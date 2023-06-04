using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private BackgroundObject otherGroundObject;

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

        transform.position += speed * Time.smoothDeltaTime * Vector3.left;

        if (transform.position.x <= -13.5f)
        {
            transform.position = new Vector3(otherGroundObject.transform.position.x + (spriteRenderer.size.x - 0.02f) * transform.localScale.x, transform.position.y);
        }
    }

    public void GameOver()
    {
        canMove = false;
    }
}
