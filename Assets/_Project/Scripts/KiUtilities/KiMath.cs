using System;
using System.Collections.Generic;
using KimicuUtilities.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KimicuUtilities.Math
{
    public static class KiMath
    {
        /// <summary>
        /// Calculates the percentage ratio.
        /// </summary>
        /// <returns>Returns the percentage.</returns>
        public static double CalculatePercentage(double value, double total)
        {
            return value / total * 100;
        }

        /// <summary>
        /// Calculates the percentage ratio.
        /// </summary>
        /// <returns>Returns the percentage.</returns>
        public static float CalculatePercentage(float value, float total)
        {
            return value / total * 100;
        }

        /// <summary>
        /// Takes n elements from the structure of objects with chances.
        /// </summary>
        /// <param name="list">Structure with objects and its chance.</param>
        /// <param name="count">The number of returned T in the list.</param>
        /// <typeparam name="T">Any object.</typeparam>
        /// <returns>Returns a random list T.</returns>
        public static T[] RandomWithChance<T>(this List<ObjectChance<T>> list, int count)
        {
            List<T> result = new List<T>();

            for (int i = 0; i < count; i++)
            {
                float totalChance = default;
                foreach (var objChance in list)
                {
                    totalChance += objChance.Chance;
                }

                foreach (var objChance in list)
                {
                    var objectChance = objChance;
                    objectChance.Chance = CalculatePercentage(objectChance.Chance, totalChance);
                }

                float randomValue = Random.Range(0f, totalChance);
                float cumulativeChance = default;

                foreach (var objChance in list)
                {
                    cumulativeChance += objChance.Chance;
                    if (randomValue >= cumulativeChance) continue;
                    result.Add(objChance.Object);
                    break;
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Calculates the vector between two points in 3D space, optionally ignoring one of the axes.
        /// </summary>
        /// <param name="position1">The first point in 3D space.</param>
        /// <param name="position2">The second point in 3D space.</param>
        /// <param name="ignoreAxis">The axis to ignore when calculating the vector.</param>
        /// <returns>The vector between the two points.</returns>
        public static Vector3 GetVectorBetweenPoints(this Vector3 position1, Vector3 position2,
            IgnoreAxis ignoreAxis = IgnoreAxis.None)
        {
            switch (ignoreAxis)
            {
                case IgnoreAxis.None:
                    return position1 - position2;
                case IgnoreAxis.IgnoreX:
                    position1.x = 0;
                    position2.x = 0;
                    return position1 - position2;
                case IgnoreAxis.IgnoreY:
                    position1.y = 0;
                    position2.y = 0;
                    return position1 - position2;
                case IgnoreAxis.IgnoreZ:
                    position1.z = 0;
                    position2.z = 0;
                    return position1 - position2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ignoreAxis), ignoreAxis, null);
            }
        }

        /// <summary>
        /// Calculates the vector between two points in 3D space, optionally ignoring one of the axes.
        /// </summary>
        /// <typeparam name="T">Any object inherited from Component.</typeparam>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second point.</param>
        /// <param name="ignoreAxis">The axis to ignore when calculating the vector.</param>
        /// <returns>The vector between the two points.</returns>
        /// <remarks>
        /// This method is an extension method for the Component class, which means it can be called on any object that inherits from Component.
        /// </remarks>
        public static Vector3 GetVectorBetweenPoints<T>(this T point1, T point2,
            IgnoreAxis ignoreAxis = IgnoreAxis.None)
            where T : Component
        {
            Vector3 position1 = point1.transform.position;
            Vector3 position2 = point2.transform.position;
            switch (ignoreAxis)
            {
                case IgnoreAxis.None:
                    break;
                case IgnoreAxis.IgnoreX:
                    position1.x = 0;
                    position2.x = 0;
                    break;
                case IgnoreAxis.IgnoreY:
                    position1.y = 0;
                    position2.y = 0;
                    break;
                case IgnoreAxis.IgnoreZ:
                    position1.z = 0;
                    position2.z = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ignoreAxis), ignoreAxis, null);
            }

            return position1 - position2;
        }
    }
}