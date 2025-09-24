using MiniGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MiniGameHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private GameObject[] Schemes;
    private MiniGamesTypes CurrentCHID;
    private DarkDirector Dark;
    private string CurrentPuzzle;
    public bool IsEnded;
    const string PasswordBreakScene = "Puzzle1";
    const string MorseCodeScene = "Puzzle2";
    const string GarlandScene = "Puzzle3";
    const string VaultScene = "Puzzle4";
    const string DoorScene = "Puzzle5";
    private GameObject CurrentScheme;
    private void Awake()
    {
        Dark = FindFirstObjectByType<DarkDirector>();
        CurrentCHID = (MiniGamesTypes)Enum.Parse(typeof(MiniGamesTypes), "MorseCode");//PlayerPrefs.GetString("CHID", "NULL"));
        switch(CurrentCHID)
        {
            case MiniGamesTypes.PasswordBreaker:
                CurrentPuzzle = PasswordBreakScene;
                CurrentScheme = Schemes[0];
                break;
            case MiniGamesTypes.MorseCode:
                CurrentPuzzle = MorseCodeScene;
                CurrentScheme = Schemes[1];
                break;
            case MiniGamesTypes.Garland:
                CurrentPuzzle = GarlandScene;
                CurrentScheme = Schemes[2];
                break;
            case MiniGamesTypes.VaultUnlock:
                CurrentPuzzle = VaultScene;
                CurrentScheme = Schemes[3];
                break;
            case MiniGamesTypes.DoorUnlock:
                CurrentPuzzle = DoorScene;
                CurrentScheme = Schemes[4];
                break;
            case MiniGamesTypes.NULL:
                Debug.LogError("CHID is not defined in NPC");
                break;
            default:
                Debug.LogError("CHID has no actions to do");
                break;
        }
        Instantiate(CurrentScheme,CanvasGroup.transform);
    }
    public void GameHandler()
    {
        StartCoroutine(LoadGame(CurrentPuzzle));
        PlayerPrefs.SetString("CHID","NULL");
    }
    private IEnumerator LoadGame(string SceneToLoad)
    {
        //Dark.Dark();
        yield return new WaitForSeconds(1);
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
        //Dark.UnDark();
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
    }
    public void UnLoadGame()
    {
        SceneManager.UnloadSceneAsync(CurrentPuzzle);
    }
}
