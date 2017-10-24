namespace LibNoise.Transformer
{
    /// <summary>
    ///     Noise module that scales the coordinates of the input value before
    ///     returning the output value from a source module.
    ///     The GetValue() method multiplies the (x, y, z) coordinates
    ///     of the input value with a scaling factor before returning the output
    ///     value from the source module.
    /// </summary>
    public class ScalePoint : TransformerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			return ((IModule3D) _sourceModule).GetValue(x * _xScale, y * _yScale, z * _zScale);
		}

		/// <summary>
	    ///     The default scaling factor applied to the x coordinate
	    /// </summary>
	    public const float DEFAULT_POINT_X = 1.0f;

	    /// <summary>
	    ///     The default scaling factor applied to the y coordinate
	    /// </summary>
	    public const float DEFAULT_POINT_Y = 1.0f;

	    /// <summary>
	    ///     The default scaling factor applied to the z coordinate
	    /// </summary>
	    public const float DEFAULT_POINT_Z = 1.0f;

		/// <summary>
	    ///     The source input module
	    /// </summary>
	    protected IModule _sourceModule;

	    /// <summary>
	    ///     the scaling factor applied to the x coordinate
	    /// </summary>
	    protected float _xScale = DEFAULT_POINT_X;

	    /// <summary>
	    ///     the scaling factor applied to the y coordinate
	    /// </summary>
	    protected float _yScale = DEFAULT_POINT_Y;

	    /// <summary>
	    ///     the scaling factor applied to the z coordinate
	    /// </summary>
	    protected float _zScale = DEFAULT_POINT_Z;

		/// <summary>
	    ///     Gets or sets the source module
	    /// </summary>
	    public IModule SourceModule
		{
			get => _sourceModule;
			set => _sourceModule = value;
		}

	    /// <summary>
	    ///     Gets or sets the scaling factor applied to the x coordinate
	    /// </summary>
	    public float XScale
		{
			get => _xScale;
			set => _xScale = value;
		}

	    /// <summary>
	    ///     Gets or sets the scaling factor applied to the y coordinate
	    /// </summary>
	    public float YScale
		{
			get => _yScale;
			set => _yScale = value;
		}

	    /// <summary>
	    ///     Gets or sets the scaling factor applied to the z coordinate
	    /// </summary>
	    public float ZScale
		{
			get => _zScale;
			set => _zScale = value;
		}

		/// <summary>
	    ///     Create a new noise module with default values
	    /// </summary>
	    public ScalePoint()
		{
		}


		public ScalePoint(IModule source)
		{
			_sourceModule = source;
		}


	    /// <summary>
	    ///     Create a new noise module with given values
	    /// </summary>
	    /// <param name="source">the source module</param>
	    /// <param name="x">the scaling factor applied to the x coordinate</param>
	    /// <param name="y">the scaling factor applied to the y coordinate</param>
	    /// <param name="z">the scaling factor applied to the z coordinate</param>
	    public ScalePoint(IModule source, float x, float y, float z)
			: this(source)
		{
			_xScale = x;
			_yScale = y;
			_zScale = z;
		}
	}
}