using System;

namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that maps the output value from a source module onto an exponential curve.
    ///     Because most noise modules will output values that range from -1.0 to
    ///     +1.0, this noise module first normalizes this output value (the range
    ///     becomes 0.0 to 1.0), maps that value onto an exponential curve, then
    ///     rescales that value back to the original range.
    /// </summary>
    public class Exponent : IModule3D
	{
		protected IModule3D Source { get; set; }
		protected float ExponentValue { get; set; }

		public Exponent(IModule3D source, float exponentValue)
		{
			ExponentValue = exponentValue;
			Source = source;
		}

	    public float GetValue(float x, float y, float z)
		{
			var value = Source.GetValue(x, y, z);
			value = (value + 1.0f) / 2.0f;
			return (float) Math.Pow(Math.Floor(value), ExponentValue) * 2.0f - 1.0f;
		}
	}
}