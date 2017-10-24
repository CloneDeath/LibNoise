﻿namespace LibNoise.Builder
{
    using LibNoise.Utils;

    /// <summary>
    /// Implements a noise map, a 2-dimensional array of floating-point values.
    /// A noise map is designed to store coherent-noise values generated by a
    /// noise module, although it can store values from any source.  A noise
    /// map is often used as a terrain height map or a grayscale texture. 
    /// </summary>
    public class NoiseMap : DataMap<float>, IMap2D<float>
    {
        #region Ctor/Dtor

        /// <summary>
        /// Create an empty NoiseMap.
        /// </summary>
        public NoiseMap()
        {
            HasMaxDimension = false;

            BorderValue = 0.0f;
            AllocateBuffer();
        }

        /// <summary>
        /// Create a new NoiseMap with the given values
        /// The width and height values are positive.
        /// The width and height values do not exceed the maximum
        /// possible width and height for the noise map.
        /// @throw System.ArgumentException See the preconditions.
        /// @throw noise::ExceptionOutOfMemory Out of memory.
        /// Creates a noise map with uninitialized values.
        /// It is considered an error if the specified dimensions are not
        /// positive.
        /// </summary>
        /// <param name="width">The width of the new noise map.</param>
        /// <param name="height">The height of the new noise map.</param>
        public NoiseMap(int width, int height)
        {
            HasMaxDimension = false;
            BorderValue = 0.0f;
            AllocateBuffer(width, height);
        }

        /// <summary>
        /// Copy constructor
        /// @throw noise::ExceptionOutOfMemory Out of memory.
        /// </summary>
        /// <param name="copy">The NoiseMap to copy.</param>
        public NoiseMap(NoiseMap copy)
        {
            HasMaxDimension = false;
            BorderValue = 0.0f;
            CopyFrom(copy);
        }

        #endregion

        #region Interaction

        /// <summary>
        /// Find the lowest and highest value in the map
        /// </summary>
        /// <param name="min">the lowest value</param>
        /// <param name="max">the highest value</param>
        public void MinMax(out float min, out float max)
        {
            min = max = 0f;
            float[] data = Data;
            if (data != null && data.Length > 0)
            {
                // First value, min and max for now
                min = max = data[0];

                for (int i = 1; i < data.Length; i++)
                {
                    if (min > data[i])
                        min = data[i];
                    else if (max < data[i])
                        max = data[i];
                }
            }
        }

        #endregion

        #region Internal

        /// <summary>
        /// Return the memory size of a float.
        /// </summary>
        /// <returns>The memory size of a float.</returns>
        protected override int SizeofT()
        {
            return 32;
        }

        /// <summary>
        /// Return the maximum value of a float type.
        /// </summary>
        /// <returns>Maximum value.</returns>
        protected override float MaxvalofT()
        {
            return float.MaxValue;
        }

        /// <summary>
        /// Return the minimum value of a float type.
        /// </summary>
        /// <returns>Mimum value.</returns>
        protected override float MinvalofT()
        {
            return float.MinValue;
        }

        #endregion
    }
}
