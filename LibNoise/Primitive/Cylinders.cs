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
		/// <summary>
		///     Frequency of the concentric cylinders.
		/// </summary>
		public float Frequency { get; set; }

		
	    public float GetValue(float x, float y, float z)
		{
			x *= Frequency;
			z *= Frequency;

			var distFromCenter = (float) Math.Sqrt(x * x + z * z);
			var distFromSmallerSphere = distFromCenter - (float) Math.Floor(distFromCenter);
			var distFromLargerSphere = 1.0f - distFromSmallerSphere;
			var nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
			return 1.0f - nearestDist * 4.0f; // Puts it in the -1.0 to +1.0 range.
		}

		/// <summary>
	    ///     Create new Cylinders generator with default values
	    /// </summary>
	    public Cylinders()
			: this(1.0f)
		{
		}


	    /// <summary>
	    ///     Create a new Cylinders generator with given values
	    /// </summary>
	    /// <param name="frequency"></param>
	    public Cylinders(float frequency)
		{
			Frequency = frequency;
		}
	}
}