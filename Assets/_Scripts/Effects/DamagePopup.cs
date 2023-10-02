using System.Collections;
using TMPro;
using UnityEngine;

namespace SimpleSurvivors.Effects
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private bool enableMoveUp;
        [SerializeField] private float moveUpSpeed;
        [SerializeField] private float moveUpDelay;
        [SerializeField] private float moveUpDuration;

        [SerializeField] private bool enableExpand;
        [SerializeField] private float expandSpeed;
        [SerializeField] private float expandDelay;
        [SerializeField] private float expandDuration;

        [SerializeField] private bool enableShrink;
        [SerializeField] private float shrinkSpeed;
        [SerializeField] private float shrinkDelay;
        [SerializeField] private float shrinkDuration;

        [SerializeField] private bool enableFadeOut;
        [SerializeField] private float fadeOutSpeed;
        [SerializeField] private float fadeOutDelay;
        [SerializeField] private float fadeOutDuration;

        [SerializeField] private bool enableDestroy;
        [SerializeField] private float destroyDuration;
        
        private float _remainingMoveUpDuration;
        private float _remainingExpandDuration;
        private float _remainingShrinkDuration;
        private float _remainingFadeOutDuration;
        private TMP_Text _tmpText;

        void Awake()
        {
            _tmpText = GetComponentInChildren<TMP_Text>();
        }

        void Start()
        {
            _remainingMoveUpDuration = moveUpDuration;
            _remainingExpandDuration = expandDuration;
            
            if (enableMoveUp)
            {
                StartCoroutine(MoveUp());
            }

            if (enableExpand)
            {
                StartCoroutine(Expand());   
            }

            if (enableDestroy)
            {
                StartCoroutine(DestroyDelayed());
            }
        }

        private IEnumerator MoveUp()
        {
            while (_remainingMoveUpDuration > 0f)
            {
                transform.position = new Vector3(transform.position.x,
                    transform.position.y + moveUpSpeed, transform.position.z);
                _remainingMoveUpDuration -= Time.deltaTime;

                yield return new WaitForSeconds(moveUpDelay);
            }
        }
        
        private IEnumerator Expand()
        {
            while (_remainingExpandDuration > 0f)
            {
                transform.localScale = new Vector3(transform.localScale.x + expandSpeed,
                    transform.localScale.y + expandSpeed, transform.localScale.z);
                _remainingExpandDuration -= Time.deltaTime;

                yield return new WaitForSeconds(expandDelay);
            }
            
            if (enableShrink)
            {
                _remainingShrinkDuration = shrinkDuration;
                StartCoroutine(Shrink());    
            }
            
            if (enableFadeOut)
            {
                _remainingFadeOutDuration = fadeOutDuration;
                StartCoroutine(FadeOut());    
            }            
        }
        
        private IEnumerator Shrink()
        {
            while (_remainingShrinkDuration > 0f)
            {
                transform.localScale = new Vector3(transform.localScale.x - shrinkSpeed,
                    transform.localScale.y - shrinkSpeed, transform.localScale.z);
                _remainingShrinkDuration -= Time.deltaTime;

                yield return new WaitForSeconds(shrinkDelay);
            }
        }

        private IEnumerator FadeOut()
        {
            while (_remainingFadeOutDuration > 0f)
            {
                _tmpText.color = new Color(_tmpText.color.r, _tmpText.color.g, _tmpText.color.b,
                    _tmpText.color.a - fadeOutSpeed);
                _remainingFadeOutDuration -= Time.deltaTime;

                yield return new WaitForSeconds(fadeOutDelay);
            }
        }

        private IEnumerator DestroyDelayed()
        {
            yield return new WaitForSeconds(destroyDuration);
            
            Destroy(gameObject);
        }
    }
}
