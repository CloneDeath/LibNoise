namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that clamps the output value from a source module to a
    ///     range of values.
    ///     The range of values in which to clamp the output value is called the
    ///     <i>clamping range</i>.
    ///     If the output value from the source module is less than the lower
    ///     bound of the clamping range, this noise module clamps that value to
    ///     the lower bound.  If the output value from the source module is
    ///     greater than the upper bound of the clamping range, this noise module
    ///     clamps that value to the upper bound.
    /// </summary>
    public class Clamp : ModifierModule, IModule3D
	{
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

			if (value < _lowerBound)
				return _lowerBound;
			if (value > _upperBound)
				return _upperBound;
			return value;
		}

		#endregion

		#region Connstant

	    /// <summary>
	    ///     Default lower bound of the clamping range
	    ///     noise module.
	    /// </summary>
	    public const float DEFAULT_LOWER_BOUND = -1.0f;

	    /// <summary>
	    ///     Default upper bound of the clamping range
	    ///     noise module.
	    /// </summary>
	    public const float DEFAULT_UPPER_BOUND = 1.0f;

		#endregion

		#region Fields

	    /// <summary>
	    /// </summary>
	    protected float _lowerBound = DEFAULT_LOWER_BOUND;

	    /// <summary>
	    /// </summary>
	    protected float _upperBound = DEFAULT_UPPER_BOUND;

		#endregion

		#region Accessors

	    /// <summary>
	    /// </summary>
	    public float LowerBound
		{
			get => _lowerBound;
			set => _lowerBound = value;
		}

	    /// <summary>
	    /// </summary>
	    public float UpperBound
		{
			get => _upperBound;
			set => _upperBound = value;
		}

		#endregion

		#region Ctor/Dtor

		public Clamp()
		{
		}


		public Clamp(IModule source)
			: base(source)
		{
		}


		public Clamp(IModule source, float lower, float upper)
			: base(source)
		{
			_lowerBound = lower;
			_upperBound = upper;
		}

		#endregion
	}
}