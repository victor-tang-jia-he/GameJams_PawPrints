using UnityEngine;

public class Timer
{
    private float currentTime = 0.0f;
    private float maxTime = 0.0f;

    Timer(float maxTime)
    {
        this.maxTime = maxTime;
    }

    bool AddTime(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime > maxTime)
        {
            currentTime = 0.0f;
            return true;
        }
        return false;
    }

    void Reset()
    {
        currentTime = 0.0f;
    }
    float GetRatio()
    {
        return currentTime / maxTime;
    }
}
