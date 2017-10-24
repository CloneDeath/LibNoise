namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that applies a scaling factor and a bias to the output
    ///     value from a source module.
    ///     The GetValue() method retrieves the output value from the source
    ///     module, multiplies it with a scaling factor, adds a bias to it, then
    ///     outputs the value.
    /// </summary>
    public class ScaleBias : IModule3D
	{
		protected IModule3D Source { get; set; }
		
	    public float GetValue(float x, float y, float z)
		{
			return Source.GetValue(x, y, z) * Scale + Bias;
		}

		/// <summary>
		///     the scaling factor to apply to the output value from the source module.
		/// </summary>
		public float Scale { get; set; }

		/// <summary>
		///     the bias to apply to the scaled output value from the source module.
		/// </summary>
		public float Bias { get; set; }

		public ScaleBias(IModule3D source, float scale, float bias)
		{
			Source = source;
			Scale = scale;
			Bias = bias;
		}
	}
}