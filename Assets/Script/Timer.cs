using UnityEngine;

public class Timer
{
    private float currentTime = 0.0f;
    private float maxTime = 0.0f;

    public Timer(float maxTime)
    {
        this.maxTime = maxTime;
    }

    // return if timer is complete
    public bool AddTime(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime > maxTime)
        {
            currentTime = 0.0f;
            return true;
        }
        return false;
    }

    public void Reset()
    {
        currentTime = 0.0f;
    }
    public float GetRatio()
    {
        return currentTime / maxTime;
    }
}
