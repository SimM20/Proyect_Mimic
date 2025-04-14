using UnityEngine;

public class TopPaddle : MonoBehaviour, IScorable
{
    private Animator anim;
    private void Start() { SetPosition(); anim = GetComponent<Animator>(); }
    private void SetPosition()
    {
        Vector3 screenPos = new Vector3(Screen.width / 2, Screen.safeArea.y + Screen.safeArea.height, Camera.main.nearClipPlane);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = new Vector3(worldPos.x, worldPos.y - (transform.localScale.x / 2), transform.position.z);
    }
    public void OnHitByBall() { GameManager.Instance.UpdatePoints(); HapticManager.Vibrate(); anim.SetTrigger("Hit"); }
}