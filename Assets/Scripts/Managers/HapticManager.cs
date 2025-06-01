using UnityEngine;

public static class HapticManager
{
    private static bool hapticsEnabled = true;

    public static void Vibrate() { if (hapticsEnabled) Handheld.Vibrate(); }

    public static void SetHapticsEnabled(bool enabled) { hapticsEnabled = enabled; }

    public static bool IsHapticsEnabled() { return hapticsEnabled; }
}
