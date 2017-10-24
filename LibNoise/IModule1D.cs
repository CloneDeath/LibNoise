namespace LibNoise
{
    /// <summary>
    ///     Abstract interface for noise modules that calculates and outputs a value
    ///     given a one-dimensional input value.
    /// </summary>
    public interface IModule1D : IModule
	{
		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    float GetValue(float x);
	}
}