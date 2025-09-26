using System.Collections;
using UnityEngine;
using Game.Input;
using Unity.Burst; 
[BurstCompile] public class NPCDirector : MonoBehaviour, IItem
{
    [SerializeField] private GameObject Camera;
    private DialGiver DialGiver;
    private Collider coll;
    private void Start()
    {
        DialGiver = GetComponent<DialGiver>();
        coll = GetComponent<Collider>();
    }
    public void InteractWithItem()
    {
        StartCoroutine(Dialog());
    }
    public IEnumerator Dialog()
    {
        Camera.SetActive(true);
        coll.enabled = false;
        InputHandler.ToggleActionMap(InputHandler.Dialog);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(DialGiver.StartDialAndWaitUntilEnd());
        Camera.SetActive(false);
        coll.enabled = true;
        InputHandler.ToggleActionMap(InputHandler.Player);
    }
}
