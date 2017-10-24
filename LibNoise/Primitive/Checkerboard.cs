namespace LibNoise.Primitive
{
    /// <summary>
    ///     Noise module that outputs a checkerboard pattern.
    ///     This noise module outputs unit-sized blocks of alternating values.
    ///     The values of these blocks alternate between -1.0 and +1.0.
    ///     This noise module is not really useful by itself, but it is often used
    ///     for debugging purposes.
    /// </summary>
    public class Checkerboard : PrimitiveModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			// Fast floor
			var ix = x > 0.0 ? (int) x : (int) x - 1;
			var iy = y > 0.0 ? (int) y : (int) y - 1;
			var iz = z > 0.0 ? (int) z : (int) z - 1;

			return ((ix & 1) ^ (iy & 1) ^ (iz & 1)) != 0 ? -1.0f : 1.0f;
		}
	}
}