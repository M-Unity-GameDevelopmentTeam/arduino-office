/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class AA : MonoBehaviour
{
    [SerializeField] private string MorseCode;
    public SerialController serialController;

    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

        serialController.SetTearDownFunction(lightsShutdown);

        Debug.Log("Press 1 or 2 to execute some actions");
    }

    // Executed each frame
    void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        if (Keyboard.current.digit1Key.wasReleasedThisFrame)
        {
            Debug.Log("Sending lights ON");
            serialController.SendSerialMessage($"1:{MorseCode}");
        }

        if (Keyboard.current.digit2Key.wasReleasedThisFrame)
        {
            Debug.Log("Sending lights OFF");
            serialController.SendSerialMessage("2:100");
        }


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + message);
    }

    // Tear-down function for the hardware at the other side of the COM port
    public void lightsShutdown()
    {
        Debug.Log("Executing teardown");
        serialController.SendSerialMessage("X");
    }
}
