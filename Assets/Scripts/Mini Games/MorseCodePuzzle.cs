using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class MorseCodePuzzle : MonoBehaviour
{
    [SerializeField] private TMP_Text inputText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private InputAction morseInput;
    private SerialController Arduino;
    private bool isHolding = false;
    private float pressTime = 0f;
    private const float holdThreshold = 0.5f; 
    private string inputSequence;
    private MiniGameHandler GameHandler;
    [SerializeField] private string correctSequence;
    private bool gameActive = false;
    private void Awake()
    {
        GameHandler = FindFirstObjectByType<MiniGameHandler>();
        Arduino = FindFirstObjectByType<SerialController>();
        morseInput = new InputAction("MorsePress", InputActionType.Button);
        morseInput.AddBinding("<Keyboard>/space");
        morseInput.performed += ctx => StartPress();
        morseInput.canceled += ctx => EndPress();
        morseInput.Enable();
    }

    public void StartGame()
    {
        gameActive = true;
        inputSequence = null;
        inputText.text = null;
        resultText.text = "Начни вводить последовательность";
        Arduino.SendSerialMessage($"M:{correctSequence}");
    }

    private void Update()
    {
        if (isHolding)
        {
            pressTime += Time.deltaTime;
        }
    }

    private void StartPress()
    {
        if (!isHolding && gameActive)
        {
            isHolding = true;
            pressTime = 0f; 
        }
    }

    private void EndPress()
    {
        if (gameActive)
        {
            isHolding = false;
            if (pressTime >= holdThreshold)
            {
                inputSequence += "-"; 
            }
            else
            {
                inputSequence += ".";
            }
            print(inputSequence);
            inputText.text = inputSequence; 
            if (inputSequence.Length >= correctSequence.Length)
            {
                CheckSequence();
            }
        }
    }
    private void CheckSequence()
    {
        if (inputSequence.Equals(correctSequence))
        {
            resultText.text = "Правильная последовательность";
            gameActive = false;
            GameHandler.IsEnded=true;
        }
        else
        {
            resultText.text = "Неверная последовательность";
            inputSequence = ""; 
            inputText.text = ""; 
        }
    }
}



