using UnityEngine;

[CreateAssetMenu(fileName = "NewBallData", menuName = "ObjectsData/Ball Data")]
public class BallData : ScriptableObject
{
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float speedIncrease = 0.5f;
    public float InitialSpeed => initialSpeed;
    public float SpeedIncrease => speedIncrease;
}