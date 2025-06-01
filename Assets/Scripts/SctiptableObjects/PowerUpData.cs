using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUps/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToWaitUntilChange;
    public float Speed => speed;
    public float TimeToWaitUntilChange => timeToWaitUntilChange;
}
