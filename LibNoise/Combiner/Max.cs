using System;

namespace LibNoise.Combiner
{
    /// <summary>
    ///     Noise module that outputs the larger of the two output values from two
    ///     source modules.
    /// </summary>
    public class Max : CombinerModule, IModule3D
	{
		#region IModule3D Members

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <param name="z">The input coordinate on the z-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y, float z)
		{
			return Math.Max(((IModule3D) _leftModule).GetValue(x, y, z), ((IModule3D) _rightModule).GetValue(x, y, z));
		}

		#endregion

		#region Ctor/Dtor

		public Max()
		{
		}


		public Max(IModule left, IModule right)
			: base(left, right)
		{
		}

		#endregion
	}
}