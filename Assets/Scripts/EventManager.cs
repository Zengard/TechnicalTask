using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnUpdatePigsCount = new UnityEvent();
    public static UnityEvent OnUpdateBirdsQueue = new UnityEvent();

    public static void UpdatePigsCount()
    {
        OnUpdatePigsCount.Invoke();
    }

    public static void UpdateBirdsQueue()
    {
        OnUpdateBirdsQueue.Invoke();
    }
}

