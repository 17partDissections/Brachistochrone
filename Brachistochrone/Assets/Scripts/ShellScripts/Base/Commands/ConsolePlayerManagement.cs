using System;
using System.Collections.Generic;
using Q17pD.Brachistochrone;
using Q17pD.Brachistochrone.Player;
using TMPro;
using UnityEngine;

namespace ConsoleShell.BaseCommands
{
    public class ConsolePlayerManagement : ConsoleManagementBase
    {
        private GameObject _fearTextObj;
        private PlayerFear _playerFear;

        public ConsolePlayerManagement()
        {
            FearTextEnableCommand();
            FearTextDisableCommand();
            SetPlayerFearCommand();
        }

        private void FearTextEnableCommand()
        {
            void EnableFearText(ArgumentsShell shell)
            {
                if (_fearTextObj == null) _fearTextObj = UnityEngine.Object.FindFirstObjectByType<FearText>().GetComponentInChildren<TextMeshProUGUI>().gameObject;
                _fearTextObj.SetActive(true);
            }
            AddCommand("/FearText.Enable", new List<Argument>(), EnableFearText);
        }
        private void FearTextDisableCommand()
        {
            void DisableFearText(ArgumentsShell shell)
            {
                if (_fearTextObj == null) _fearTextObj = UnityEngine.Object.FindFirstObjectByType<FearText>().GetComponentInChildren<TextMeshProUGUI>().gameObject;
                _fearTextObj.SetActive(false);
            }
            AddCommand("/FearText.Disable", new List<Argument>(), DisableFearText);
        }
        private void SetPlayerFearCommand()
        {
            void SetPlayerFear(ArgumentsShell shell)
            {
                if (_playerFear == null) _playerFear = UnityEngine.Object.FindAnyObjectByType<PlayerFear>();
                _playerFear.CurrentPlayerFear = (int)shell.GetNumber("idk");
            }
            AddCommand("/Player.PlayerFear.SetFear", new List<Argument>(), SetPlayerFear);
        }
    }
}