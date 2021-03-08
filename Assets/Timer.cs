using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

    public void ResetTimer()
    {
        this.stopWatch.Reset();
    }

    public void StartTimer()
    {
        this.ResetTimer();
        stopWatch.Start();
    }

    public void StopTimer()
    {
        stopWatch.Stop();
    }

    public int GetCurrentTime()
    {
        return (int)(this.stopWatch.ElapsedMilliseconds / 1000f);
    }

    private void FixedUpdate()
    {
        int currentTime = this.GetCurrentTime();
        Debug.Log("Current Time: " + currentTime);
        this.timerText.text = currentTime.ToString();
    }
}
