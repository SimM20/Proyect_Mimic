using UnityEngine;

public static class QualitySetter
{
    public static void Apply() { SetQuality(); }

    private static void SetQuality()
    {
        int ram = SystemInfo.systemMemorySize;
        int cores = SystemInfo.processorCount;

        QualitySettings.vSyncCount = 1;

        if (ram < 3000 || cores < 4) //Gama baja
        {
            QualitySettings.SetQualityLevel(0);
            Application.targetFrameRate = 30;
        }
        else if (ram < 5000) //Gama media
        {
            QualitySettings.SetQualityLevel(1);
            Application.targetFrameRate = -1;
        }
        else //Gama alta
        {
            QualitySettings.SetQualityLevel(2);
            Application.targetFrameRate = -1;
        }
    }
}
