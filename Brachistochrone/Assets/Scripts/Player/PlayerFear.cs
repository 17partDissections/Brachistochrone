using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Q17pD.Brachistochrone.Player
{
    public class PlayerFear : MonoBehaviour
    {
        public int CurrentPlayerFear;
        [SerializeField] private float _playerFearSleepTime;
        public WaitForSeconds Sleep;
        [SerializeField] private List<AudioClip> _ambientSounds;
        [SerializeField] private List<Sprite> _pictureScreamers;
        private EventBus _bus;
        private AudioHandler _audioHandler;

        [Inject] private void Construct(EventBus bus, AudioHandler audioHandler)
        {
            _bus = bus;
            _audioHandler = audioHandler;
            Sleep = new WaitForSeconds(_playerFearSleepTime);
        }
        private IEnumerator Start()
        {
            while (true)
            {
                yield return Sleep;
                CurrentPlayerFear++;
                if (CurrentPlayerFear % 10 == 0)
                {


                    if (CurrentPlayerFear == 100)
                    {
                        _bus.FearFullEvent?.Invoke();
                        CurrentPlayerFear = 0;
                    }
                    else
                        _bus.FearAdditionalEvent?.Invoke();
                }
            }
        }
        private void PlayAmbientSound() { _audioHandler.PlaySFX(_ambientSounds[Random.Range(0, _ambientSounds.Count)], 0.5f); }
        private void ShowPictureScreamer()
        {
            //idk
        }
    }
}
