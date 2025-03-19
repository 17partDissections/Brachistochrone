using System.Collections;
using Q17pD.Brachistochrone.Player;
using TMPro;
using UnityEngine;

namespace Q17pD.Brachistochrone
{
    public class FearText : MonoBehaviour
    {
        [SerializeField] private GameObject _fearTextGameObject;
        private TextMeshProUGUI _text;
        [SerializeField] private PlayerFear _playerFear;
        private WaitForSeconds _sleep;
        private void Start()
        {
            _text = _fearTextGameObject.GetComponent<TextMeshProUGUI>();
            _sleep = _playerFear.Sleep;
            StartCoroutine(FearTextCoroutine());
        }

        private IEnumerator FearTextCoroutine()
        {
            while (_fearTextGameObject.activeSelf)
            {
                _text.text = $"Fear: {_playerFear.CurrentPlayerFear.ToString()}";
                yield return _sleep;
            }
        }
    }
}
