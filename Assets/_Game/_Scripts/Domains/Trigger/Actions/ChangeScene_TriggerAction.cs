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
        private ISceneController _sceneController;

        public override void Inject(IObjectResolver objectResolver)
        {
            _sceneController = objectResolver.Resolve<ISceneController>();
            base.Inject(objectResolver);
        }

        protected override void OnTriggered()
        {
            _sceneController.LoadScene(_sceneData);
        }
    }
}
