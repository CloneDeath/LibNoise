namespace LibNoise.Combiner
{
    /// <summary>
    ///     Noise module that outputs the product of the two output values from
    ///     two source modules.
    /// </summary>
    public class Multiply : CombinerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			return ((IModule3D)LeftModule).GetValue(x, y, z) * ((IModule3D)RightModule).GetValue(x, y, z);
		}

		public Multiply()
		{
		}


		public Multiply(IModule left, IModule right)
			: base(left, right)
		{
		}
	}
}