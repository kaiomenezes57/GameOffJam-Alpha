using Game.Core.Scene;
using Game.Core.Scene.Data;
using UnityEngine;
using VContainer;

namespace Game.Unity.Buttons
{
    public sealed class StartGame : BaseButtonAction
    {
        [SerializeField] private SceneDataSO _sceneData;
        [Inject] private ISceneController _sceneController;

        public override void Inject(IObjectResolver resolver)
        {
            _sceneController = resolver.Resolve<ISceneController>();
        }

        public override void OnClick()
        {
            _sceneController.LoadScene(_sceneData);
        }
    }
}
