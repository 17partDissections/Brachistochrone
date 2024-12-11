using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonsInteraction : MonoBehaviour
{
    private Image _image;
    private TextMeshProUGUI _text;

    private void Awake() { _image = GetComponent<Image>(); _text = GetComponentInChildren<TextMeshProUGUI>(); }

    public void PointerEnter() { _text.color = new Color32(150, 0, 0, 255); }
    public void PointerExit() { _text.color = new Color32(115, 80, 80, 255); /*_image.color = new Color32(255, 255, 255, 255);*/ }
    public void PointerDown() { _text.color = new Color32(150, 0, 0, 255); /*_image.color = new Color32(255, 255, 255, 0);*/ }
}
