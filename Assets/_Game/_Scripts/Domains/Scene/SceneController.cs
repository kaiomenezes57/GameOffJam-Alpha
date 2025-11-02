using Cysharp.Threading.Tasks;
using Game.Core.FadeTransition;
using Game.Core.Scene;
using Game.Core.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.Domains.Scene
{
    public sealed class SceneController : ISceneController
    {
        [Inject] private readonly IFadeTransition _fadeTransition;

        public async void LoadScene(SceneDataSO sceneData)
        {
            if (sceneData == null)
            {
#if DEBUG
                Debug.LogError($"[SCENE CONTROLLER] Scene data is null.");
#endif
                return;
            }

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene == null || scene.buildIndex == 0)
                    continue;
                await SceneManager.UnloadSceneAsync(scene);
            }

            _fadeTransition.Perform(blackScreenDuration: 2f, 
                onFadeOutCompleted: async () =>
                {
                    await SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Additive);
                    for (var i = 0; i < SceneManager.sceneCount; i++)
                    {
                        var scene = SceneManager.GetSceneAt(i);
                        if (scene == null || !scene.name.Equals(sceneData.SceneName))
                            continue;
                        SceneManager.SetActiveScene(scene);
                    }
                });
        }
    }
}
