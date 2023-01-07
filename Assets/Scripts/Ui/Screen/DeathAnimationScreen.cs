using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Screen
{
    /// <summary>
    /// Controlls the death animation screen.
    /// </summary>
    public class DeathAnimationScreen : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] animationSprites;

        [SerializeField]
        private Image imageComponent;

        public float AnimationDurationSeconds { get; set; } = 1.0f;

        private void OnEnable()
        {
            _ = this.StartCoroutine(this.AnimationCoroutine());
        }

        /// <summary>
        /// Plays all the frames of the animation in order in the specified time.
        /// </summary>
        private IEnumerator AnimationCoroutine()
        {
            float frameTime = this.AnimationDurationSeconds / this.animationSprites.Length;
            for (int i = 0; i < this.animationSprites.Length; i++)
            {
                this.imageComponent.sprite = this.animationSprites[i];
                yield return new WaitForSeconds(frameTime);
            }
        }
    }
}
