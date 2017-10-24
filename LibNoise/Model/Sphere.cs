namespace LibNoise.Model
{
    /// <summary>
    ///     Model that defines the surface of a sphere.
    ///     This model returns an output value from a noise module given the
    ///     coordinates of an input value located on the surface of a sphere.
    ///     To generate an output value, pass the (latitude, longitude)
    ///     coordinates of an input value to the GetValue() method.
    ///     This model is useful for creating:
    ///     - seamless textures that can be mapped onto a sphere
    ///     - terrain height maps for entire planets
    ///     This sphere has a radius of 1.0 unit and its center is located at
    ///     the origin.
    /// </summary>
    public class Sphere : AbstractModel
	{
		#region Interaction

	    /// <summary>
	    ///     Returns the output value from the noise module given the
	    ///     (latitude, longitude) coordinates of the specified input value
	    ///     located on the surface of the sphere.
	    ///     Use a negative latitude if the input value is located on the
	    ///     southern hemisphere.
	    ///     Use a negative longitude if the input value is located on the
	    ///     western hemisphere.
	    /// </summary>
	    /// <param name="lat">The latitude of the input value, in degrees</param>
	    /// <param name="lon">The longitude of the input value, in degrees</param>
	    /// <returns>The output value from the noise module</returns>
	    public float GetValue(float lat, float lon)
		{
			float x = 0.0f, y = 0.0f, z = 0.0f;
			Libnoise.LatLonToXYZ(lat, lon, ref x, ref y, ref z);
			return ((IModule3D) PSourceModule).GetValue(x, y, z);
		}

		#endregion

		#region Ctor/Dtor

	    /// <summary>
	    ///     Default constructor
	    /// </summary>
	    public Sphere()
		{
		}


	    /// <summary>
	    ///     Constructor
	    /// </summary>
	    /// <param name="module">The noise module that is used to generate the output values</param>
	    public Sphere(IModule3D module)
			: base(module)
		{
		}

		#endregion

		#region Internal

		#endregion
	}
}