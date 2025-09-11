using System.Collections;
using UnityEngine;
using Game.Input;
using Unity.Burst;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement;
using MiniGames;
[BurstCompile] public class MiniGamesDirector : MonoBehaviour, IInteractable
{
    [SerializeField] private ProximityPromptDirector ProximityPanel;
    [SerializeField] private GameObject Camera;
    [SerializeField] private MiniGamesTypes CHID;
    private bool IsChecking = true;
    private bool CanInteract;
    [SerializeField] private bool CanInteractOnlyOnce;
    private DialGiver DialGiver;
    private InputAction ActionToHappen;
    [SerializeField] private DarkAnimationDirector Dark;
    private MiniGameHandler Handler;
    private void Start()
    {
        ActionToHappen = InputHandler.Interact;
        DialGiver = GetComponent<DialGiver>();
    }
    public void Update() => CheckAction();
    public void CheckAction()
    {
        if (CanInteract && ActionToHappen.WasReleasedThisFrame())
            StartCoroutine(Dialog());
    }
    public void CheckProximityPanel(bool IsClose)
    {
        if (IsChecking)
        {
            CanInteract = IsClose;
            if (IsClose && !ProximityPanel.IsRunningF)
                ProximityPanel.Enable();
            else if (!IsClose && ProximityPanel.IsRunningF)
                ProximityPanel.Disable();
        }
    }
    public IEnumerator Dialog()
    {
        IsChecking = false;
        Camera.SetActive(true);
        ProximityPanel.Disable();
        InputHandler.ToggleActionMap(InputHandler.Dialog);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(DialGiver.StartDialAndWaitUntilEnd());
        yield return StartCoroutine(LoadGame());
        Camera.SetActive(false);
        PlayerPrefs.SetInt("MiniGames", PlayerPrefs.GetInt("MiniGames",0)+1);
        if (!CanInteractOnlyOnce) 
        {
            IsChecking = true;
            CheckProximityPanel(true);
        }
        else 
        {
            CanInteract = false;
            Destroy(ProximityPanel.gameObject);
        }
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
