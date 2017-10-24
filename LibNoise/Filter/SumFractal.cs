// The following code is based on Ken Musgrave's explanations and sample
// source code in the book "Texturing and Modelling: A procedural approach"

namespace LibNoise.Filter
{
    /// <summary>
    ///     Noise module that outputs 3-dimensional Sum Fractal noise. This noise
    ///     is also known as "Fractional BrownianMotion noise"
    ///     Sum Fractal noise is the sum of several coherent-noise functions of
    ///     ever-increasing frequencies and ever-decreasing amplitudes.
    ///     This class implements the original noise::module::Perlin
    /// </summary>
    public class SumFractal : FilterModule, IModule3D, IModule2D
	{
		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y)
		{
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

			return value;
		}

		
	    public float GetValue(float x, float y, float z)
		{
			float signal;
			float value;
			int curOctave;

			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			// Initialize value, fBM starts with 0
			value = 0;

			// Inner loop of spectral construction, where the fractal is built
			for (curOctave = 0; curOctave < OctaveCount; curOctave++)
			{
				// Get the coherent-noise value.
				signal = Primitive3D.GetValue(x, y, z) * SpectralWeights[curOctave];

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

			return value;
		}
	}
}