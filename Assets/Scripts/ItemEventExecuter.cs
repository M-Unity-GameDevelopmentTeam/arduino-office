using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ItemEventExecuter : MonoBehaviour, IItem
{
    [SerializeField] private DarkDirector Dark;
    [SerializeField] private GameObject Camera;
    [SerializeField] private UnityEvent A;
    [SerializeField] private float EventDuration;
    [SerializeField] private float DelayBeforeEvent;
    [SerializeField] private bool DoAnimation = true;
    public void InteractWithItem()
    {
        StartCoroutine(DoAnimation ? nameof(EventExecutionWithAnimation) : nameof(EventExecution));
    }
    private IEnumerator EventExecutionWithAnimation()
    {
        Camera.SetActive(true);
        yield return new WaitForSeconds(DelayBeforeEvent);
        Dark.Dark();
        yield return new WaitForSeconds(0.5f);
        A.Invoke();
        yield return new WaitForSeconds(EventDuration);
        Camera.SetActive(false);
        yield return new WaitForSeconds(DelayBeforeEvent - 1);
        Dark.UnDark();
    }

    private IEnumerator EventExecution()
    {
        Camera.SetActive(true);
        A.Invoke();
        yield return 1;
    }
}
