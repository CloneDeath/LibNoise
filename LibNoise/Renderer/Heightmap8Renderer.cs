namespace LibNoise.Renderer
{
    /// <summary>
    ///     class for an 8bit-heightmap renderer
    /// </summary>
    public class Heightmap8Renderer : AbstractHeightmapRenderer
	{
		/// <summary>
	    ///     The destination heightmap
	    /// </summary>
	    protected Heightmap8 _heightmap;

		/// <summary>
	    ///     Gets or sets the destination heightmap
	    /// </summary>
	    public Heightmap8 Heightmap
		{
			get => _heightmap;
			set => _heightmap = value;
		}

		/// <summary>
	    ///     Sets the new size for the target heightmap.
	    /// </summary>
	    /// <param name="width">width The new width for the heightmap</param>
	    /// <param name="height">height The new height for the heightmap</param>
	    protected override void SetHeightmapSize(int width, int height)
		{
			_heightmap.SetSize(width, height);
		}


	    /// <summary>
	    /// </summary>
	    /// <returns></returns>
	    protected override bool CheckHeightmap()
		{
			return _heightmap != null;
		}


	    /// <summary>
	    /// </summary>
	    /// <param name="x"></param>
	    /// <param name="y"></param>
	    /// <param name="source"></param>
	    /// <param name="boundDiff"></param>
	    protected override void RenderHeight(int x, int y, float source, float boundDiff)
		{
			byte elevation;

			if (source <= _lowerHeightBound)
				elevation = byte.MinValue;
			else if (source >= _upperHeightBound)
				elevation = byte.MaxValue;
			else
				elevation = (byte) ((source - _lowerHeightBound) / boundDiff * 255.0f);

			_heightmap.SetValue(x, y, elevation);
		}
	}
}