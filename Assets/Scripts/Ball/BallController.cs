using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class BallController : MonoBehaviour
{
    [SerializeField] private BallData data;

    public event Action OnOutOfBounds;

    private Rigidbody2D rb;
    private float currentSpeed;
    private AudioSource audioSource;
    private Vector3 screenBottom;
    private Vector3 worldBottom;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        currentSpeed = data.InitialSpeed;

        Vector2 direction = new Vector2(Random.Range(-1, 1), -1).normalized;
        rb.velocity = direction * currentSpeed;
        screenBottom = new Vector3(Screen.width / 2f, Screen.safeArea.yMin, Camera.main.nearClipPlane);
        worldBottom = Camera.main.ScreenToWorldPoint(screenBottom);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < worldBottom.y)
        {
            OnOutOfBounds?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
        IScorable scorable = collision.gameObject.GetComponent<IScorable>();
        if (scorable != null)
        {
            scorable.OnHitByBall();
            currentSpeed += data.SpeedIncrease;
            rb.velocity = rb.velocity.normalized * currentSpeed;
        }
    }
}
