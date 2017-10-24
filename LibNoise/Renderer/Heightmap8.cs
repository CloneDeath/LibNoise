using LibNoise.Utils;

namespace LibNoise.Renderer
{
    /// <summary>
    ///     Implements a 8 bits Heightmap, a 2-dimensional array of unsigned byte values (0 to 255).
    /// </summary>
    public class Heightmap8 : DataMap<byte>, IMap2D<byte>
	{
		/// <summary>
	    ///     Find the lowest and highest value in the map.
	    /// </summary>
	    /// <param name="min">The lowest value.</param>
	    /// <param name="max">The highest value.</param>
	    public void MinMax(out byte min, out byte max)
		{
			min = max = 0;
			var data = Data;

			if (data != null && data.Length > 0)
			{
				// First value, min and max for now
				min = max = data[0];

				for (var i = 0; i < data.Length; i++)
					if (min > data[i])
						min = data[i];
					else if (max < data[i])
						max = data[i];
			}
		}

		/// <summary>
	    ///     0-args constructor.
	    /// </summary>
	    public Heightmap8()
		{
			BorderValue = byte.MinValue;
			AllocateBuffer();
		}

	    /// <summary>
	    ///     Create a new Heightmap8 with the given values
	    ///     The width and height values must be positive.
	    /// </summary>
	    /// <param name="width">The width of the new noise map.</param>
	    /// <param name="height">The height of the new noise map</param>
	    public Heightmap8(int width, int height)
		{
			BorderValue = byte.MinValue;
			AllocateBuffer(width, height);
		}

	    /// <summary>
	    ///     Copy constructor.
	    /// </summary>
	    /// <param name="copy">The heightmap to copy.</param>
	    public Heightmap8(Heightmap8 copy)
		{
			BorderValue = byte.MinValue;
			CopyFrom(copy);
		}

		/// <summary>
	    ///     Return the memory size of a unsigned byte.
	    /// </summary>
	    /// <returns>The memory size of a unsigned byte.</returns>
	    protected override int SizeofT()
		{
			return 8;
		}

	    /// <summary>
	    ///     Return the maximum value of a unsigned byte type (255).
	    /// </summary>
	    /// <returns>Maximum value.</returns>
	    protected override byte MaxvalofT()
		{
			return byte.MaxValue;
		}

	    /// <summary>
	    ///     Return the minimum value of a unsigned byte type (0).
	    /// </summary>
	    /// <returns>Minimum value.</returns>
	    protected override byte MinvalofT()
		{
			return byte.MinValue;
		}
	}
}