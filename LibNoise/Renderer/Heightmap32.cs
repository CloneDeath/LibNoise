using LibNoise.Builder;

namespace LibNoise.Renderer
{
    /// <summary>
    ///     Implements a 32 bits Heightmap, a 2-dimensional array of float values (+-1.5 x 10^−45 to +-3.4 x 10^38)
    /// </summary>
    public class Heightmap32 : NoiseMap
	{
		#region Ctor/Dtor

	    /// <summary>
	    ///     0-args constructor
	    /// </summary>
	    public Heightmap32()
		{
		}


	    /// <summary>
	    ///     Create a new Heightmap32 with the given values
	    ///     The width and height values must be positive.
	    /// </summary>
	    /// <param name="width">The width of the new noise map.</param>
	    /// <param name="height">The height of the new noise map</param>
	    public Heightmap32(int width, int height)
			: base(width, height)
		{
		}


	    /// <summary>
	    ///     Copy constructor
	    /// </summary>
	    /// <param name="copy">The heightmap to copy</param>
	    public Heightmap32(Heightmap32 copy)
			: base(copy)
		{
		}

		#endregion
	}
}