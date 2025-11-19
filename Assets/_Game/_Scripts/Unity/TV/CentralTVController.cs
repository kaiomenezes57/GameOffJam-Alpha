using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using VContainer;

namespace Game.Unity.TV
{
    public sealed class CentralTVController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _previous;
        [SerializeField] private TextMeshProUGUI _current;
        [SerializeField] private TextMeshProUGUI _next;

        public void SetPrograms(LocalizedString previousProgram, 
            LocalizedString currentProgram, 
            LocalizedString nextProgram)
        {
            try
            {
                SetPrograms(previousProgram.GetLocalizedString(),
                    currentProgram.GetLocalizedString(),
                    nextProgram.GetLocalizedString());
            }
            catch
            {
#if DEBUG
                UnityEngine.Debug.LogError("Localization not ready yet.");
#endif
            }
        }

        public void SetPrograms(string previous, string current, string next)
        {
            _previous.text = previous;
            _current.text = current;
            _next.text = next;
        }
    }
}
