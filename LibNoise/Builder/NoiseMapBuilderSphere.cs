﻿using System;
using LibNoise.Model;

namespace LibNoise.Builder
{
    /// <summary>
    ///     Builds a spherical noise map.
    ///     This class builds a noise map by filling it with coherent-noise values
    ///     generated from the surface of a sphere.
    ///     This class describes these input values using a (latitude, longitude)
    ///     coordinate system.  After generating the coherent-noise value from the
    ///     input value, it then "flattens" these coordinates onto a plane so that
    ///     it can write the values into a two-dimensional noise map.
    ///     The sphere model has a radius of 1.0 unit.  Its center is at the
    ///     origin.
    ///     The x coordinate in the noise map represents the longitude.  The y
    ///     coordinate in the noise map represents the latitude.
    ///     The application must provide the southern, northern, western, and
    ///     eastern bounds of the noise map, in degrees.
    /// </summary>
    public class NoiseMapBuilderSphere : NoiseMapBuilder
	{
		/// <summary>
	    ///     Default constructor
	    /// </summary>
	    public NoiseMapBuilderSphere()
		{
			SetBounds(-90f, 90f, -180f, 180f); // degrees
		}

		/// <summary>
	    ///     Eastern boundary of the spherical noise map, in degrees.
	    /// </summary>
	    private float _eastLonBound;

	    /// <summary>
	    ///     Northern boundary of the spherical noise map, in degrees.
	    /// </summary>
	    private float _northLatBound;

	    /// <summary>
	    ///     Southern boundary of the spherical noise map, in degrees.
	    /// </summary>
	    private float _southLatBound;

	    /// <summary>
	    ///     Western boundary of the spherical noise map, in degrees.
	    /// </summary>
	    private float _westLonBound;

		/// <summary>
	    ///     Gets the eastern boundary of the spherical noise map, in degrees.
	    /// </summary>
	    public float EastLonBound => _eastLonBound;

	    /// <summary>
	    ///     Gets the northern boundary of the spherical noise map, in degrees.
	    /// </summary>
	    public float NorthLatBound => _northLatBound;

	    /// <summary>
	    ///     Gets the southern boundary of the spherical noise map, in degrees.
	    /// </summary>
	    public float SouthLatBound => _southLatBound;

	    /// <summary>
	    ///     Gets the western boundary of the spherical noise map, in degrees.
	    /// </summary>
	    public float WestLonBound => _westLonBound;

		/// <summary>
	    ///     Sets the coordinate boundaries of the noise map.
	    ///     @pre The southern boundary is less than the northern boundary.
	    ///     @pre The western boundary is less than the eastern boundary.
	    /// </summary>
	    /// <param name="southLatBound"></param>
	    /// <param name="northLatBound"></param>
	    /// <param name="westLonBound"></param>
	    /// <param name="eastLonBound"></param>
	    public void SetBounds(float southLatBound, float northLatBound, float westLonBound, float eastLonBound)
		{
			if (southLatBound >= northLatBound || westLonBound >= eastLonBound)
				throw new ArgumentException(
					"Incoherent bounds : southLatBound >= northLatBound or westLonBound >= eastLonBound");

			_southLatBound = southLatBound;
			_northLatBound = northLatBound;
			_westLonBound = westLonBound;
			_eastLonBound = eastLonBound;
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
			if (_southLatBound >= _northLatBound || _westLonBound >= _eastLonBound)
				throw new ArgumentException(
					"Incoherent bounds : southLatBound >= northLatBound or westLonBound >= eastLonBound");

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
			var model = new Sphere((IModule3D) PSourceModule);

			var lonExtent = _eastLonBound - _westLonBound;
			var latExtent = _northLatBound - _southLatBound;

			var xDelta = lonExtent / PWidth;
			var yDelta = latExtent / PHeight;

			var curLat = _southLatBound;

			// Fill every point in the noise map with the output values from the model.
			for (var y = 0; y < PHeight; y++)
			{
				var curLon = _westLonBound;

				for (var x = 0; x < PWidth; x++)
				{
					float finalValue;
					var level = FilterLevel.Source;

					if (Filter != null)
						level = Filter.IsFiltered(x, y);

					if (level == FilterLevel.Constant && Filter != null)
					{
						finalValue = Filter.ConstantValue;
					}
					else
					{
						finalValue = model.GetValue(curLat, curLon);

						if (level == FilterLevel.Filter && Filter != null)
							finalValue = Filter.FilterValue(x, y, finalValue);
					}

					PNoiseMap.SetValue(x, y, finalValue);

					curLon += xDelta;
				}

				curLat += yDelta;

				if (PCallBack != null)
					PCallBack(y);
			}
		}
	}
}