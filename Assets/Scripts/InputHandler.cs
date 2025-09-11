using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Burst;
using UnityEngine.EventSystems;
namespace Game.Input
{
    [BurstCompile] public class InputHandler : MonoBehaviour
    {
        public static event Action<InputActionMap> OnMapChanged;
        private static MainInputAction input;
        public static InputAction Interact => input.Player.Interact;
        public static InputAction Pointer => input.UI.Point;
        public static InputAction Click => input.Mouse.Click;
        public static InputAction DialogNextPhrase => input.Dialog.DialogNextPhrase;
        public static InputActionMap Player => input.Player;
        public static InputActionMap Dialog => input.Dialog;
        public static InputAction DialogEscape => input.Dialog.Escape;
        public static Vector2 MoveAxis;
        public static Vector2 CustomMoveAxis;
        private void Awake()
        {
            InputHandler[] ExistingObjects = FindObjectsByType<InputHandler>(FindObjectsSortMode.None);
            foreach (InputHandler obj in ExistingObjects)
            {
                if (obj != this)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            DontDestroyOnLoad(gameObject);
            GetComponent<EventSystem>().enabled = true;
            input = new MainInputAction();
            ToggleActionMap(input.Player);
        }
        private void Update()
        {
            MoveAxis.x = ZeroOneRound(Mathf.Lerp(MoveAxis.x, Mathf.Clamp(input.Player.Move.ReadValue<Vector2>().x + CustomMoveAxis.x, -1, 1), 5 * Time.deltaTime));
            MoveAxis.y = ZeroOneRound(Mathf.Lerp(MoveAxis.y, Mathf.Clamp(input.Player.Move.ReadValue<Vector2>().y + CustomMoveAxis.y, -1, 1), 5 * Time.deltaTime));
        }
        private float ZeroOneRound(float value)
        {
            if (Math.Abs(value) <= 0.01)
                return 0;
            else if (Math.Abs(value) >= 0.99)
                return Math.Sign(value);
            return value;
        }
        public static void ToggleActionMap(InputActionMap map)
        {
            if (map.enabled) return;
            input.Disable();
            map.Enable();
            OnMapChanged?.Invoke(map);
        }
    }
}