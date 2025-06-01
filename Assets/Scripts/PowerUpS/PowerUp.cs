using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected PowerUpData data;
    private Rigidbody2D rb;
    private Vector3 screenBottom;
    private Vector3 worldBottom;

    private void Awake() { rb = GetComponent<Rigidbody2D>(); }

    private void Start()
    {
        screenBottom = new Vector3(Screen.width / 2f, Screen.safeArea.yMin, Camera.main.nearClipPlane);
        worldBottom = Camera.main.ScreenToWorldPoint(screenBottom);
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + Vector2.down * data.Speed * Time.deltaTime);
        if (rb.position.y < worldBottom.y) Destroy(gameObject);
    }

    protected virtual void HandleCollision(GameObject other) {  }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            HandleCollision(other.gameObject);
            Destroy(gameObject);
        }
    }
}
