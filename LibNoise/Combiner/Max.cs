using System;

namespace LibNoise.Combiner
{
    /// <summary>
    ///     Noise module that outputs the larger of the two output values from two
    ///     source modules.
    /// </summary>
    public class Max : CombinerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			return Math.Max(((IModule3D)LeftModule).GetValue(x, y, z), ((IModule3D)RightModule).GetValue(x, y, z));
		}

		public Max()
		{
		}


		public Max(IModule left, IModule right)
			: base(left, right)
		{
		}
	}
}