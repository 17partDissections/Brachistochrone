using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioSlidersValueText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    //private void Awake() { _text = GetComponent<TextMeshProUGUI>(); }
    public void OnValueChanged(float percent) { _text.text = percent.ToString(); }
}
