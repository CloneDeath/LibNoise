namespace LibNoise.Combiner
{
    /// <summary>
    ///     Noise module that outputs the sum of the two output values from two source modules.
    /// </summary>
    public class Add : CombinerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			return ((IModule3D)LeftModule).GetValue(x, y, z) + ((IModule3D)RightModule).GetValue(x, y, z);
		}

		public Add()
		{
		}


		public Add(IModule left, IModule right)
			: base(left, right)
		{
		}
	}
}