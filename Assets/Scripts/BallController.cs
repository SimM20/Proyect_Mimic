using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class BallController : MonoBehaviour
{
    [SerializeField] private BallData data;

    public event Action OnOutOfBounds;

    private Rigidbody2D rb;
    private float currentSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = data.initialSpeed;

        Vector2 direction = new Vector2(Random.Range(-1, 1), -1).normalized;
        rb.velocity = direction * currentSpeed;
    }

    private void Update()
    {
        Vector3 screenBottom = new Vector3(Screen.width / 2f, Screen.safeArea.yMin, Camera.main.nearClipPlane);
        Vector3 worldBottom = Camera.main.ScreenToWorldPoint(screenBottom);

        if (transform.position.y < worldBottom.y)
        {
            OnOutOfBounds?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IScorable scorable = collision.gameObject.GetComponent<IScorable>();
        if (scorable != null)
        {
            scorable.OnHitByBall();
            currentSpeed += data.speedIncrease;
            rb.velocity = rb.velocity.normalized * currentSpeed;
        }
    }
}
