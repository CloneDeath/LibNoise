using System;

namespace LibNoise.Renderer
{
    /// <summary>
    ///     Renders a normal map from a noise map.
    ///     This class renders an image containing the normal vectors from a noise
    ///     map object.  This image can then be used as a bump map for a 3D
    ///     application or game.
    ///     This class encodes the (x, y, z) components of the normal vector into
    ///     the (red, green, blue) channels of the image.  Like any 24-bit
    ///     true-color image, the channel values range from 0 to 255.  0
    ///     represents a normal coordinate of -1.0 and 255 represents a normal
    ///     coordinate of +1.0.
    ///     You should also specify the <i>bump height</i> before rendering the
    ///     normal map.  The bump height specifies the ratio of spatial resolution
    ///     to elevation resolution.  For example, if your noise map has a spatial
    ///     resolution of 30 meters and an elevation resolution of one meter, set
    ///     the bump height to 1.0 / 30.0.
    ///     <b>Rendering the normal map</b>
    ///     To render the image containing the normal map, perform the following
    ///     steps:
    ///     - Pass a IMap2D
    ///     <float>
    ///         object to the NoiseMap property.
    ///         - Pass an IMap2D
    ///         <Color>
    ///             object to the Image property.
    ///             - Call the Render() method.
    /// </summary>
    public class NormalMapRenderer : AbstractImageRenderer
	{
		#region Ctor/Dtor

	    /// <summary>
	    ///     Default constructor
	    /// </summary>
	    public NormalMapRenderer()
		{
			_WrapEnabled = false;
			_bumpHeight = 1.0f;
		}

		#endregion

		#region Interaction

	    /// <summary>
	    ///     Renders the noise map to the destination image.
	    /// </summary>
	    public override void Render()
		{
			if (_noiseMap == null)
				throw new ArgumentException("A noise map must be provided");

			if (_image == null)
				throw new ArgumentException("An image map must be provided");

			if (_noiseMap.Width <= 0 || _noiseMap.Height <= 0)
				throw new ArgumentException("Incoherent noise map size (0,0)");

			var width = _noiseMap.Width;
			var height = _noiseMap.Height;
			var rightEdge = width - 1;
			var topEdge = height - 1;
			var leftEdgeOffset = -rightEdge;
			var bottomEdgeOffset = -topEdge;

			_image.SetSize(width, height);

			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					// Calculate the positions of the current point's right and up
					// neighbors.
					int yUpOffset, xRightOffset;

					if (_WrapEnabled)
					{
						if (x == rightEdge)
							xRightOffset = leftEdgeOffset; // left edge
						else
							xRightOffset = 1; // next

						if (y == topEdge)
							yUpOffset = bottomEdgeOffset; //bottom edge
						else
							yUpOffset = 1; // above
					}
					else
					{
						if (x == rightEdge)
							xRightOffset = 0; // same
						else
							xRightOffset = 1; // next

						if (y == topEdge)
							yUpOffset = 0; // same
						else
							yUpOffset = 1; // above
					}

					// Get the noise value of the current point in the source noise map
					// and the noise values of its right and up neighbors.
					var nc = _noiseMap.GetValue(x, y);
					var nr = _noiseMap.GetValue(x + xRightOffset, y);
					var nu = _noiseMap.GetValue(x, y + yUpOffset);

					// Blend the source color, background color, and the light
					// intensity together, then update the destination image with that
					// color.
					_image.SetValue(x, y, CalcNormalColor(nc, nr, nu, _bumpHeight));
				}

