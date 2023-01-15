using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Props
{
    /// <summary>
    /// A lamp, which flickers from time to time...
    /// </summary>
    public class Lamp : MonoBehaviour, IOnOff
    {
        private bool isOn;

        [SerializeField]
        private LightFixture lightFixture;

        [SerializeField]
        private float minFlickerInterval = 0.5f;

        [SerializeField]
        private float maxFlickerInterval = 60f;

        [SerializeField]
        private ushort minFlickers = 1;

        [SerializeField]
        private ushort maxFlickers = 5;

        [SerializeField]
        private bool isOnAtStart = true;

        public bool IsOn
        {
            get => this.isOn;
            set
            {
                this.isOn = value;
                this.lightFixture.IsOn = value;

                if (value)
                {
                    this.StartCoroutine(this.FlickerRepeatedly());
                }
            }
        }

        private void Awake()
        {
            Debug.Assert(this.lightFixture != null);
            Debug.Assert(this.minFlickerInterval > 0);
            Debug.Assert(this.maxFlickerInterval > 0);
            Debug.Assert(this.minFlickerInterval < this.maxFlickerInterval);
            Debug.Assert(this.minFlickers < this.maxFlickers);

            this.IsOn = this.isOnAtStart;
            this.lightFixture.IsOn = this.isOnAtStart;

            this.StartCoroutine(this.FlickerRepeatedly());
        }

        private IEnumerator FlickerRepeatedly()
        {
            while (this.IsOn)
            {
                // Wait till next flicker...
                var interval = Random.Range(this.minFlickerInterval, this.maxFlickerInterval);

                yield return new WaitForSeconds(interval);

                // Start flickering.
                this.StartCoroutine(this.FlickerOnce());
            }

            yield return null;
        }

        private IEnumerator FlickerOnce()
        {
            var nFlickers = Random.Range(this.minFlickers, this.maxFlickers);

            for (var i = 0; i < nFlickers; i++)
            {
                this.lightFixture.IsOn = false;
                yield return new WaitForSeconds(0.03f);
                this.lightFixture.IsOn = this.IsOn;
                yield return new WaitForSeconds(0.03f);
            }

            yield return null;
        }
    }
}
