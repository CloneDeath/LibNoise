// The following code is based on Ken Musgrave's explanations and sample
// source code in the book "Texturing and Modelling: A procedural approach"

using System;

namespace LibNoise.Filter
{
    /// <summary>
    ///     Noise module that outputs 3-dimensional Sin Fractal noise.
    ///     This noise module is nearly identical to SumFractal except
    ///     this noise module modifies each octave with an absolute-value
    ///     function and apply a Sine function of output value + x coordinate
    /// </summary>
    public class SinFractal : FilterModule, IModule3D, IModule2D
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
			var ox = x;
			int curOctave;

			x *= _frequency;
			y *= _frequency;

			// Initialize value, fBM starts with 0
			float value = 0;

			// Inner loop of spectral construction, where the fractal is built

			for (curOctave = 0; curOctave < _octaveCount; curOctave++)
			{
				// Get the coherent-noise value.
				var signal = _source2D.GetValue(x, y) * _spectralWeights[curOctave];

				if (signal < 0.0)
					signal = -signal;

				// Add the signal to the output value.
				value += signal;

				// Go to the next octave.
				x *= _lacunarity;
				y *= _lacunarity;
			}

			//take care of remainder in _octaveCount
			var remainder = _octaveCount - (int) _octaveCount;
			if (remainder > 0.0f)
				value += remainder * _source2D.GetValue(x, y) * _spectralWeights[curOctave];

			return (float) Math.Sin(ox + value);
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
			int curOctave;
			var ox = x;

			x *= _frequency;
			y *= _frequency;
			z *= _frequency;

			// Initialize value, fBM starts with 0
			float value = 0;

			// Inner loop of spectral construction, where the fractal is built
			for (curOctave = 0; curOctave < _octaveCount; curOctave++)
			{
				// Get the coherent-noise value.
				var signal = _source3D.GetValue(x, y, z) * _spectralWeights[curOctave];

				if (signal < 0.0)
					signal = -signal;

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
				value += remainder * _source3D.GetValue(x, y, z) * _spectralWeights[curOctave];

			return (float) Math.Sin(ox + value);
		}

		#endregion

		#region Ctor/Dtor

		#endregion
	}
}