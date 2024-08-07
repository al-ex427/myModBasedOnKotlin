
using Pixelplacement;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

namespace KOTLIN.Events
{
    public class EventManager : Singleton<EventManager>
    {
        private List<RandomEvent> Events = new();
        [SerializeField] private float Countdown;
        public bool DoCountdown;
        [SerializeField] private AudioClip bell;
        private AudioSource audSource;

        [SerializeField] private GameObject EventUI;
        [SerializeField] private TextMeshProUGUI EventText;

        private GameControllerScript gc;

        private RandomEvent CurrEvent;
        private void Start()
        {
            gc = GameControllerScript.Instance; 
            audSource = GetComponent<AudioSource>();

            if (audSource == null)
                audSource = gameObject.AddComponent<AudioSource>();

            FindEvents();

            if (gc != null && Events.Count != 0 && EventUI != null && EventText != null)
                StartCoroutine(MyIenumerator());
        }

        private IEnumerator MyIenumerator()
        {
            yield return new WaitForSeconds(1);
            if (DoCountdown)
            {
                Countdown--;
                if (Countdown == 0)
                {
                    if (gc.spoopMode)
                    {
                        DoEvent();
                    }
                    yield return null;
                }
            }

            StartCoroutine(MyIenumerator());
            yield return null;
        }

        private void DoEvent()
        {
            DoCountdown = false;
            CurrEvent = Events[Random.Range(0, Events.Count)];

            if (bell != null)
                audSource.PlayOneShot(bell);

            EventText.text = CurrEvent.EVTDescription;
            EventUI.SetActive(true);
            CurrEvent.StartEvent();
        }

        public void EndEvent()
        {
            CurrEvent = null;
            DoCountdown = true;
            EventText.text = CurrEvent.EVTDescription;
            EventUI.SetActive(true);
        }

        private void FindEvents()
        {
            foreach (RandomEvent evt in GetComponentsInChildren<RandomEvent>())
            {
                Events.Add(evt);
            }
        }
    }
}