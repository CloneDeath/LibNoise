namespace LibNoise
{
    /// <summary>
    ///     Base class for all filter module
    ///     Provides some commons or usefull properties and constants
    /// </summary>
    public abstract class FilterModule : IModule
	{
		protected SpectralWeights SpectralWeights { get; } = new SpectralWeights();

	    /// <summary>
	    ///     Default frequency for the noise module.
	    /// </summary>
	    public const float DefaultFrequency = 1.0f;

	    /// <summary>
	    ///     Default lacunarity for the noise module.
	    /// </summary>
	    public const float DefaultLacunarity = 2.0f;

	    /// <summary>
	    ///     Default number of octaves for the noise module.
	    /// </summary>
	    public const float DefaultOctaveCount = 6.0f;

	    /// <summary>
	    ///     Default offset
	    /// </summary>
	    public const float DefaultOffset = 1.0f;

	    /// <summary>
	    ///     Default gain
	    /// </summary>
	    public const float DefaultGain = 2.0f;

	    /// <summary>
	    ///     Default spectral exponent
	    /// </summary>
	    public const float DefaultSpectralExponent = 0.9f;

		/// <summary>
		///     Frequency of the first octave
		/// </summary>
		public float Frequency { get; set; }


		/// <summary>
		///     The lacunarity specifies the frequency multipler between successive
		///     octaves.
		///     The effect of modifying the lacunarity is subtle; you may need to play
		///     with the lacunarity value to determine the effects.  For best results,
		///     set the lacunarity to a number between 1.5 and 3.5.
		/// </summary>
	    public float Lacunarity
	    {
		    get => SpectralWeights.Lucinarity;
			set => SpectralWeights.Lucinarity = value;
		}

		/// <summary>
		///     The number of octaves control the <i>amount of detail</i> of the
		///     noise.  Adding more octaves increases the detail of the
		///     noise, but with the drawback of increasing the calculation time.
		///     An octave is one of the coherent-noise functions in a series of
		///     coherent-noise functions that are added together to form noise.
		/// </summary>
	    public float OctaveCount { get; set; }

		/// <summary>
		///     Gets or sets the offset
		/// </summary>
		public float Offset { get; set; } = DefaultOffset;


		/// <summary>
		///     Gets or sets the gain
		/// </summary>
		public float Gain { get; set; } = DefaultGain;


		/// <summary>
		///     Gets or sets the spectralExponent
		/// </summary>
		public float SpectralExponent
		{
			get => SpectralWeights.Exponent;
			set => SpectralWeights.Exponent = value;
		}


	    /// <summary>
	    ///     Gets or sets the primitive 4D
	    /// </summary>
	    public IModule4D Primitive4D { get; set; }


	    /// <summary>
	    ///     Gets or sets the primitive 3D
	    /// </summary>
	    public IModule3D Primitive3D { get; set; }


	    /// <summary>
	    ///     Gets or sets the primitive 2D
	    /// </summary>
	    public IModule2D Primitive2D { get; set; }


	    /// <summary>
	    ///     Gets or sets the primitive 1D
	    /// </summary>
	    public IModule1D Primitive1D { get; set; }

	    /// <summary>
	    ///     Template default constructor
	    /// </summary>
	    protected FilterModule()
			: this(DefaultFrequency, DefaultLacunarity, DefaultSpectralExponent, DefaultOctaveCount)
		{
		}


	    /// <summary>
	    ///     Template constructor
	    /// </summary>
	    /// <param name="frequency"></param>
	    /// <param name="lacunarity"></param>
	    /// <param name="exponent"></param>
	    /// <param name="octaveCount"></param>
	    protected FilterModule(float frequency, float lacunarity, float exponent, float octaveCount)
		{
			Frequency = frequency;
			Lacunarity = lacunarity;
			SpectralExponent = exponent;
			OctaveCount = octaveCount;
		}
	}
}