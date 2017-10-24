namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that inverts the output value from a source module.
    /// </summary>
    public class Invert : IModule3D
	{
		protected IModule3D Source { get; set; }

		public float GetValue(float x, float y, float z)
		{
			return -Source.GetValue(x, y, z);
		}

		public Invert(IModule3D source)
		{
			Source = source;
		}
	}
}