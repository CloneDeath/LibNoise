//
// The following code is based on Ken Musgrave's explanations and sample
// source code in the book "Texturing and Modelling: A procedural approach"

namespace LibNoise.Filter
{
    /// <summary>
    ///     Noise module that outputs 3-dimensional Heterogeneous-multifractal noise.
    ///     Heterogeneous multifractal is similar to multifractal; the single perturbation is computed as follows:
    ///     offset is first added to gradient noise and then the result is multiplied to the i-th spectral weight.
    ///     The result is, in turn, multiplied with the current noise value. Perturbations are then combined additively.
    ///     The overall result is a soft version of multifractal algorithm, where slopes are less pronounced.
    ///     (From http://meshlab.sourceforge.net/wiki/index.php/Fractal_Creation )
    ///     This noise module outputs values that usually range from offset to offset *2.5,
    ///     but there are no guarantees that all output values will exist within that range.
    /// </summary>
    public class HeterogeneousMultiFractal : FilterModule, IModule3D, IModule2D
	{
		#region IModule2D Members

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y)
		{
			float signal;
			int curOctave;

			x *= _frequency;
			y *= _frequency;

			// Initialize value : first unscaled octave of function; later octaves are scaled 
			var value = _offset + _source2D.GetValue(x, y);

			x *= _lacunarity;
			y *= _lacunarity;

			// inner loop of spectral construction, where the fractal is built
			for (curOctave = 1; curOctave < _octaveCount; curOctave++)
			{
				// obtain displaced noise value.
				signal = _offset + _source2D.GetValue(x, y);

				//scale amplitude appropriately for this frequency 
				signal *= _spectralWeights[curOctave];

				// scale increment by current altitude of function
				signal *= value;

				// Add the signal to the output value.
				value += signal;

				// Go to the next octave.
				x *= _lacunarity;
				y *= _lacunarity;
			}

			//take care of remainder in _octaveCount
			var remainder = _octaveCount - (int) _octaveCount;

			if (remainder > 0.0f)
			{
				signal = _offset + _source2D.GetValue(x, y);
				signal *= _spectralWeights[curOctave];
				signal *= value;
				signal *= remainder;
				value += signal;
			}

			return value;
		}

		#endregion

		#region IModule3D Members

	    /// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <param name="z">The input coordinate on the z-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y, float z)
		{
			float signal;
			int curOctave;

			x *= _frequency;
			y *= _frequency;
			z *= _frequency;

			// Initialize value : first unscaled octave of function; later octaves are scaled 
			var value = _offset + _source3D.GetValue(x, y, z);

			x *= _lacunarity;
			y *= _lacunarity;
			z *= _lacunarity;

			// inner loop of spectral construction, where the fractal is built
			for (curOctave = 1; curOctave < _octaveCount; curOctave++)
			{
				// obtain displaced noise value.
				signal = _offset + _source3D.GetValue(x, y, z);

				//scale amplitude appropriately for this frequency 
				signal *= _spectralWeights[curOctave];

				// scale increment by current altitude of function
				signal *= value;

				// Add the signal to the output value.
				value += signal;

				// Go to the next octave.
				x *= _lacunarity;
				y *= _lacunarity;
				z *= _lacunarity;
			}

			//take care of remainder in _octaveCount
			var remainder = _octaveCount - (int) _octaveCount;

			if (remainder > 0.0f)
			{
				signal = _offset + _source3D.GetValue(x, y, z);
				signal *= _spectralWeights[curOctave];
				signal *= value;
				signal *= remainder;
				value += signal;
			}

			return value;
		}

		#endregion

		#region Ctor/Dtor

		#endregion
	}
}