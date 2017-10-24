using LibNoise.Renderer;

namespace LibNoise.Builder
{
    /// <summary>
    ///     Shape filter.
    /// </summary>
    public class ShapeFilter : IBuilderFilter
	{
		/// <summary>
	    ///     Default value.
	    /// </summary>
	    public const float DefaultValue = -0.5f;

		/// <summary>
	    ///     Get greyscale level.
	    /// </summary>
	    /// <param name="x">X.</param>
	    /// <param name="y">Y.</param>
	    /// <returns>Value.</returns>
	    protected byte GetGreyscaleLevel(int x, int y)
		{
			// Is this position is stored in cache ?
			if (!Cache.IsCached(x, y))
				Cache.Update(x, y, PShape.GetValue(x, y).Red);

			return Cache.Level;
		}

		/// <summary>
	    ///     A simple 2d-coordinates struct used as a cached value
	    /// </summary>
	    protected struct LevelCache
		{
		    /// <summary>
		    ///     Level.
		    /// </summary>
		    public byte Level;

			private int _x;

			private int _y;

		    /// <summary>
		    ///     Default constructor.
		    /// </summary>
		    /// <param name="x">X.</param>
		    /// <param name="y">Y.</param>
		    /// <param name="level">Level.</param>
		    public LevelCache(int x, int y, byte level)
			{
				_x = x;
				_y = y;
				Level = level;
			}


		    /// <summary>
		    ///     IsCached.
		    /// </summary>
		    /// <param name="x">X.</param>
		    /// <param name="y">Y.</param>
		    public bool IsCached(int x, int y)
			{
				return _x == x && _y == y;
			}


		    /// <summary>
		    ///     Update.
		    /// </summary>
		    /// <param name="x">X.</param>
		    /// <param name="y">Y.</param>
		    /// <param name="level">Level.</param>
		    public void Update(int x, int y, byte level)
			{
				_x = x;
				_y = y;
				Level = level;
			}
		}

		/// <summary>
	    /// </summary>
	    protected LevelCache Cache = new LevelCache(-1, -1, 0);

	    /// <summary>
	    /// </summary>
	    protected float Constant = DefaultValue;

	    /// <summary>
	    ///     The shape image
	    /// </summary>
	    protected IMap2D<IColor> PShape;

		/// <summary>
	    ///     Gets or sets the shape image
	    /// </summary>
	    public IMap2D<IColor> Shape
		{
			get => PShape;
			set => PShape = value;
		}

	    /// <summary>
	    ///     the constant output value.
	    /// </summary>
	    public float ConstantValue
		{
			get => Constant;
			set => Constant = value;
		}

		/// <summary>
	    ///     Return the filter level at this position
	    /// </summary>
	    /// <param name="x"></param>
	    /// <param name="y"></param>
	    /// <returns></returns>
	    public FilterLevel IsFiltered(int x, int y)
		{
			var level = GetGreyscaleLevel(x, y);

			if (level == byte.MinValue)
				return FilterLevel.Constant;

			return level == byte.MaxValue ? FilterLevel.Source : FilterLevel.Filter;
		}


	    /// <summary>
	    ///     Filter value.
	    /// </summary>
	    /// <param name="x">X.</param>
	    /// <param name="y">Y.</param>
	    /// <param name="source">Source.</param>
	    /// <returns>Filtered value.</returns>
	    public float FilterValue(int x, int y, float source)
		{
			var level = GetGreyscaleLevel(x, y);

			if (level == byte.MaxValue)
				return source;
			if (level == byte.MinValue)
				return Constant;

			return Libnoise.Lerp(
				Constant,
				source,
				level / 255.0f
			);
		}
	}
}