using UnityEngine;

namespace Q17pD.Brachistochrone.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private float _sensitivity;
        public GameObject _playersRotation;
        private float _xRot, _yRot;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensitivity;

            _xRot -= mouseY;
            _yRot += mouseX;


            _xRot = Mathf.Clamp(_xRot, -90f, 90f);

            _playersRotation.transform.rotation = Quaternion.Euler(new Vector3(_playersRotation.transform.rotation.x, _yRot, _playersRotation.transform.rotation.z));

            transform.rotation = Quaternion.Euler(new Vector3(_xRot, _yRot, transform.rotation.z));
        }
    }
}
