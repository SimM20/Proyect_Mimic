using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    private float minX;
    private float maxX;

    private void Start() { SetPosition(); TouchHandler.Instance.OnTouch += Move; }

    private void Move(Vector3 direction)
    {
        float clampedX = Mathf.Clamp(direction.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void SetPosition()
    {
        Vector3 screenPos = new Vector3(Screen.width / 2, Screen.safeArea.y, Camera.main.nearClipPlane);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = new Vector3(worldPos.x, worldPos.y + transform.localScale.x, transform.position.z);

        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMin, transform.position.y, Camera.main.nearClipPlane));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMax, transform.position.y, Camera.main.nearClipPlane));

        minX = leftEdge.x + transform.localScale.y;
        maxX = rightEdge.x - transform.localScale.y;
    }

    public void ChangeLocalScale(float timeUntilChange) { StartCoroutine(RenueveScale(timeUntilChange)); }

    private IEnumerator RenueveScale(float timeUntilChange)
    {
        transform.localScale = data.BigPlayerScale;
        SetPosition();

        yield return new WaitForSeconds(timeUntilChange);

        transform.localScale = data.PlayerScale;
        SetPosition();
    }
}
