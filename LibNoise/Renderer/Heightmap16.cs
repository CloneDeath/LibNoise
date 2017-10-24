using LibNoise.Utils;

namespace LibNoise.Renderer
{
    /// <summary>
    ///     Implements a 16 bits Heightmap, a 2-dimensional array of unsigned short values (0 to 65 535)
    /// </summary>
    public class Heightmap16 : DataMap<ushort>, IMap2D<ushort>
	{
		#region Interaction

	    /// <summary>
	    ///     Find the lowest and highest value in the map
	    /// </summary>
	    /// <param name="min">the lowest value</param>
	    /// <param name="max">the highest value</param>
	    public void MinMax(out ushort min, out ushort max)
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

		#endregion

		#region Ctor/Dtor

	    /// <summary>
	    ///     0-args constructor
	    /// </summary>
	    public Heightmap16()
		{
			BorderValue = ushort.MinValue;
			AllocateBuffer();
		}

	    /// <summary>
	    ///     Create a new Heightmap16 with the given values
	    ///     The width and height values must be positive.
	    /// </summary>
	    /// <param name="width">The width of the new noise map.</param>
	    /// <param name="height">The height of the new noise map</param>
	    public Heightmap16(int width, int height)
		{
			BorderValue = ushort.MinValue;
			AllocateBuffer(width, height);
		}

	    /// <summary>
	    ///     Copy constructor
	    /// </summary>
	    /// <param name="copy">The heightmap to copy</param>
	    public Heightmap16(Heightmap16 copy)
		{
			BorderValue = ushort.MinValue;
			CopyFrom(copy);
		}

		#endregion

		#region Internal

	    /// <summary>
	    ///     Return the memory size of a ushort
	    /// </summary>
	    /// <returns>The memory size of a ushort</returns>
	    protected override int SizeofT()
		{
			return 16;
		}

	    /// <summary>
	    ///     Return the maximum value of a ushort type (65535)
	    /// </summary>
	    /// <returns></returns>
	    protected override ushort MaxvalofT()
		{
			return ushort.MaxValue;
		}

	    /// <summary>
	    ///     Return the minimum value of a ushort type (0)
	    /// </summary>
	    /// <returns></returns>
	    protected override ushort MinvalofT()
		{
			return ushort.MinValue;
		}

		#endregion
	}
}