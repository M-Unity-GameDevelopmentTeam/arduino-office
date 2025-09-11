using System; using UnityEngine; using Unity.Burst; 
[BurstCompile] public class GameObjectFinder : MonoBehaviour
{
    private GameObject[] FoundObjects;
    [SerializeField] private string[] GameObjectsToFind;
    private GameObject[] FoundDisabledObjects;
    [SerializeField] private string[] DisabledGameObjectsToFind;
    public GameObject[] FoundObjectsF {get => FoundObjects;}
    public GameObject[] FoundDisabledObjectsF {get => FoundDisabledObjects;}
    private byte i;
    private void Awake()
    {
        Array.Resize(ref FoundObjects, GameObjectsToFind.Length);
        Array.Resize(ref FoundDisabledObjects, DisabledGameObjectsToFind.Length);
        i=0;
        foreach (string DisabledGameObjectToFind in DisabledGameObjectsToFind)
        {
            FoundDisabledObjects[i] = GameObject.Find(DisabledGameObjectToFind);
            FoundDisabledObjects[i].SetActive(false);
            i++;
        }
        i=0;
        foreach (string GameObjectToFind in GameObjectsToFind)
        {
            FoundObjects[i] = GameObject.Find(GameObjectToFind);
            i++;
        }
    }
}
