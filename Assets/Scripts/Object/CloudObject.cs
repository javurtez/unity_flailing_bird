using UnityEngine;

public class CloudObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;

    private void Update()
    {
        transform.position += speed * Time.smoothDeltaTime * Vector3.left;

        if (transform.position.x <= -5f)
        {
            transform.position = new Vector3(6.0f, transform.position.y);
        }
    }
}