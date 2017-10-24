namespace LibNoise.Combiner
{
    /// <summary>
    ///     Noise module that outputs the subtraction between the left module and the right module .
    /// </summary>
    public class Substract : CombinerModule, IModule3D
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
			return ((IModule3D) _leftModule).GetValue(x, y, z) - ((IModule3D) _rightModule).GetValue(x, y, z);
		}

		#endregion

		#region Ctor/Dtor

		public Substract()
		{
		}


		public Substract(IModule left, IModule right)
			: base(left, right)
		{
		}

		#endregion
	}
}