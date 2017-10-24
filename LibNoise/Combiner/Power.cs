using System;

namespace LibNoise.Combiner
{
    /// <summary>
    ///     Noise module that raises the output value from the left source module
    ///     to the power of the output value from the right source module.
    /// </summary>
    public class Power : CombinerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			return
				(float)
				Math.Pow(((IModule3D)LeftModule).GetValue(x, y, z), ((IModule3D)RightModule).GetValue(x, y, z));
		}

		public Power()
		{
		}


		public Power(IModule left, IModule right)
			: base(left, right)
		{
		}
	}
}