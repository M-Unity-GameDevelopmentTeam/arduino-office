using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
public class SafeLock : MonoBehaviour
{
    [SerializeField] private Transform safeLock;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private float rotationStep = 5f;
    [SerializeField] private float[] combination = { 240f, 120f, 300f };
    [SerializeField] private bool[] correctDirections = { true, false, true };
    private float currentRotation = 0f;
    private int step = 0;
    private bool isUnlocked = false;
    private float lastInputDirection = 0f;
    private MiniGameHandler GameHandler;
    private SerialController Arduino;
    private void Awake()
    {
        GameHandler = FindFirstObjectByType<MiniGameHandler>();
        Arduino = FindFirstObjectByType<SerialController>();
        Arduino.SetTearDownFunction(lightsShutdown);
    }
    private void Update()
    {
        Arduino.SendSerialMessage("P:360");
        if (int.TryParse(Arduino.ReadSerialMessage(), out int result))
            RotateOverwrite(result);
    }
    public void RotateLeft()
    {
        lastInputDirection = 1f;
        Rotate(rotationStep);
    }

    public void RotateRight()
    {
        lastInputDirection = -1f;
        Rotate(-rotationStep);
    }

    public void CheckStep()
    {
        float angle = currentRotation % 360f;
        if (angle < 0) angle += 360f;
        float targetAngle = combination[step];
        float delta = Mathf.Abs(angle - targetAngle);
        if (delta < 2f || Mathf.Abs(delta - 360f) < 2f)
        {
            Debug.Log("Выполнен шаг " + step);
            step++;
            if (step >= combination.Length)
            {
                isUnlocked = true;
                GameHandler.IsEnded = true;
            }
        }
    }

    private void Rotate(float angle)
    {
        if (isUnlocked) return;
        currentRotation += angle;
        safeLock.localRotation = Quaternion.Euler(0, 0, Mathf.Round(currentRotation));
        UpdateInfoText();
    }
    private void RotateOverwrite(float angle)
    {
        if (isUnlocked || angle.Equals(currentRotation)) return;
        currentRotation = angle;
        safeLock.localEulerAngles = new Vector3(0, 0, Mathf.Round(currentRotation));
        UpdateInfoText();
    }
    private int GetCurrentNumber()
    {
        float angle = currentRotation % 360f;
        if (angle < 0) angle += 360f;
        return Mathf.RoundToInt(6 - (angle / 360f * 6)) % 6;
    }
    private void UpdateInfoText()
    {
        float angle = currentRotation % 360f;
        if (angle < 0) angle += 360f;
        int number = GetCurrentNumber();
        string direction = lastInputDirection > 0 ? "по часовой" : "против";
        infoText.text = "Угол: " + angle.ToString("F1") + "°\n" + "Направление: " + direction + " стрелке";
    }
    public void lightsShutdown()
    {
        Arduino.enabled = false;
    }
}
