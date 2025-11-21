using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Game.Unity.Buttons
{
    public sealed class ChangeLanguage : BaseButtonAction
    {
        public override void OnClick()
        {
            var current = LocalizationSettings.SelectedLocale;

            Locale english = null;
            Locale portuguese = null;

            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                if (locale.Identifier.Code.StartsWith("en"))
                    english = locale;
                else if (locale.Identifier.Code.StartsWith("pt"))
                    portuguese = locale;
            }

            if (current.Identifier.Code.StartsWith("en") && portuguese != null)
                LocalizationSettings.SelectedLocale = portuguese;
            else if (current.Identifier.Code.StartsWith("pt") && english != null)
                LocalizationSettings.SelectedLocale = english;
        }
    }
}
