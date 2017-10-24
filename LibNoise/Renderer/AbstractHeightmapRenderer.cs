using System;

namespace LibNoise.Renderer
{
    /// <summary>
    ///     Abstract base class for an heightmap renderer
    /// </summary>
    public abstract class AbstractHeightmapRenderer : AbstractRenderer
	{
		#region Ctor/Dtor

	    /// <summary>
	    ///     template constructor
	    /// </summary>
	    public AbstractHeightmapRenderer()
		{
			_WrapEnabled = false;
		}

		#endregion

		#region Fields

	    /// <summary>
	    ///     If wrapping is/ enabled, and the initial point is on the edge of
	    ///     the noise map, the appropriate neighbors that lie outside of the
	    ///     noise map will "wrap" to the opposite side(s) of the noise map.
	    ///     Enabling wrapping is useful when creating tileable heightmap
	    /// </summary>
	    protected bool _WrapEnabled;

	    /// <summary>
	    ///     Lower height boundary of the heightmap
	    /// </summary>
	    protected float _lowerHeightBound;

	    /// <summary>
	    ///     Upper height boundary of the heightmap
	    /// </summary>
	    protected float _upperHeightBound;

		#endregion

		#region Accessors

	    /// <summary>
	    ///     Gets or sets the lower height boundary of the heightmap
	    /// </summary>
	    public float LowerHeightBound => _lowerHeightBound;

	    /// <summary>
	    ///     Gets or sets the upper height boundary of the heightmap
	    /// </summary>
	    public float UpperHeightBound => _upperHeightBound;

	    /// <summary>
	    ///     Enables or disables heightmap wrapping.
	    /// </summary>
	    public bool WrapEnabled
		{
			get => _WrapEnabled;
			set => _WrapEnabled = value;
		}

		#endregion

		#region Interaction

	    /// <summary>
	    ///     Sets the boundaries of the heightmap.
	    ///     @throw ArgumentException if the lower boundary equals the upper boundary
	    ///     or if the lower boundary is greater than upper boundary
	    /// </summary>
	    /// <param name="lowerBound">The lower boundary of the heightmap</param>
	    /// <param name="upperBound">The upper boundary of the heightmap</param>
	    public void SetBounds(float lowerBound, float upperBound)
		{
			if (lowerBound == upperBound || lowerBound > upperBound)
				throw new ArgumentException("Incoherent bounds : lowerBound == upperBound or lowerBound > upperBound");

			_lowerHeightBound = lowerBound;
			_upperHeightBound = upperBound;
		}


	    /// <summary>
	    ///     Find in the noise map the lowest and highest value to define
	    ///     the LowerHeightBound and UpperHeightBound
	    /// </summary>
	    public void ExactFit()
		{
			_noiseMap.MinMax(out _lowerHeightBound, out _upperHeightBound);
		}


	    /// <summary>
	    ///     Renders the destination heightmap using the contents of the source
	    ///     noise map
	    /// </summary>
	    /// This class defines the main algorithm, children must implement
	    /// RenderHeight() method to render a value for the target heightmap
	    public override void Render()
		{
			if (_noiseMap == null)
				throw new ArgumentException("A noise map must be provided");

			if (CheckHeightmap() == false)
				throw new ArgumentException("An heightmap must be provided");

			if (_noiseMap.Width <= 0 || _noiseMap.Height <= 0)
				throw new ArgumentException("Incoherent noise map size (0,0)");

			if (_lowerHeightBound == _upperHeightBound || _lowerHeightBound > _upperHeightBound)
				throw new ArgumentException("Incoherent bounds : lowerBound == upperBound or lowerBound > upperBound");

			var width = _noiseMap.Width;
			var height = _noiseMap.Height;

			var rightEdge = width - 1;
			var topEdge = height - 1;

			var leftEdge = 0;
			var bottomEdge = 0;

			SetHeightmapSize(width, height);

			float pSource, pSourceOffset;

			int yOffset, xOffset;

			var boundDiff = _upperHeightBound - _lowerHeightBound;

			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					pSource = _noiseMap.GetValue(x, y);

					if (_WrapEnabled)
					{
						if (x == rightEdge)
							xOffset = leftEdge; // left edge
						else if (x == leftEdge)
							xOffset = rightEdge; // right edge
						else
							xOffset = x; // same

						if (y == topEdge)
							yOffset = bottomEdge; //bottom edge
						else if (y == bottomEdge)
							yOffset = topEdge; //top edge
						else
							yOffset = y; // same

						// Lerp between edge values
						if (xOffset != x || yOffset != y)
						{
							pSourceOffset = _noiseMap.GetValue(xOffset, yOffset);
							pSource = Libnoise.Lerp(pSource, pSourceOffset, 0.5f);
						}
					}

					// Implemented by children
					RenderHeight(x, y, pSource, boundDiff);
				}

				if (_callBack != null)
					_callBack(y);
			}
		}

		#endregion

		#region internal

	    /// <summary>
	    /// </summary>
	    /// <returns></returns>
	    protected abstract bool CheckHeightmap();

	    /// <summary>
	    ///     Sets the new size for the target heightmap.
	    /// </summary>
	    /// <param name="width">width The new width for the heightmap</param>
	    /// <param name="height">height The new height for the heightmap</param>
	    protected abstract void SetHeightmapSize(int width, int height);

	    /// <summary>
	    /// </summary>
	    /// <param name="x"></param>
	    /// <param name="y"></param>
	    /// <param name="source"></param>
	    /// <param name="boundDiff"></param>
	    protected abstract void RenderHeight(int x, int y, float source, float boundDiff);

		#endregion
	}
}