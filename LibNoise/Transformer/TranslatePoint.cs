namespace LibNoise.Transformer
{
    /// <summary>
    ///     Noise module that moves the coordinates of the input value before
    ///     returning the output value from a source module.
    ///     The GetValue() method moves the ( x, y, z ) coordinates of
    ///     the input value by a translation amount before returning the output
    ///     value from the source module.
    /// </summary>
    public class TranslatePoint : TransformerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			return ((IModule3D) _sourceModule).GetValue(x + _xTranslate, y + _yTranslate, z + _zTranslate);
		}

		/// <summary>
	    ///     The default translation amount to apply to the x coordinate
	    /// </summary>
	    public const float DEFAULT_TRANSLATE_X = 1.0f;

	    /// <summary>
	    ///     The default translation amount to apply to the y coordinate
	    /// </summary>
	    public const float DEFAULT_TRANSLATE_Y = 1.0f;

	    /// <summary>
	    ///     The default translation amount to apply to the z coordinate
	    /// </summary>
	    public const float DEFAULT_TRANSLATE_Z = 1.0f;

		/// <summary>
	    ///     The source input module
	    /// </summary>
	    protected IModule _sourceModule;

	    /// <summary>
	    ///     the translation amount to apply to the x coordinate
	    /// </summary>
	    protected float _xTranslate = DEFAULT_TRANSLATE_X;

	    /// <summary>
	    ///     the translation amount to apply to the y coordinate
	    /// </summary>
	    protected float _yTranslate = DEFAULT_TRANSLATE_Y;

	    /// <summary>
	    ///     the translation amount to apply to the z coordinate
	    /// </summary>
	    protected float _zTranslate = DEFAULT_TRANSLATE_Z;

		/// <summary>
	    ///     Gets or sets the source module
	    /// </summary>
	    public IModule SourceModule
		{
			get => _sourceModule;
			set => _sourceModule = value;
		}

	    /// <summary>
	    ///     Gets or sets the translation amount to apply to the x coordinate
	    /// </summary>
	    public float XTranslate
		{
			get => _xTranslate;
			set => _xTranslate = value;
		}

	    /// <summary>
	    ///     Gets or sets the translation amount to apply to the y coordinate
	    /// </summary>
	    public float YTranslate
		{
			get => _yTranslate;
			set => _yTranslate = value;
		}

	    /// <summary>
	    ///     Gets or sets the translation amount to apply to the z coordinate
	    /// </summary>
	    public float ZTranslate
		{
			get => _zTranslate;
			set => _zTranslate = value;
		}

		/// <summary>
	    ///     Create a new noise module with default values
	    /// </summary>
	    public TranslatePoint()
		{
		}


	    /// <summary>
	    ///     Create a new noise module with given values
	    /// </summary>
	    /// <param name="source">the source module</param>
	    public TranslatePoint(IModule source)
		{
			_sourceModule = source;
		}


	    /// <summary>
	    ///     Create a new noise module with given values
	    /// </summary>
	    /// <param name="source">the source module</param>
	    /// <param name="x">the translation amount to apply to the x coordinate</param>
	    /// <param name="y">the translation amount to apply to the y coordinate</param>
	    /// <param name="z">the translation amount to apply to the z coordinate</param>
	    public TranslatePoint(IModule source, float x, float y, float z)
			: this(source)
		{
			_xTranslate = x;
			_yTranslate = y;
			_zTranslate = z;
		}
	}
}