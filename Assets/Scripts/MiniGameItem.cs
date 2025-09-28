using System.Collections;
using UnityEngine;
using Unity.Burst;
using UnityEngine.SceneManagement;
using MiniGames;
using UnityEngine.Events;
[BurstCompile] public class MiniGameItem : MonoBehaviour, IItem
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private bool DisableCameraAfterComplete = true;
    [SerializeField] private MiniGamesTypes CHID;
    [SerializeField] private DarkDirector Dark;
    [SerializeField] private UnityEvent OnGameComplete;
    [SerializeField] private float DelayBeforeEvent = 0;
    private MiniGameHandler Handler;
    private bool IsInteracted;
    public void InteractWithItem()
    {
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
        if (DelayBeforeEvent.Equals(0)) OnGameComplete.Invoke();
        yield return new WaitForSeconds(1);
        Handler.UnLoadGame();
        SceneManager.UnloadSceneAsync("SchemeScene");
        Dark.UnDark();
        if (!DelayBeforeEvent.Equals(0)) OnGameComplete.Invoke();
        Camera.SetActive(!DisableCameraAfterComplete);
        yield return new WaitUntil(() => SceneManager.loadedSceneCount.Equals(1));
        yield return new WaitForSeconds(1);
    }
}
