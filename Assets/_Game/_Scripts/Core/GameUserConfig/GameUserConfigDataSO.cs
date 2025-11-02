using UnityEngine;

namespace Game.Core.GameUserConfig
{
    [CreateAssetMenu(menuName = "Assets/User Config")]
    public sealed class GameUserConfigDataSO : ScriptableObject
    {
        public float MouseSensitivity 
        {
            get => _mouseSensitivity;
            set
            {
                if (_mouseSensitivity == value)
                    return;
                _mouseSensitivity = Mathf.Clamp(value, 0.1f, 3.0f);
            }
        }
        [SerializeField] private float _mouseSensitivity;
    }
}
