using DG.Tweening;
using UnityEngine;

namespace Game.Views
{
    public sealed class SwitchLightSwitcher : MonoBehaviour
    {
        public void Switch()
        {
            var xRotation = transform.localEulerAngles.x;
            var newXRotation = xRotation > 0f ? 0f : 45f;
            transform.DOLocalRotate(new Vector3(newXRotation, 0f, 0f), 0.1f)
                .SetLink(gameObject);
        }
    }
}
