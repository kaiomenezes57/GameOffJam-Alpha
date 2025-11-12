using DG.Tweening;
using UnityEngine;

namespace Game.Views
{
    public sealed class SwitchLightSwitcher : MonoBehaviour
    {
        public void Switch()
        {
            var xRotation = transform.rotation.eulerAngles.x;
            var newXRotation = xRotation > 0f ? 0f : 45f;
            transform.DORotate(new Vector3(newXRotation, 0f, 0f), 0.2f)
                .SetLink(gameObject);
        }
    }
}
