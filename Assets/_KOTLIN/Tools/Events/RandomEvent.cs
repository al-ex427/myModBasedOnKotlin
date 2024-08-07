using KOTLIN.Events;
using UnityEngine;

//inherit from

namespace KOTLIN.Events
{
    public class RandomEvent : MonoBehaviour
    {
        public string EVTName;
        public string EVTDescription;

        public virtual void StartEvent()
        {

        }

        public virtual void EndEvent()
        {
            EventManager.Instance.EndEvent();
        }
    }
}