using UnityEngine;

[CreateAssetMenu(fileName = "NewBallData", menuName = "ObjectsData/Ball Data")]
public class BallData : ScriptableObject
{
    public float initialSpeed = 5f;
    public float speedIncrease = 0.5f;
}