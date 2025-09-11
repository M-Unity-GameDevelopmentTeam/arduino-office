using UnityEngine; using DG.Tweening; using Unity.Burst; 
[BurstCompile] public class ProximityPromptDirector : MonoBehaviour
{
    private RectTransform ProximityPanel;
    private double pos = 0;
    private Canvas Canvas;
    private bool IsRunning = false;
    public bool IsRunningF {get=>IsRunning;}
    private void Awake()
    {
        ProximityPanel = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        Canvas = GetComponent<Canvas>();
    }
    public void Enable()
    {
        //Canvas.enabled = true;
        IsRunning = true;
        pos = 0;
        DOTween.To(() => pos, x => pos = x, 2, 1).OnUpdate(() => {
            ProximityPanel.offsetMin = new Vector2((float)-pos, ProximityPanel.offsetMin.y);
        }).OnComplete(() => {ProximityPanel.offsetMin = new Vector2(-2, ProximityPanel.offsetMin.y);}).SetEase(Ease.InOutCubic);
    }
    public void EnableOLD()
    {

        IsRunning = true;
        pos = 0;
        DOTween.To(() => pos, x => pos = x, 2, 1).OnUpdate(() => {
            ProximityPanel.offsetMin = new Vector2((float)-pos, ProximityPanel.offsetMin.y);
        }).OnComplete(() => { ProximityPanel.offsetMin = new Vector2(-2, ProximityPanel.offsetMin.y); }).SetEase(Ease.InOutCubic);
    }
    public void Disable()
    {
        pos = 2;
        DOTween.To(() => pos, x => pos = x, 0, 1).OnUpdate(() => {
            ProximityPanel.offsetMin = new Vector2((float)-pos, ProximityPanel.offsetMin.y);
        }).OnComplete(() => {ProximityPanel.offsetMin = new Vector2(0, ProximityPanel.offsetMin.y);}).SetEase(Ease.InOutCubic);
        IsRunning = false;
    }
}
