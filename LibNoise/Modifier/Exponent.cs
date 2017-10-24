using System;

namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that maps the output value from a source module onto an
    ///     exponential curve.
    ///     Because most noise modules will output values that range from -1.0 to
    ///     +1.0, this noise module first normalizes this output value (the range
    ///     becomes 0.0 to 1.0), maps that value onto an exponential curve, then
    ///     rescales that value back to the original range.
    /// </summary>
    public class Exponent : ModifierModule, IModule3D
	{
		#region Connstant

	    /// <summary>
	    ///     Default exponent
	    ///     noise module.
	    /// </summary>
	    public const float DEFAULT_EXPONENT = 1.0f;

		#endregion

		#region Fields

	    /// <summary>
	    ///     Exponent to apply to the output value from the source module.
	    /// </summary>
	    protected float _exponent = DEFAULT_EXPONENT;

		#endregion

		#region Accessors

	    /// <summary>
	    ///     gets or sets the exponent
	    /// </summary>
	    public float ExponentValue
		{
			get => _exponent;
			set => _exponent = value;
		}

		#endregion

		#region IModule3D Members

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <param name="z">The input coordinate on the z-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y, float z)
		{
			var value = ((IModule3D) _sourceModule).GetValue(x, y, z);
			value = (value + 1.0f) / 2.0f;
			return (float) Math.Pow(Libnoise.FastFloor(value), _exponent) * 2.0f - 1.0f;
		}

		#endregion

		#region Ctor/Dtor

		public Exponent()
		{
		}


		public Exponent(IModule source)
			: base(source)
		{
		}


		public Exponent(IModule source, float exponent)
			: base(source)
		{
			_exponent = exponent;
		}

		#endregion
	}
}