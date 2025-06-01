using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    public BallController SpawnBall()
    {
        GameObject obj = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        BallController ball = obj.GetComponent<BallController>();
        return ball;
    }
}