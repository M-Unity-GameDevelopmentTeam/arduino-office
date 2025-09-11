using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MiniGamesCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text CounterText;
    [SerializeField] private DarkAnimationDirector Dark;
    private bool IsWork = true;
    private void Awake()
    {
        PlayerPrefs.SetInt("MiniGames", 0);
        StartCoroutine(nameof(UpdateCounter));
    }
    private IEnumerator UpdateCounter()
    {
        while(IsWork)
        {
            CounterText.text = PlayerPrefs.GetInt("MiniGames",0).ToString()+"/4";
            yield return new WaitForSeconds(0.1f);
            if (PlayerPrefs.GetInt("MiniGames",0).Equals(4))
            {
                IsWork = false;
                Dark.Dark();
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
