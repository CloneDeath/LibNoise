namespace LibNoise.Filter
{
    /// <summary>
    ///     Noise module that outputs the input source value without modification.
    ///     Just a convenient class for any purpose.
    /// </summary>
    public class Pipe : FilterModule, IModule4D, IModule3D, IModule2D, IModule1D
	{
		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x)
		{
			x *= Frequency;

			return Primitive1D.GetValue(x);
		}

		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y)
		{
			x *= Frequency;
			y *= Frequency;

			return Primitive2D.GetValue(x, y);
		}

		
	    public float GetValue(float x, float y, float z)
		{
			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			return Primitive3D.GetValue(x, y, z);
		}

		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <param name="z">The input coordinate on the z-axis.</param>
	    /// <param name="t">The input coordinate on the t-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y, float z, float t)
		{
			x *= Frequency;
			y *= Frequency;
			z *= Frequency;
			t *= Frequency;

			return Primitive4D.GetValue(x, y, z, t);
		}
	}
}