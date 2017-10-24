namespace LibNoise
{
    /// <summary>
    ///     Abstract interface for noise modules that calculates and outputs a value
    ///     given a two-dimensional input value.
    /// </summary>
    public interface IModule2D : IModule
	{
		#region Interaction

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    float GetValue(float x, float y);

		#endregion
	}
}