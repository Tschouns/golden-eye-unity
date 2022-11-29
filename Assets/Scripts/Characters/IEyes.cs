
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Provides access to a character's eyes.
    /// </summary>
    public interface IEyes
    {
        /// <summary>
        /// Sets a specific point for the eyes to focus on.
        /// </summary>
        /// <param name="focusPoint">
        /// The focus point
        /// </param>
        void SetEyesFocus(Vector3 focusPoint);

        /// <summary>
        /// Unsets the focus point. Allows the eyes to focus on nothing in particular.
        /// </summary>
        void UnsetEyesFocus();
    }
}
