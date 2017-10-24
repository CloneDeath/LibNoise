namespace LibNoise.Combiner
{
    using System;

    /// <summary>
    /// Noise module that raises the output value from the left source module
    /// to the power of the output value from the right source module.
    /// </summary>
    public class Power : CombinerModule, IModule3D
    {
        #region Ctor/Dtor

        public Power()
        {
        }


        public Power(IModule left, IModule right)
            : base(left, right)
        {
        }

        #endregion

        #region IModule3D Members

        /// <summary>
        /// Generates an output value given the coordinates of the specified input value.
        /// </summary>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <returns>The resulting output value.</returns>
        public float GetValue(float x, float y, float z)
        {
            return
                (float)
                    Math.Pow(((IModule3D) _leftModule).GetValue(x, y, z), ((IModule3D) _rightModule).GetValue(x, y, z));
        }

        #endregion
    }
}
