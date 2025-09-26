using System.Collections;
using UnityEngine;
using Unity.Burst;
using UnityEngine.SceneManagement;
using MiniGames;
[BurstCompile] public class MiniGameItem : MonoBehaviour, IItem
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private Room Room;
    private bool IsInteracted;
    [SerializeField] private MiniGamesTypes CHID;
    [SerializeField] private DarkDirector Dark;
    private MiniGameHandler Handler;
    public void InteractWithItem()
    {
        print(Room.FRoomID);
        IsInteracted = !IsInteracted;
        Camera.SetActive(IsInteracted);
        StartCoroutine(Dialog());
    }
    public IEnumerator Dialog()
    {
        yield return StartCoroutine(LoadGame());
        PlayerPrefs.SetInt("MiniGames", PlayerPrefs.GetInt("MiniGames",0)+1);
    }
    public IEnumerator LoadGame()
    {
        Dark.Dark();
        yield return new WaitForSeconds(1);
        Dark.UnDark();
        PlayerPrefs.SetString("CHID", CHID.ToString());
        SceneManager.LoadScene("SchemeScene", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1);
        Handler = FindFirstObjectByType<MiniGameHandler>();
        yield return new WaitUntil(() => Handler.IsEnded);
        Dark.Dark();
        yield return new WaitForSeconds(1);
        Dark.UnDark();
        Handler.UnLoadGame();
        SceneManager.UnloadSceneAsync("SchemeScene");
        yield return new WaitUntil(() => SceneManager.loadedSceneCount.Equals(1));
        yield return new WaitForSeconds(1);
    }
}
