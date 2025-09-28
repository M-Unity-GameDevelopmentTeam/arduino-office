using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private DarkAnimationDirector Dark;
    private void Awake() => Dark = FindAnyObjectByType<DarkAnimationDirector>();
    public void LoadScene(string scene) => StartCoroutine(FadeLoadScene(scene));
    private IEnumerator FadeLoadScene(string scene)
    {
        Dark.Dark();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
