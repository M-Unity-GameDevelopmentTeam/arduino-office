using UnityEngine; using DG.Tweening; using Unity.Burst; 
[BurstCompile] public class DarkAnimationDirector : MonoBehaviour, IDarkDirector
{
    [SerializeField] private bool IsDarkOnAwake;
    [SerializeField] private Vector3 StartScale = Vector3.zero;
    [SerializeField] private Vector3 EndScale = new Vector3(0,0,0);
    private Transform SpriteDark;
    private void Awake()
    {
        SpriteDark = GetComponent<Transform>();
        if(IsDarkOnAwake) SpriteDark.localScale = StartScale; UnDark();  
    }
    public void Dark() => SpriteDark.DOScale(StartScale,1).SetEase(Ease.InOutCubic);
    public void UnDark() => SpriteDark.DOScale(EndScale,1).SetEase(Ease.InOutCubic);
}
