using UnityEngine;
using TMPro;
public class SafeLock : MonoBehaviour
{
    [SerializeField] private Transform safeLock;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private int[] Combination;
    private float CurrentRotation;
    private int CurrentStep;
    private int Step;
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
        Arduino.SendSerialMessage("P:20");
        if (int.TryParse(Arduino.ReadSerialMessage(), out int result))
        {
            CurrentStep = result;
            Rotate(CurrentStep);
            CheckStep();
        }
    }
    public void CheckStep()
    {
        if (Step < Combination.Length && Combination[Step].Equals(CurrentStep))
        {
            Debug.Log($"Completed step {Step+1}");
            Step++;
            if (Step >= Combination.Length)
            {
                infoText.text = "Good j*b!";
                GameHandler.IsEnded = true;
            }
        }
    }
    private void Rotate(float angle)
    {
        if (GameHandler.IsEnded || angle.Equals(CurrentRotation)) return;
        UpdateInfoText();
        CurrentRotation = Mathf.Round(angle * 18);
        safeLock.localEulerAngles = new Vector3(0, 0, CurrentRotation);
        UpdateInfoText();
    }
    private void UpdateInfoText()
    {
        infoText.text = $"Steps Completed: {Step}";
    }
    public void ArduinoShutdown() => Arduino.ClearQueue();
}
