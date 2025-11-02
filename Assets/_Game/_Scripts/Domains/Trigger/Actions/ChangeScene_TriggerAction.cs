using Game.Core.Scene;
using Game.Core.Scene.Data;
using Game.Core.Trigger;
using UnityEngine;
using VContainer;

namespace Game.Domains.Trigger
{
    public sealed class ChangeScene_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private SceneDataSO _sceneData;
        [Inject] private readonly ISceneController _sceneController;

        protected override void OnTriggered()
        {
            _sceneController.LoadScene(_sceneData);
        }
    }
}
