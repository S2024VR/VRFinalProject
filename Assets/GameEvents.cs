using UnityEngine.Events;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public UnityEvent onSupplyDropCollected;

    public void SupplyDropCollected()
    {
        if (onSupplyDropCollected != null)
        {
            onSupplyDropCollected.Invoke();
        }
    }
}
