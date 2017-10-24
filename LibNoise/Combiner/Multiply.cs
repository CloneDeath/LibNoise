namespace LibNoise.Combiner
{
    /// <summary>
    /// Noise module that outputs the product of the two output values from
    /// two source modules.
    /// </summary>
    public class Multiply : CombinerModule, IModule3D
    {
        #region Ctor/Dtor

        public Multiply()
        {
        }


        public Multiply(IModule left, IModule right)
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
            return ((IModule3D) _leftModule).GetValue(x, y, z)*((IModule3D) _rightModule).GetValue(x, y, z);
        }

        #endregion
    }
}
