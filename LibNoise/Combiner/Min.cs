using System;

namespace LibNoise.Combiner
{
    /// <summary>
    ///     Noise module that outputs the smaller of the two output values from
    ///     two source modules.
    /// </summary>
    public class Min : CombinerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			return Math.Min(((IModule3D)LeftModule).GetValue(x, y, z), ((IModule3D)RightModule).GetValue(x, y, z));
		}

		public Min()
		{
		}


		public Min(IModule left, IModule right)
			: base(left, right)
		{
		}
	}
}