namespace LibNoise
{
    /// <inheritdoc />
    /// <summary>
    ///     Abstract interface for noise modules that calculates and outputs a value
    ///     given a three-dimensional input value.
    /// </summary>
    public interface IModule3D : IModule
	{
	    
	    float GetValue(float x, float y, float z);
	}
}