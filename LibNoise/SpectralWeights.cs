using System;

namespace LibNoise
{
	public class SpectralWeights
	{
		/// <summary>
		///     The lacunarity specifies the frequency multipler between successive
		///     octaves.
		///     The effect of modifying the lacunarity is subtle; you may need to play
		///     with the lacunarity value to determine the effects.  For best results,
		///     set the lacunarity to a number between 1.5 and 3.5.
		/// </summary>
		public float Lucinarity { get; set; }
		public float Exponent { get; set; }
		
		public float this[int octave] => (float) Math.Pow(Lucinarity, -octave * Exponent);
	}
}