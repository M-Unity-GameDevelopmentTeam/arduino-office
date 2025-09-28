using System.Collections;
using UnityEngine;
using Game.Input;
using Unity.Burst;
using UnityEngine.SceneManagement;
using MiniGames;
[BurstCompile] public class MiniGamesDirector : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private MiniGamesTypes CHID;
    [SerializeField] private bool CanInteractOnlyOnce;
    private DialGiver DialGiver;
    [SerializeField] private DarkAnimationDirector Dark;
    private MiniGameHandler Handler;
    private void Start()
    {
        DialGiver = GetComponent<DialGiver>();
    }
    public IEnumerator Dialog()
    {
        Camera.SetActive(true);
        InputHandler.ToggleActionMap(InputHandler.Dialog);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(DialGiver.StartDialAndWaitUntilEnd());
        yield return StartCoroutine(LoadGame());
        Camera.SetActive(false);
        PlayerPrefs.SetInt("MiniGames", PlayerPrefs.GetInt("MiniGames",0)+1);
        InputHandler.ToggleActionMap(InputHandler.Player);
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
