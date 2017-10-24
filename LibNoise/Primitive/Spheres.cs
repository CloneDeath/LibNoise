using System;

namespace LibNoise.Primitive
{
    /// <summary>
    ///     Noise module that outputs concentric spheres.
    ///     This noise module outputs concentric spheres centered on the origin
    ///     like the concentric rings of an onion.
    ///     The first sphere has a radius of 1.0.  Each subsequent sphere has a
    ///     radius that is 1.0 unit larger than the previous sphere.
    ///     The output value from this noise module is determined by the distance
    ///     between the input value and the the nearest spherical surface.  The
    ///     input values that are located on a spherical surface are given the
    ///     output value 1.0 and the input values that are equidistant from two
    ///     spherical surfaces are given the output value -1.0.
    ///     An application can change the frequency of the concentric spheres.
    ///     Increasing the frequency reduces the distances between spheres.
    ///     This noise module, modified with some low-frequency, low-power
    ///     turbulence, is useful for generating agate-like textures.
    /// </summary>
    public class Spheres : PrimitiveModule, IModule3D
	{
		/// <summary>
		///     Frequency of the concentric cylinders.
		/// </summary>
	    public float Frequency { get; set; }

		
	    public float GetValue(float x, float y, float z)
		{
			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			var distFromCenter = (float) Math.Sqrt(x * x + y * y + z * z);
			var distFromSmallerSphere = distFromCenter - (float) Math.Floor(distFromCenter);
			var distFromLargerSphere = 1.0f - distFromSmallerSphere;
			var nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
			return 1.0f - nearestDist * 4.0f; // Puts it in the -1.0 to +1.0 range.
		}

		/// <summary>
	    ///     Create new Spheres generator with default values
	    /// </summary>
	    public Spheres()
			: this(1.0f)
		{
		}


	    /// <summary>
	    ///     Create a new Spheres generator with given values
	    /// </summary>
	    /// <param name="frequency"></param>
	    public Spheres(float frequency)
		{
			Frequency = frequency;
		}
	}
}