using UnityEngine; using DG.Tweening;
public class DialogAnim : MonoBehaviour
{
    private RectTransform DialogPanel;
    private void Awake() => DialogPanel = GetComponent<RectTransform>();
    public void OnEnable()
    {
        DialogPanel.sizeDelta= new Vector2(DialogPanel.sizeDelta.x, -5);
        DialogPanel.DOSizeDelta(new Vector2(DialogPanel.sizeDelta.x, 375), 0.5f).SetEase(Ease.InOutCubic);
    }
}
