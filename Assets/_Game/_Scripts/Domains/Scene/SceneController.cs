using Game.Core.Scene;
using Game.Core.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Domains.Scene
{
    public sealed class SceneController : ISceneController
    {
        public void LoadScene(SceneDataSO sceneData)
        {
            if (sceneData == null)
            {
#if DEBUG
                Debug.LogError($"[SCENE CONTROLLER] Scene data is null.");
#endif
                return;
            }

            SceneManager.LoadScene(sceneData.SceneName);
        }
    }
}
