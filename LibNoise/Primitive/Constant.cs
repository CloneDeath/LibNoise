namespace LibNoise.Primitive
{
    /// <summary>
    ///     Noise module that outputs a constant value.
    ///     This noise module is not useful by itself, but it is often used as a
    ///     source module for other noise modules.
    /// </summary>
    public class Constant : PrimitiveModule, IModule4D, IModule3D, IModule2D, IModule1D
	{
		#region Constants

	    /// <summary>
	    /// </summary>
	    public const float DEFAULT_VALUE = 0.5f;

		#endregion

		#region Fields

	    /// <summary>
	    /// </summary>
	    protected float _constant = DEFAULT_VALUE;

		#endregion

		#region Accessors

	    /// <summary>
	    ///     the constant output value for this noise module.
	    /// </summary>
	    public float ConstantValue
		{
			get => _constant;
			set => _constant = value;
		}

		#endregion

		#region IModule1D Members

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x)
		{
			return _constant;
		}

		#endregion

		#region IModule2D Members

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y)
		{
			return _constant;
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
			return _constant;
		}

		#endregion

		#region IModule4D Members

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <param name="z">The input coordinate on the z-axis.</param>
	    /// <param name="t">The input coordinate on the t-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y, float z, float t)
		{
			return _constant;
		}

		#endregion

		#region Ctor/Dtor

	    /// <summary>
	    ///     Create a new noiws module with DEFAULT_VALUE
	    /// </summary>
	    public Constant()
			: this(DEFAULT_VALUE)
		{
		}


	    /// <summary>
	    ///     Create a new noise module width given value
	    /// </summary>
	    /// <param name="value">The value to use</param>
	    public Constant(float value)
		{
			_constant = value;
		}

		#endregion
	}
}