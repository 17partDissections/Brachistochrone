using UnityEngine;
using Zenject;

public class GameStart : MonoBehaviour
{
    private Animation _animation;
    private AudioHandler _audioHandler;
    [SerializeField] private AudioClip _gameStartSound;

    [Inject] private void Construct(AudioHandler ahandler) {  _audioHandler = ahandler; }
    public void StartGame()
    {
        _audioHandler.PlaySFX(_gameStartSound, 1);
        _animation.Play("PanelDarkening");

    }
    

}
