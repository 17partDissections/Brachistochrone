using UnityEngine;

namespace Q17pD.Brachistochrone.Player
{
    public class GroundCheck : MonoBehaviour
    {
        PlayerMovement _playerMovement;
        private void Awake()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 7) //should be Floor layer
            {
                _playerMovement.Grounded = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 7) //should be Floor layer
            {
                _playerMovement.Grounded = false;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == 7) //should be Floor layer
            {
                _playerMovement.Grounded = true;
            }
        }
    }
}
