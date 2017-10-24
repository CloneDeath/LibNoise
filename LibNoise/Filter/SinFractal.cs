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

			x *= Frequency;
			y *= Frequency;

			// Initialize value, fBM starts with 0
			float value = 0;

			// Inner loop of spectral construction, where the fractal is built

			for (curOctave = 0; curOctave < OctaveCount; curOctave++)
			{
				// Get the coherent-noise value.
				var signal = Primitive2D.GetValue(x, y) * SpectralWeights[curOctave];

				if (signal < 0.0)
					signal = -signal;

				// Add the signal to the output value.
				value += signal;

				// Go to the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
			}

			//take care of remainder in OctaveCount
			var remainder = OctaveCount - (int) OctaveCount;
			if (remainder > 0.0f)
				value += remainder * Primitive2D.GetValue(x, y) * SpectralWeights[curOctave];

			return (float) Math.Sin(ox + value);
		}

		
	    public float GetValue(float x, float y, float z)
		{
			int curOctave;
			var ox = x;

			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			// Initialize value, fBM starts with 0
			float value = 0;

			// Inner loop of spectral construction, where the fractal is built
			for (curOctave = 0; curOctave < OctaveCount; curOctave++)
			{
				// Get the coherent-noise value.
				var signal = Primitive3D.GetValue(x, y, z) * SpectralWeights[curOctave];

				if (signal < 0.0)
					signal = -signal;

				// Add the signal to the output value.
				value += signal;

				// Go to the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
				z *= Lacunarity;
			}

			//take care of remainder in OctaveCount
			var remainder = OctaveCount - (int) OctaveCount;
			if (remainder > 0.0f)
				value += remainder * Primitive3D.GetValue(x, y, z) * SpectralWeights[curOctave];

			return (float) Math.Sin(ox + value);
		}
	}
}