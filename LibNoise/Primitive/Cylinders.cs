using System;

namespace LibNoise.Primitive
{
    /// <summary>
    ///     Noise module that outputs concentric cylinders.
    ///     This noise module outputs concentric cylinders centered on the origin.
    ///     These cylinders are oriented along the y axis similar to the
    ///     concentric rings of a tree.  Each cylinder extends infinitely along
    ///     the y axis.
    ///     The first cylinder has a radius of 1.0.  Each subsequent cylinder has
    ///     a radius that is 1.0 unit larger than the previous cylinder.
    ///     The output value from this noise module is determined by the distance
    ///     between the input value and the the nearest cylinder surface.  The
    ///     input values that are located on a cylinder surface are given the
    ///     output value 1.0 and the input values that are equidistant from two
    ///     cylinder surfaces are given the output value -1.0.
    ///     An application can change the frequency of the concentric cylinders.
    ///     Increasing the frequency reduces the distances between cylinders.
    ///     This noise module, modified with some low-frequency, low-power
    ///     turbulence, is useful for generating wood-like textures.
    /// </summary>
    public class Cylinders : PrimitiveModule, IModule3D
	{
		#region Constants

	    /// <summary>
	    ///     Frequency of the concentric spheres.
	    /// </summary>
	    public const float DEFAULT_FREQUENCY = 1.0f;

		#endregion

		#region Fields

	    /// <summary>
	    ///     Frequency of the concentric cylinders.
	    /// </summary>
	    protected float _frequency = DEFAULT_FREQUENCY;

		#endregion

		#region Accessors

	    /// <summary>
	    ///     Gets or sets the frequency
	    /// </summary>
	    public float Frequency
		{
			get => _frequency;
			set => _frequency = value;
		}

		#endregion

		#region IModule3D Members

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <param name="z">The input coordinate on the z-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y, float z)
		{
			x *= _frequency;
			z *= _frequency;

			var distFromCenter = (float) Math.Sqrt(x * x + z * z);
			var distFromSmallerSphere = distFromCenter - (float) Math.Floor(distFromCenter);
			var distFromLargerSphere = 1.0f - distFromSmallerSphere;
			var nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
			return 1.0f - nearestDist * 4.0f; // Puts it in the -1.0 to +1.0 range.
		}

		#endregion

		#region Ctor/Dtor

	    /// <summary>
	    ///     Create new Cylinders generator with default values
	    /// </summary>
	    public Cylinders()
			: this(DEFAULT_FREQUENCY)
		{
		}


	    /// <summary>
	    ///     Create a new Cylinders generator with given values
	    /// </summary>
	    /// <param name="frequency"></param>
	    public Cylinders(float frequency)
		{
			_frequency = frequency;
		}

		#endregion
	}
}