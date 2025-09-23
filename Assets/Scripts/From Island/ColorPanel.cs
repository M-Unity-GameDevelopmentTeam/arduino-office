using System;
using System.Linq.Expressions;
using UnityEngine;
public class ColorPanel : MonoBehaviour
{
    [SerializeField] private Garland[] LEDs;
    private MiniGameHandler GameHandler;
    private SerialController Arduino;
    private int CurrentButton = 0;
    private bool IsStarted;
    private string TempMessage;
    private void Awake()
    {
        GameHandler = FindFirstObjectByType<MiniGameHandler>();
        Arduino = FindFirstObjectByType<SerialController>();
        Arduino.SetTearDownFunction(ArduinoShutdown);
        Arduino.SendSerialMessage("M:");
    }
    private void Update()
    {
        if (GameHandler.IsEnded || !IsStarted) return;
        Arduino.SendSerialMessage($"B:{CurrentButton}");
        TempMessage = Arduino.ReadSerialMessage();
        try
        {
            CheckStep(TempMessage);
        }
        catch (Exception) { }
    }
    public void StartGame() => IsStarted = true;
    public void CheckStep(string result)
    {
        if (result[0].Equals(CurrentButton.ToString()[0]) && result[2].Equals('0'))
        {
            Arduino.ClearQueue();
            Arduino.SendSerialMessage($"H:{CurrentButton}");
            LEDs[CurrentButton].ColorChange();
            if (!CurrentButton.Equals(2))
                CurrentButton++;
            else
                GameHandler.IsEnded = true;
        }
    }
    public void ArduinoShutdown()
    {
        Arduino.ClearQueue();
        Arduino.SendSerialMessage("L:0");
        Arduino.SendSerialMessage("L:1");
        Arduino.SendSerialMessage("L:2");
    }
        
}
