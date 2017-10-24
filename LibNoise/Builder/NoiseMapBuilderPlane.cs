using System;
using LibNoise.Model;

namespace LibNoise.Builder
{
    /// <summary>
    ///     Builds a planar noise map.
    ///     This class builds a noise map by filling it with coherent-noise values
    ///     generated from the surface of a plane.
    ///     This class describes these input values using (x, z) coordinates.
    ///     Their y coordinates are always 0.0.
    ///     The application must provide the lower and upper x coordinate bounds
    ///     of the noise map, in units, and the lower and upper z coordinate
    ///     bounds of the noise map, in units.
    ///     To make a tileable noise map with no seams at the edges, use the
    ///     Seamless property.
    /// </summary>
    public class NoiseMapBuilderPlane : NoiseMapBuilder
	{
		/// <summary>
	    ///     Lower x boundary of the planar noise map, in units.
	    /// </summary>
	    private float _lowerXBound;

	    /// <summary>
	    ///     Lower z boundary of the planar noise map, in units.
	    /// </summary>
	    private float _lowerZBound;

	    /// <summary>
	    ///     A flag specifying whether seamless tiling is enabled.
	    /// </summary>
	    private bool _seamless;

	    /// <summary>
	    ///     Upper x boundary of the planar noise map, in units.
	    /// </summary>
	    private float _upperXBound;

	    /// <summary>
	    ///     Upper z boundary of the planar noise map, in units.
	    /// </summary>
	    private float _upperZBound;

		/// <summary>
	    ///     Gets or sets a flag specifying whether seamless tiling is enabled.
	    /// </summary>
	    public bool Seamless
		{
			get => _seamless;
			set => _seamless = value;
		}

	    /// <summary>
	    ///     Gets the lower x boundary of the planar noise map, in units.
	    /// </summary>
	    public float LowerXBound => _lowerXBound;

	    /// <summary>
	    ///     Gets the lower z boundary of the planar noise map, in units.
	    /// </summary>
	    public float LowerZBound => _lowerZBound;

	    /// <summary>
	    ///     Gets the upper x boundary of the planar noise map, in units.
	    /// </summary>
	    public float UpperXBound => _upperXBound;

	    /// <summary>
	    ///     Gets the upper z boundary of the planar noise map, in units.
	    /// </summary>
	    public float UpperZBound => _upperZBound;

		/// <summary>
	    ///     Default constructor
	    /// </summary>
	    public NoiseMapBuilderPlane()
		{
			_seamless = false;
			_lowerXBound = _lowerZBound = _upperXBound = _upperZBound = 0.0f;
		}


	    /// <summary>
	    ///     Create a new plane with given value.
	    ///     @pre The lower x boundary is less than the upper x boundary.
	    ///     @pre The lower z boundary is less than the upper z boundary.
	    ///     @throw ArgumentException See the preconditions.
	    /// </summary>
	    /// <param name="lowerXBound">The lower x boundary of the noise map, in units.</param>
	    /// <param name="upperXBound">The upper x boundary of the noise map, in units.</param>
	    /// <param name="lowerZBound">The lower z boundary of the noise map, in units.</param>
	    /// <param name="upperZBound">The upper z boundary of the noise map, in units.</param>
	    /// <param name="seamless">a flag specifying whether seamless tiling is enabled.</param>
	    public NoiseMapBuilderPlane(float lowerXBound, float upperXBound, float lowerZBound, float upperZBound,
			bool seamless)
		{
			_seamless = seamless;
			SetBounds(lowerXBound, upperXBound, lowerZBound, upperZBound);
		}

