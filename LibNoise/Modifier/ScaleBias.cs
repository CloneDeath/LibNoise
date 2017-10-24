namespace LibNoise.Modifier
{
    /// <summary>
    /// Noise module that applies a scaling factor and a bias to the output
    /// value from a source module.
    ///
    /// The GetValue() method retrieves the output value from the source
    /// module, multiplies it with a scaling factor, adds a bias to it, then
    /// outputs the value.
    ///
    /// </summary>
    public class ScaleBias : ModifierModule, IModule3D
    {
        #region Constants

        /// <summary>
        /// Default scale
        /// noise module.
        /// </summary>
        public const float DEFAULT_SCALE = 1.0f;

        /// <summary>
        /// Default bias
        /// noise module.
        /// </summary>
        public const float DEFAULT_BIAS = 0.0f;

        #endregion

        #region Fields

        /// <summary>
        /// the bias to apply to the scaled output value from the source module.
        /// </summary>
        protected float _bias = DEFAULT_BIAS;

        /// <summary>
        /// the scaling factor to apply to the output value from the source module.
        /// </summary>
        protected float _scale = DEFAULT_SCALE;

        #endregion

        #region Accessors

        /// <summary>
        /// gets or sets the scale value
        /// </summary>
        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        /// <summary>
        /// gets or sets the bias value
        /// </summary>
        public float Bias
        {
            get { return _bias; }
            set { _bias = value; }
        }

        #endregion

        #region Ctor/Dtor

        public ScaleBias()
        {
        }


        public ScaleBias(IModule source)
            : base(source)
        {
        }


        public ScaleBias(IModule source, float scale, float bias)
            : base(source)
        {
            _scale = scale;
            _bias = bias;
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
            return ((IModule3D) _sourceModule).GetValue(x, y, z)*_scale + _bias;
        }

        #endregion
    }
}
