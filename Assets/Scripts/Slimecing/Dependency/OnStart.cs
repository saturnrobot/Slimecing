using Slimecing.Dependency;
using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    [SerializeField] private int loops = 2;
    [SerializeField] private UnityEvent onTimerEnd = null;

    private Timer timer;

    private void Start()
    {
        Debug.Log("start");
        // Creates new instance of the namespace 'Timer'
        timer = new Timer(duration, loops);

        // Listen to events created by Timer
        timer.OnTimerEnd += HandleTimerEnd;
    }

    private void HandleTimerEnd()
    {
        // Calls event (alerts listeners)
        onTimerEnd.Invoke();

        // Any code that you wish to be executed whenever the Timer loops belongs here
        Debug.Log("loop finished");
    }

    private void Update() => timer.Tick(Time.deltaTime);
}