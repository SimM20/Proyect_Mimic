using UnityEngine;

public class SlowGameplayPowerUp : PowerUp
{
    protected override void HandleCollision(GameObject other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            GameManager.Instance.ChangeGameplayTime(data.TimeToWaitUntilChange);
            Destroy(gameObject);
        }
    }
}
