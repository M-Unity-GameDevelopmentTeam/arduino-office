using UnityEngine;
using TMPro;
public class DoorLock : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText;
    private MiniGameHandler GameHandler;
    private SerialController Arduino;
    private void Awake()
    {
        GameHandler = FindFirstObjectByType<MiniGameHandler>();
        Arduino = FindFirstObjectByType<SerialController>();
        Arduino.SetTearDownFunction(ArduinoShutdown);
    }
    private void Update()
    {
        if (GameHandler.IsEnded)
        {
            ArduinoShutdown();
            return;
        }
        Arduino.SendSerialMessage("K:0");
        if (int.TryParse(Arduino.ReadSerialMessage(), out int result))
        {
            CheckStep(result);
        }
    }
    public void CheckStep(int result)
    {
        if (result.Equals(1))
        {
            infoText.text = "Good j*b!";
            GameHandler.IsEnded = true;
        }
    }
    public void ArduinoShutdown() => Arduino.ClearQueue();
}
