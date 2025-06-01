using UnityEngine;

public class BigPaddlePowerUp : PowerUp
{
    protected override void HandleCollision(GameObject other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeLocalScale(data.TimeToWaitUntilChange);
            Destroy(gameObject);
        }
    }
}
