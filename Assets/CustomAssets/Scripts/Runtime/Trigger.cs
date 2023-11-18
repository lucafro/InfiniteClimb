using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace LF.Scripts.Runtime.Audio
{
    /// <summary>
    /// Checks if the player has entered and exited a trigger collider and throws respective events.
    /// The events can be used to trigger certain events such as audio playing, animations, interactions etc.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class Trigger : MonoBehaviour
    {
        private Collider _collider;

        [SerializeField, Tooltip("Enable cooldown, to only invoke events after the cooldown time has passed.")]
        private bool _useCooldown;

        [SerializeField]
        private int _cooldownTime;

        //private bool _inCooldown = false;

        public UnityEvent OnPlayerEnteredTrigger = new UnityEvent();
        public UnityEvent OnPlayerExitedTrigger = new UnityEvent();

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            if (_collider == null) return;
            _collider.isTrigger = true;

            //if (_useCooldown == true && _cooldownTime == 0)
            //    _cooldownTime = Random.Range(1, 20);
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (other.gameObject.GetComponent<PlayerController>() != null)
            //{
            //    if (!_inCooldown)
            //    {
            //        OnPlayerEnteredTrigger.Invoke();
            //        //Debug.Log("PLAYER ENTERED TRIGGER with cooldown time of " + _cooldownTime);
            //    }
            //    else
            //    {
            //        //Debug.Log("ENTERED, BUT CURRENTLY IN COOLDOWN");
            //    }

            //    if (_useCooldown) StartCoroutine(Cooldown());
            //}
        }

        private void OnTriggerExit(Collider other)
        {
            //if (other.gameObject.GetComponent<PlayerController>() != null)
            //{
            //    if (!_inCooldown)
            //    {
            //        OnPlayerExitedTrigger.Invoke();
            //        //Debug.Log("PLAYER EXITED TRIGGER with cooldown time of " + _cooldownTime);
            //    }
            //    else
            //    {
            //        //Debug.Log("EXITED, CURRENTLY IN COOLDOWN");
            //    }

            //    if (_useCooldown) StartCoroutine(Cooldown());
            //}
        }

        private IEnumerator Cooldown()
        {
            //_inCooldown = true;
            yield return new WaitForSeconds(_cooldownTime);
            //_inCooldown = false;
        }
    }
}
