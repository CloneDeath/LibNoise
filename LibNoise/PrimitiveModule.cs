﻿namespace LibNoise
{
    /// <summary>
    ///     Base class for all noise primitive.
    /// </summary>
    public abstract class PrimitiveModule : IModule
	{
		/// <summary>
	    ///     Default noise seed for the noise module.
	    /// </summary>
	    public const int DefaultSeed = 0;

	    /// <summary>
	    ///     Default noise quality for the noise module.
	    /// </summary>
	    public const NoiseQuality DefaultQuality = NoiseQuality.Standard;

		/// <summary>
	    ///     The quality of the Perlin noise.
	    /// </summary>
	    private NoiseQuality _quality = DefaultQuality;

	    /// <summary>
	    ///     The seed value used by the Perlin-noise function.
	    /// </summary>
	    private int _seed = DefaultSeed;

		/// <summary>
	    ///     Gets or sets the seed of the perlin noise.
	    /// </summary>
	    public virtual int Seed
		{
			get => _seed;
			set => _seed = value;
		}

	    /// <summary>
	    ///     Gets or sets the quality
	    /// </summary>
	    public virtual NoiseQuality Quality
		{
			get => _quality;
			set => _quality = value;
		}

		/// <summary>
	    ///     A 0-args constructor
	    /// </summary>
	    protected PrimitiveModule()
			: this(DefaultSeed, DefaultQuality)
		{
		}

	    /// <summary>
	    ///     A basic connstrucutor
	    /// </summary>
	    /// <param name="seed"></param>
	    protected PrimitiveModule(int seed)
			: this(seed, DefaultQuality)
		{
		}

	    /// <summary>
	    ///     A basic connstrucutor
	    /// </summary>
	    /// <param name="seed"></param>
	    /// <param name="quality"></param>
	    protected PrimitiveModule(int seed, NoiseQuality quality)
		{
			_seed = seed;
			_quality = quality;
		}
	}
}