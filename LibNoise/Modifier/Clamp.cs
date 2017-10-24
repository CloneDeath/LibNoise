namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that clamps the output value from a source module to a range of values.
    /// </summary>
    public class Clamp : IModule3D
	{
		protected IModule3D Source { get; set; }
		
	    public float GetValue(float x, float y, float z)
		{
			var value = Source.GetValue(x, y, z);

			if (value < LowerBound) return LowerBound;
			return value > UpperBound ? UpperBound : value;
		}

	    public float LowerBound { get; set; }
	    public float UpperBound { get; set; }

		public Clamp(IModule3D source, float lower, float upper)
		{
			Source = source;
			LowerBound = lower;
			UpperBound = upper;
		}
	}
}