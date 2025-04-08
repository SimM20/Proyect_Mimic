using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    private void Start() { SpawnBall(); }

    public BallController SpawnBall()
    {
        GameObject obj = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        BallController ball = obj.GetComponent<BallController>();
        return ball;
    }
}