		/// <summary>
	    ///     Sets the boundaries of the planar noise map.
	    ///     @pre The lower x boundary is less than the upper x boundary.
	    ///     @pre The lower z boundary is less than the upper z boundary.
	    ///     @throw ArgumentException See the preconditions.
	    /// </summary>
	    /// <param name="lowerXBound">The lower x boundary of the noise map, in units.</param>
	    /// <param name="upperXBound">The upper x boundary of the noise map, in units.</param>
	    /// <param name="lowerZBound">The lower z boundary of the noise map, in units.</param>
	    /// <param name="upperZBound">The upper z boundary of the noise map, in units.</param>
	    public void SetBounds(float lowerXBound, float upperXBound, float lowerZBound, float upperZBound)
		{
			if (lowerXBound >= upperXBound || lowerZBound >= upperZBound)
				throw new ArgumentException(
					"Incoherent bounds : lowerXBound >= upperXBound or lowerZBound >= upperZBound");

			_lowerXBound = lowerXBound;
			_upperXBound = upperXBound;
			_lowerZBound = lowerZBound;
			_upperZBound = upperZBound;
		}


	    /// <summary>
	    ///     Builds the noise map.
	    ///     @pre SetBounds() was previously called.
	    ///     @pre NoiseMap was previously defined.
	    ///     @pre a SourceModule was previously defined.
	    ///     @pre The width and height values specified by SetSize() are
	    ///     positive.
	    ///     @pre The width and height values specified by SetSize() do not
	    ///     exceed the maximum possible width and height for the noise map.
	    ///     @post The original contents of the destination noise map is
	    ///     destroyed.
	    ///     @throw noise::ArgumentException See the preconditions.
	    ///     If this method is successful, the destination noise map contains
	    ///     the coherent-noise values from the noise module specified by
	    ///     the SourceModule.
	    /// </summary>
	    public override void Build()
		{
			if (_lowerXBound >= _upperXBound || _lowerZBound >= _upperZBound)
				throw new ArgumentException(
					"Incoherent bounds : lowerXBound >= upperXBound or lowerZBound >= upperZBound");

			if (PWidth < 0 || PHeight < 0)
				throw new ArgumentException("Dimension must be greater or equal 0");

			if (PSourceModule == null)
				throw new ArgumentException("A source module must be provided");

			if (PNoiseMap == null)
				throw new ArgumentException("A noise map must be provided");

			// Resize the destination noise map so that it can store the new output
			// values from the source model.
			PNoiseMap.SetSize(PWidth, PHeight);

			// Create the plane model.
			var model = new Plane(PSourceModule);

			var xExtent = _upperXBound - _lowerXBound;
			var zExtent = _upperZBound - _lowerZBound;
			var xDelta = xExtent / PWidth;
			var zDelta = zExtent / PHeight;
			var zCur = _lowerZBound;

			// Fill every point in the noise map with the output values from the model.
			for (var z = 0; z < PHeight; z++)
			{
				var xCur = _lowerXBound;

				for (var x = 0; x < PWidth; x++)
				{
					float finalValue;
					var level = FilterLevel.Source;

					if (Filter != null)
						level = Filter.IsFiltered(x, z);

					if (level == FilterLevel.Constant && Filter != null)
					{
						finalValue = Filter.ConstantValue;
					}
					else
					{
						if (_seamless)
						{
							var swValue = model.GetValue(xCur, zCur);
							var seValue = model.GetValue(xCur + xExtent, zCur);
							var nwValue = model.GetValue(xCur, zCur + zExtent);
							var neValue = model.GetValue(xCur + xExtent, zCur + zExtent);

							var xBlend = 1.0f - (xCur - _lowerXBound) / xExtent;
							var zBlend = 1.0f - (zCur - _lowerZBound) / zExtent;

							var z0 = Libnoise.Lerp(swValue, seValue, xBlend);
							var z1 = Libnoise.Lerp(nwValue, neValue, xBlend);

							finalValue = Libnoise.Lerp(z0, z1, zBlend);
						}
						else
						{
							finalValue = model.GetValue(xCur, zCur);
						}

						if (level == FilterLevel.Filter && Filter != null)
							finalValue = Filter.FilterValue(x, z, finalValue);
					}

					PNoiseMap.SetValue(x, z, finalValue);

					xCur += xDelta;
				}

				zCur += zDelta;

				if (PCallBack != null)
					PCallBack(z);
			}
		}
	}
}