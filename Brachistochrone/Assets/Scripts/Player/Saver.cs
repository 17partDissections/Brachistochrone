using System;
using UnityEngine.InputSystem;

namespace Q17pD.Brachistochrone.Player
{
    public class Saver : IDisposable
    {
        private PlayerActionMap _playerActionMap;
        private MasterSave _masterSave;
        private Saver(PlayerActionMap map, MasterSave masterSave)
        {
            _playerActionMap = map;
            _masterSave = masterSave;
            _playerActionMap.Player.SaveAllData.performed += Save;
        }
        public void Save(InputAction.CallbackContext context) 
        {
            _masterSave.SaveAllData(); 
        }
        public void Dispose() { _playerActionMap.Player.SaveAllData.performed -= Save; }

    }
}
