using UnityEngine;
using UnityEngine.UI;

public class GarlandChecker : MonoBehaviour
{
    private MiniGameHandler GameHandler;
    [SerializeField] private int LightsCount;
    private Image[] Bulbs;
    [SerializeField] private int Count;
    private void Awake()
    {
        GameHandler = FindFirstObjectByType<MiniGameHandler>();
        Bulbs = GetComponentsInChildren<Image>();
    }
    private void Update()
    {
        Count=0;
        foreach(Image im in Bulbs)
        {
            if (im.color.a.Equals(1))
            {
                Count++;
            }
        }
        GameHandler.IsEnded = Count.Equals(LightsCount);
        Count=0;
    }
}
