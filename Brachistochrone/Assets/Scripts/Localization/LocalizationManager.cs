/*
 * DFT Games Studios
 * All rights reserved 2009-Present
 */
using UnityEngine;
using DFTGames.Localization;

namespace DFTGames.Localization
{
    public class LocalizationManager : MonoBehaviour
    {

        public void SetEnglish()
        {
            Localize.SetCurrentLanguage(SystemLanguage.English);
            LocalizeImage.SetCurrentLanguage(SystemLanguage.English);
        }

        public void SetRussian()
        {
            Localize.SetCurrentLanguage(SystemLanguage.Russian);
            LocalizeImage.SetCurrentLanguage(SystemLanguage.Russian);
        }
    }
}