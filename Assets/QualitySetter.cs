using UnityEngine;

public static class QualitySetter
{
    public static void Apply()
    {
#if UNITY_ANDROID
        SetQualityForAndroid();
#endif
    }

    private static void SetQualityForAndroid()
    {
        int ram = SystemInfo.systemMemorySize;
        int cores = SystemInfo.processorCount;

        QualitySettings.vSyncCount = 1;

        if (ram < 3000 || cores < 4) //Gama baja
        {
            QualitySettings.SetQualityLevel(1);
            Application.targetFrameRate = 30;
        }
        else if (ram < 5000) //Gama media
        {
            QualitySettings.SetQualityLevel(2);
            Application.targetFrameRate = -1;
        }
        else //Gama alta
        {
            QualitySettings.SetQualityLevel(3);
            Application.targetFrameRate = -1;
        }

        Debug.Log($"AutoDetec -> RAM: {ram}MB | Cores: {cores} | QualityPreset: {QualitySettings.GetQualityLevel()}");
    }
}
