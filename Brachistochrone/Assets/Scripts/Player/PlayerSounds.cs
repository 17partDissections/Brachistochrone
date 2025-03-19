using UnityEngine;
namespace Q17pD.Brachistochrone.PLayer
{
    public class PlayerSounds : MonoBehaviour
    {
        [Header("Walk sounds")]
        [SerializeField] public AudioClip[] WalkSounds_Rock;
        [SerializeField] public AudioClip[] WalkSounds_Gravel;
        [SerializeField] public AudioClip[] WalkSounds_Metal;
        [SerializeField] public AudioClip[] WalkSounds_Water;
        [Header("Run sounds")]
        [SerializeField] public AudioClip[] RunSounds_Rock;
        [SerializeField] public AudioClip[] RunSounds_Gravel;
        [SerializeField] public AudioClip[] RunSounds_Metal;
        [SerializeField] public AudioClip[] RunSounds_Water;
        [Header("Jump sounds")]
        [SerializeField] public AudioClip[] JumpSounds_Rock;
        [SerializeField] public AudioClip[] JumpSounds_Gravel;
        [SerializeField] public AudioClip[] JumpSounds_Metal;
        [SerializeField] public AudioClip[] JumpSounds_Water;
        public AudioClip RandomizeSound(AudioClip[] sounds) { return sounds[Random.Range(0, sounds.Length)]; }
    }
}
