using System;

namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that outputs the absolute value of the output value from a source module.
    /// </summary>
    public class Abs : IModule3D
	{
		protected readonly IModule3D Source;

		public Abs(IModule3D source)
		{
			Source = source;
		}
		
	    public float GetValue(float x, float y, float z)
		{
			return Math.Abs(Source.GetValue(x, y, z));
		}
	}
}