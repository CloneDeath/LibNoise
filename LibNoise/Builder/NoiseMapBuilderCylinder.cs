﻿using System;
using LibNoise.Model;

namespace LibNoise.Builder
{
    /// <summary>
    ///     Builds a cylindrical noise map.
    ///     This class builds a noise map by filling it with coherent-noise values
    ///     generated from the surface of a cylinder.
    ///     This class describes these input values using an (angle, height)
    ///     coordinate system.  After generating the coherent-noise value from the
    ///     input value, it then "flattens" these coordinates onto a plane so that
    ///     it can write the values into a two-dimensional noise map.
    ///     The cylinder model has a radius of 1.0 unit and has infinite height.
    ///     The cylinder is oriented along the y axis.  Its center is at the
    ///     origin.
    ///     The x coordinate in the noise map represents the angle around the
    ///     cylinder's y axis.  The y coordinate in the noise map represents the
    ///     height above the x-z plane.
    ///     The application must provide the lower and upper angle bounds of the
    ///     noise map, in degrees, and the lower and upper height bounds of the
    ///     noise map, in units.
    /// </summary>
    public class NoiseMapBuilderCylinder : NoiseMapBuilder
	{
		/// <summary>
	    ///     Default constructor
	    /// </summary>
	    public NoiseMapBuilderCylinder()
		{
			SetBounds(-180.0f, 180.0f, -10.0f, 10.0f);
		}

		/// <summary>
	    ///     Lower Angle boundary of the planar noise map, in units.
	    /// </summary>
	    private float _lowerAngleBound;

	    /// <summary>
	    ///     Lower Height boundary of the planar noise map, in units.
	    /// </summary>
	    private float _lowerHeightBound;

	    /// <summary>
	    ///     Upper Angle boundary of the planar noise map, in units.
	    /// </summary>
	    private float _upperAngleBound;

	    /// <summary>
	    ///     Upper Height boundary of the planar noise map, in units.
	    /// </summary>
	    private float _upperHeightBound;

		/// <summary>
	    ///     Gets the lower Height boundary of the planar noise map, in units.
	    /// </summary>
	    public float LowerHeightBound => _lowerHeightBound;

	    /// <summary>
	    ///     Gets the lower Angle boundary of the planar noise map, in units.
	    /// </summary>
	    public float LowerAngleBound => _lowerAngleBound;

	    /// <summary>
	    ///     Gets the upper Angle boundary of the planar noise map, in units.
	    /// </summary>
	    public float UpperAngleBound => _upperAngleBound;

	    /// <summary>
	    ///     Gets the upper Height boundary of the planar noise map, in units.
	    /// </summary>
	    public float UpperHeightBound => _upperHeightBound;

		/// <summary>
	    ///     Sets the boundaries of the planar noise map.
	    ///     @pre The lower Angle boundary is less than the upper Angle boundary.
	    ///     @pre The lower Height boundary is less than the upper Height boundary.
	    ///     @throw ArgumentException See the preconditions.
	    /// </summary>
	    /// <param name="lowerAngleBound">The lower Angle boundary of the noise map, in units.</param>
	    /// <param name="upperAngleBound">The upper Angle boundary of the noise map, in units.</param>
	    /// <param name="lowerHeightBound">The lower Height boundary of the noise map, in units.</param>
	    /// <param name="upperHeightBound">The upper Height boundary of the noise map, in units.</param>
	    public void SetBounds(float lowerAngleBound, float upperAngleBound, float lowerHeightBound,
			float upperHeightBound)
		{
			if (lowerAngleBound >= upperAngleBound || lowerHeightBound >= upperHeightBound)
				throw new ArgumentException(
					"Incoherent bounds : lowerAngleBound >= upperAngleBound or lowerZBound >= upperHeightBound");

			_lowerAngleBound = lowerAngleBound;
			_upperAngleBound = upperAngleBound;
			_lowerHeightBound = lowerHeightBound;
			_upperHeightBound = upperHeightBound;
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
			if (_lowerAngleBound >= _upperAngleBound || _lowerHeightBound >= _upperHeightBound)
				throw new ArgumentException(
					"Incoherent bounds : lowerAngleBound >= upperAngleBound or lowerZBound >= upperHeightBound");

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
			var model = new Cylinder((IModule3D) PSourceModule);

			var angleExtent = _upperAngleBound - _lowerAngleBound;
			var heightExtent = _upperHeightBound - _lowerHeightBound;

			var xDelta = angleExtent / PWidth;
			var yDelta = heightExtent / PHeight;

			var curHeight = _lowerHeightBound;

			// Fill every point in the noise map with the output values from the model.
			for (var y = 0; y < PHeight; y++)
			{
				var curAngle = _lowerAngleBound;

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
						finalValue = model.GetValue(curAngle, curHeight);

						if (level == FilterLevel.Filter && Filter != null)
							finalValue = Filter.FilterValue(x, y, finalValue);
					}

					PNoiseMap.SetValue(x, y, finalValue);

					curAngle += xDelta;
				}

				curHeight += yDelta;

				if (PCallBack != null)
					PCallBack(y);
			}
		}
	}
}