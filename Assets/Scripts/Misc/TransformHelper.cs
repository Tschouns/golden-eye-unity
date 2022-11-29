
using UnityEngine;

namespace Assets.Scripts.Misc
{
    /// <summary>
    /// Helps transforming transforms in specific ways.
    /// </summary>
    public static class TransformHelper
    {
        /// <summary>
        /// Randomly rotates the specified vector by a random amount, no more than the maximum specified.
        /// </summary>
        /// <param name="direction">
        /// The original vector
        /// </param>
        /// <param name="maxRadians">
        /// The maximum radians by which to rotate the vector
        /// </param>
        /// <returns>
        /// A randomly rotated vector
        /// </returns>
        public static Vector3 RandomRotate(Vector3 direction, float maxRadians)
        {
            var randomVector = new Vector3(Random.value, Random.value, Random.value).normalized;
            var radians = Random.Range(0f, Mathf.Abs(maxRadians));

            var rotatedVector = Vector3.RotateTowards(direction, randomVector, radians, 0f);

            return rotatedVector;
        }

        /// <summary>
        /// Rotates a specified original direction vector towards a specified target direction vector, at the specified angular speed (in degrees).
        /// </summary>
        /// <param name="originalDirection">
        /// The original direction
        /// </param>
        /// <param name="targetDirection">
        /// The target direction
        /// </param>
        /// <param name="angularSpeedDeg">
        /// The angular speed in degrees
        /// </param>
        /// <returns>
        /// The updated direction vector
        /// </returns>
        public static Vector3 RotateTowardsAtSpeed(Vector3 originalDirection, Vector3 targetDirection, float angularSpeedDeg)
        {
            var maxRadiansDelta = angularSpeedDeg * Mathf.Deg2Rad * Time.deltaTime;
            var updatedDirection = Vector3.RotateTowards(originalDirection, targetDirection, maxRadiansDelta, maxMagnitudeDelta: float.MaxValue);

            return updatedDirection;
        }
    }
}