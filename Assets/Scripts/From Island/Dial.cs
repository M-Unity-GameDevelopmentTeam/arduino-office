using System.Collections;
using TMPro; 
using UnityEngine; 
using Game.Input; 
using DG.Tweening; 
using Unity.Burst;
[BurstCompile]
public class Dial : MonoBehaviour
{
    [SerializeField] private TMP_Text DialogText;
    [SerializeField] private TMP_Text LabelText;
    [SerializeField] private AudioClip ButtonSound;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private Dialogs.Part DialogPart;
    [SerializeField] private TextAsset JsonFile;
    public TextAsset JsonFileF { set => JsonFile = value; }
    [SerializeField] private AudioClip[] TextSound;
    public AudioClip[] TextSoundF { set => TextSound = value; }
    private bool IsEnded = false;
    public bool IsEndedF { get => IsEnded; set => IsEnded = value; }
    public IEnumerator Dialog()
    {
        DialogPart = Dialogs.CreateFromJSON(JsonFile.text);
        DialogText.text = null;
        LabelText.text = DialogPart.Label;
        yield return new WaitForSeconds(0.5f);
        foreach (string TextPart in DialogPart.Texts)
        {
            DialogText.text = null;
            foreach (char a in TextPart)
            {
                AudioSource.PlayOneShot(TextSound[Random.Range(0, TextSound.Length - 1)]);
                DialogText.text += a;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitUntil(() => InputHandler.DialogNextPhrase.WasReleasedThisFrame());
            AudioSource.PlayOneShot(ButtonSound);
        }
        IsEnded = true;
        DialogText.text = null;
        GetComponent<RectTransform>().DOSizeDelta(new Vector2(GetComponent<RectTransform>().sizeDelta.x, -5), 0.75f).SetEase(Ease.InOutCubic).OnComplete(() => { gameObject.SetActive(false); });
    }
}
