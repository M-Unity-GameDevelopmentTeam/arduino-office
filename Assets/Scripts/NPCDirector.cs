using System.Collections;
using UnityEngine;
using Game.Input;
using Unity.Burst;
using UnityEngine.Events;
[BurstCompile] public class NPCDirector : MonoBehaviour, IItem
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private UnityEvent OnDialogEnd;
    [SerializeField] private bool TriggerOnClick = true;
    private DialGiver DialGiver;
    private Collider NPCCollider;
    private void Start()
    {
        DialGiver = GetComponent<DialGiver>();
        NPCCollider = GetComponent<Collider>();
    }
    public void InteractWithItem()
    {
        if (TriggerOnClick) StartCoroutine(Dialog());
    }
    public void ManualInteractWithItem()
    {
        StartCoroutine(Dialog());
    }
    public IEnumerator Dialog()
    {
        Camera.SetActive(true);
        if (!NPCCollider.Equals(null)) NPCCollider.enabled = false;
        InputHandler.ToggleActionMap(InputHandler.Dialog);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(DialGiver.StartDialAndWaitUntilEnd());
        Camera.SetActive(false);
        if (!NPCCollider.Equals(null)) NPCCollider.enabled = true;
        InputHandler.ToggleActionMap(InputHandler.Player);
        OnDialogEnd.Invoke();
    }
}
