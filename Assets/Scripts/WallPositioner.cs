using UnityEngine;

public class WallPositioner : MonoBehaviour
{
    [SerializeField] private enum WallSide { Left, Right, Top }
    [SerializeField] private WallSide side;
    [SerializeField] private float thickness = 0.5f;
    [SerializeField] private float padding = 0.1f;

    private void Start()
    {
        Camera cam = Camera.main;
        Rect safeArea = Screen.safeArea;

        Vector3 leftWorld = cam.ScreenToWorldPoint(new Vector3(safeArea.xMin, safeArea.center.y, cam.nearClipPlane));
        Vector3 rightWorld = cam.ScreenToWorldPoint(new Vector3(safeArea.xMax, safeArea.center.y, cam.nearClipPlane));
        Vector3 topWorld = cam.ScreenToWorldPoint(new Vector3(safeArea.center.x, safeArea.yMax, cam.nearClipPlane));
        Vector3 bottomWorld = cam.ScreenToWorldPoint(new Vector3(safeArea.center.x, safeArea.yMin, cam.nearClipPlane));
        Vector3 centerWorld = cam.ScreenToWorldPoint(new Vector3(safeArea.center.x, safeArea.center.y, cam.nearClipPlane));

        float usableWidth = Mathf.Abs(rightWorld.x - leftWorld.x);
        float usableHeight = Mathf.Abs(topWorld.y - bottomWorld.y);

        switch (side)
        {
            case WallSide.Left:
                transform.position = new Vector3(leftWorld.x - thickness / 2f - padding, centerWorld.y, 0f);
                transform.localScale = new Vector3(thickness, usableHeight + 2 * padding, 1f);
                break;

            case WallSide.Right:
                transform.position = new Vector3(rightWorld.x + thickness / 2f + padding, centerWorld.y, 0f);
                transform.localScale = new Vector3(thickness, usableHeight + 2 * padding, 1f);
                break;

            case WallSide.Top:
                transform.position = new Vector3(centerWorld.x, topWorld.y + thickness / 2f + padding, 0f);
                transform.localScale = new Vector3(usableWidth + 2 * padding, thickness, 1f);
                break;
        }
    }
}