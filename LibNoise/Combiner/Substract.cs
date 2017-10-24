namespace LibNoise.Combiner
{
    /// <summary>
    ///     Noise module that outputs the subtraction between the left module and the right module .
    /// </summary>
    public class Substract : CombinerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			return ((IModule3D)LeftModule).GetValue(x, y, z) - ((IModule3D)RightModule).GetValue(x, y, z);
		}

		public Substract()
		{
		}


		public Substract(IModule left, IModule right)
			: base(left, right)
		{
		}
	}
}