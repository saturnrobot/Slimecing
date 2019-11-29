using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float durationOfTick = 1f;
    [SerializeField] private float loopFor = 0f;
    [SerializeField] private bool loopInfinite = false;
    [SerializeField] private UnityEvent onTimerEnd = new UnityEvent();

    private bool looping = false;
    private void Start()
    {
        InitializeTimer();
    }

    public void InitializeTimer()
    {
        if (loopFor > 0f)
        {
            looping = true;
            StartCoroutine(LoopTimeSubtractor());
        }
        StartCoroutine(StartTimer());
    }

    public void FinishTimerEarly()
    {
        onTimerEnd?.Invoke();
        StopAllCoroutines();
    }

    private IEnumerator LoopTimeSubtractor()
    {
        while(loopFor > 0)
        {
            yield return new WaitForSeconds(0.1f);
            loopFor -= 0.1f;
        }
        looping = false;
    }

    private IEnumerator StartTimer()
    {
        while (loopInfinite || looping)
        {
            yield return new WaitForSeconds(durationOfTick);
            onTimerEnd?.Invoke();
        }
    }
}
