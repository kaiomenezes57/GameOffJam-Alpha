using DG.Tweening;
using UnityEngine;
using System;

namespace Game.Domains.Smartphone
{
    public sealed class PhonePicker : MonoBehaviour
    {
        [SerializeField] private Transform _pickedTransform;
        [SerializeField] private Transform _putDownTransform;
        [SerializeField] private GameObject _phoneObj;
        
        public bool IsPhonePicked => _phoneObj.activeSelf;
        private const float ANIMATION_DURATION = 0.3f;

        private void Start()
        {
            _phoneObj.transform.position = _putDownTransform.position;
            _phoneObj.SetActive(false);
        }

        public void PickUpPhone()
        {
            _phoneObj.SetActive(true);
            _phoneObj.transform
                .DOMove(_pickedTransform.position, ANIMATION_DURATION)
                .SetLink(_phoneObj);
        }

        public void PutDownPhone(Action onComplete)
        {
            _phoneObj.transform
                .DOMove(_putDownTransform.position, ANIMATION_DURATION)
                .OnComplete(() => {
                    _phoneObj.SetActive(false);
                    onComplete?.Invoke();
                })
                .SetLink(_phoneObj);
        }
    }
}