				if (_callBack != null)
					_callBack(y);
			}
		}

		#endregion

		#region Internal

	    /// <summary>
	    ///     Calculates the normal vector at a given point on the noise map.
	    ///     This method encodes the (x, y, z) components of the normal vector
	    ///     into the (red, green, blue) channels of the returned color.  In
	    ///     order to represent the vector as a color, each coordinate of the
	    ///     normal is mapped from the -1.0 to 1.0 range to the 0 to 255 range.
	    ///     The bump height specifies the ratio of spatial resolution to
	    ///     elevation resolution.  For example, if your noise map has a
	    ///     spatial resolution of 30 meters and an elevation resolution of one
	    ///     meter, set the bump height to 1.0 / 30.0.
	    ///     The spatial resolution and elevation resolution are determined by
	    ///     the application.
	    /// </summary>
	    /// <param name="nc">The height of the given point in the noise map</param>
	    /// <param name="nr">The height of the left neighbor</param>
	    /// <param name="nu">The height of the up neighbor</param>
	    /// <param name="bumpHeight">The bump height</param>
	    /// <returns>The normal vector represented as a color</returns>
	    private IColor CalcNormalColor(float nc, float nr, float nu, float bumpHeight)
		{
			// Calculate the surface normal.
			nc *= bumpHeight;
			nr *= bumpHeight;
			nu *= bumpHeight;

			var ncr = nc - nr;
			var ncu = nc - nu;
			var d = (float) Math.Sqrt(ncu * ncu + ncr * ncr + 1);
			var vxc = (nc - nr) / d;
			var vyc = (nc - nu) / d;
			var vzc = 1.0f / d;

			// Map the normal range from the (-1.0 .. +1.0) range to the (0 .. 255)
			// range.
			byte xc, yc, zc;

			xc = (byte) (Libnoise.FastFloor((vxc + 1.0f) * 127.5f) & 0xff);
			yc = (byte) (Libnoise.FastFloor((vyc + 1.0f) * 127.5f) & 0xff);
			zc = (byte) (Libnoise.FastFloor((vzc + 1.0f) * 127.5f) & 0xff);

			//
			//zc = (byte)((int)((floor)((vzc + 1.0f) * 127.5f)) & 0xff); 

			return new Color(xc, yc, zc, 255);
		}

		#endregion

		#region Fields

	    /// <summary>
	    ///     This object requires three points (the initial point and the right
	    ///     and up neighbors) to calculate the normal vector at that point.
	    ///     If wrapping is/ enabled, and the initial point is on the edge of
	    ///     the noise map, the appropriate neighbors that lie outside of the
	    ///     noise map will "wrap" to the opposite side(s) of the noise map.
	    ///     Otherwise, the appropriate neighbors are cropped to the edge of
	    ///     the noise map.
	    ///     Enabling wrapping is useful when creating spherical and tileable
	    ///     normal maps.
	    /// </summary>
	    protected bool _WrapEnabled;

	    /// <summary>
	    ///     The bump height specifies the ratio of spatial resolution to
	    ///     elevation resolution.  For example, if your noise map has a
	    ///     spatial resolution of 30 meters and an elevation resolution of one
	    ///     meter, set the bump height to 1.0 / 30.0.
	    ///     The spatial resolution and elevation resolution are determined by
	    ///     the application.
	    /// </summary>
	    protected float _bumpHeight;

		#endregion

		#region Accessors

	    /// <summary>
	    ///     Enables or disables noise-map wrapping.
	    ///     This object requires five points (the initial point and its four
	    ///     neighbors) to calculate light shading.  If wrapping is enabled,
	    ///     and the initial point is on the edge of the noise map, the
	    ///     appropriate neighbors that lie outside of the noise map will
	    ///     "wrap" to the opposite side(s) of the noise map.  Otherwise, the
	    ///     appropriate neighbors are cropped to the edge of the noise map.
	    ///     Enabling wrapping is useful when creating spherical renderings and
	    ///     tileable textures.
	    /// </summary>
	    public bool WrapEnabled
		{
			get => _WrapEnabled;
			set => _WrapEnabled = value;
		}

	    /// <summary>
	    ///     Gets or Sets the bump height
	    /// </summary>
	    public float BumpHeight
		{
			get => _bumpHeight;
			set => _bumpHeight = value;
		}

		#endregion
	}
}