namespace LibNoise
{
    /// <summary>
    ///     Abstract interface for noise modules that calculates and outputs a value
    ///     given a four-dimensional input value.
    /// </summary>
    public interface IModule4D : IModule
	{
		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <param name="z">The input coordinate on the z-axis.</param>
	    /// <param name="t">The input coordinate on the t-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    float GetValue(float x, float y, float z, float t);
	}
}