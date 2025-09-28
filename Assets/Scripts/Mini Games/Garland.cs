using UnityEngine;
using UnityEngine.UI;
public class Garland : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Image lightbulb;
    public void ColorChange()
    {
        lightbulb.color = color;
    }
}
