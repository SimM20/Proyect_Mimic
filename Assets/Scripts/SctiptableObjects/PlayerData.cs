using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private Vector3 playerScale;
    [SerializeField] private Vector3 bigPlayerScale;
    public Vector3 PlayerScale => playerScale;
    public Vector3 BigPlayerScale => bigPlayerScale;
}
