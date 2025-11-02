using UnityEngine;

namespace Game.Core.Scene.Data
{
    [CreateAssetMenu(menuName = "Assets/Scenes/Scene")]
    public sealed class SceneDataSO : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; private set; }
    }
}