using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private float pitchIncrease = 0.02f;

    private void Start() { GameManager.OnUpdatePoints += IncreasePitch; }
    private void IncreasePitch(int score, int highScore) { bgMusic.pitch = Mathf.Clamp(1f + score * pitchIncrease, 1f, 2f); }
}
