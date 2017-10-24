namespace LibNoise.Model
{
    /// <summary>
    ///     Model that defines the surface of a plane.
    ///     This model returns an output value from a noise module given the
    ///     coordinates of an input value located on the surface of an ( x, z ) plane.
    ///     To generate an output value, pass the (x, z) coordinates of
    ///     an input value to the GetValue() method.
    ///     This model is useful for creating:
    ///     - two-dimensional textures
    ///     - terrain height maps for local areas
    ///     This plane extends infinitely in both directions.
    /// </summary>
    public class Plane : AbstractModel
	{
		#region Interaction

	    /// <summary>
	    ///     Returns the output value from the noise module given the
	    ///     (x, z) coordinates of the specified input value located
	    ///     on the surface of the plane.
	    /// </summary>
	    /// <param name="x">The x coordinate of the input value</param>
	    /// <param name="z">The z coordinate of the input value</param>
	    /// <returns>The output value from the noise module</returns>
	    public float GetValue(float x, float z)
		{
			return ((IModule3D) PSourceModule).GetValue(x, 0.0f, z);
		}

		#endregion

		#region Ctor/Dtor

	    /// <summary>
	    ///     Default constructor
	    /// </summary>
	    public Plane()
		{
		}


	    /// <summary>
	    ///     Constructor
	    /// </summary>
	    /// <param name="module">The noise module that is used to generate the output values</param>
	    public Plane(IModule module)
			: base(module)
		{
		}

		#endregion

		#region Internal

		#endregion
	}
}