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
        float zDist = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMin, 0, zDist));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMax, 0, zDist));

        float halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;

        minX = leftEdge.x + halfWidth;
        maxX = rightEdge.x - halfWidth;
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
