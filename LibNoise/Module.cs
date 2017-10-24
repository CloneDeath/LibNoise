namespace LibNoise
{
    /// <summary>
    ///     Abstract interface for noise modules.
    ///     A <i>noise module</i> is an object that calculates and outputs a value
    ///     given a N-dimensional input value.
    ///     Each type of noise module uses a specific method to calculate an
    ///     output value.  Some of these methods include:
    ///     - Calculating a value using a coherent-noise function or some other
    ///     mathematical function.
    ///     - Mathematically changing the output value from another noise module
    ///     in various ways.
    ///     - Combining the output values from two noise modules in various ways.
    /// </summary>
    public interface IModule
	{
	}
}