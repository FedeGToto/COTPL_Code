using UnityEngine;
using UnityEngine.Events;

namespace StatSystem {
    [System.Serializable]
    public class Resource
    {
        private readonly Stat maxValue;
        private float value;

        public UnityAction<float> OnValueChanged;

        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                this.value = Mathf.Clamp(this.value, 0f, maxValue.Value);
                OnValueChanged?.Invoke(this.value);
            }
        }

        public float MaxValue => maxValue.Value;

        public Resource(Stat maxValue)
        {
            this.maxValue = maxValue;
            value = maxValue.Value;
        }

        public Resource(float maxValue)
        {
            this.maxValue = new Stat(maxValue);
            value = this.maxValue.Value;
        }

        public Stat GetStat() => maxValue;

    }
}
