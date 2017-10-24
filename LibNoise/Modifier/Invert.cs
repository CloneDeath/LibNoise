namespace LibNoise.Modifier
{
    /// <summary>
    /// Noise module that inverts the output value from a source module.
    /// </summary>
    public class Invert : ModifierModule, IModule3D
    {
        #region Ctor/Dtor

        public Invert()
        {
        }


        public Invert(IModule source)
            : base(source)
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
            return -(((IModule3D) _sourceModule).GetValue(x, y, z));
        }

        #endregion
    }
}
