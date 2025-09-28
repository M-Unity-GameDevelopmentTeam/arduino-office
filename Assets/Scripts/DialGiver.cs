using System.Collections; using UnityEngine; using Unity.Burst; 
[BurstCompile] public class DialGiver : MonoBehaviour
{
    [SerializeField] private GameObject DialogPanel;
    [SerializeField] private TextAsset JsonEN; 
    [SerializeField] private TextAsset JsonRU;
    [SerializeField] private TextAsset JsonToUse; 
    [SerializeField] private AudioClip[] TextSoundsToUse;
    private void Awake()
    {
        JsonToUse = PlayerPrefs.GetInt("Language",0).Equals(1) ? JsonRU : JsonEN;
    }
    private void Start()
    {
        //DialogPanel = //GameObject.Find("Systems").GetComponent<GameObjectFinder>().FoundDisabledObjectsF[0];
    }
    public void GiveDial()
    {
        DialogPanel.GetComponent<Dial>().JsonFileF = JsonToUse;
        DialogPanel.GetComponent<Dial>().TextSoundF = TextSoundsToUse;
        DialogPanel.SetActive(true);
        StartCoroutine(DialogPanel.GetComponent<Dial>().Dialog());
    }
    public IEnumerator StartDialAndWaitUntilEnd()
    {
        GiveDial();
        yield return new WaitUntil(() => DialogPanel.GetComponent<Dial>().IsEndedF);
        DialogPanel.GetComponent<Dial>().IsEndedF = false;
        yield return true;
    }							
}
