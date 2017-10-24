//
// The following code is based on Ken Musgrave's explanations and sample
// source code in the book "Texturing and Modelling: A procedural approach"

namespace LibNoise.Filter
{
    /// <summary>
    ///     Noise module that outputs 3-dimensional hybrid-multifractal noise.
    ///     Hybrid-multifractal noise the perturbations are combined additively,
    ///     but the single perturbation is computed by multiplying two quantities
    ///     called weight and signal. The signal quantity is the standard multifractal
    ///     perturbation, and the weight quantity is the multiplicative combination
    ///     of all the previous signal quantities.
    ///     Hybrid-multifractal attempts to control the amount of details according
    ///     to the slope of the underlying overlays. Hybrid Multifractal  is
    ///     conventionally used to generate terrains with smooth valley areas and
    ///     rough peaked mountains. With high Lacunarity values, it tends to produce
    ///     embedded plateaus.
    ///     Some good parameter values to start with:
    ///     gain = 1.0;
    ///     offset = 0.7;
    ///     spectralExponent = 0.25;
    /// </summary>
    public class HybridMultiFractal : FilterModule, IModule3D, IModule2D
	{
		/// <summary>
	    ///     0-args constructor
	    /// </summary>
	    public HybridMultiFractal()
		{
			Gain = 1.0f;
			Offset = 0.7f;
			SpectralExponent = 0.25f;
		}

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

			x *= Frequency;
			y *= Frequency;

			// Initialize value : get first octave of function; later octaves are weighted
			var value = Primitive2D.GetValue(x, y) + Offset;
			var weight = Gain * value;

			x *= Lacunarity;
			y *= Lacunarity;

			// inner loop of spectral construction, where the fractal is built
			for (curOctave = 1; weight > 0.001 && curOctave < OctaveCount; curOctave++)
			{
				// prevent divergence
				if (weight > 1.0)
					weight = 1.0f;

				// get next higher frequency
				signal = (Offset + Primitive2D.GetValue(x, y)) * SpectralWeights[curOctave];

				// The weighting from the previous octave is applied to the signal.
				signal *= weight;

				// Add the signal to the output value.
				value += signal;

				// update the (monotonically decreasing) weighting value
				weight *= Gain * signal;

				// Go to the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
			}

			//take care of remainder in OctaveCount
			var remainder = OctaveCount - (int) OctaveCount;

			if (remainder > 0.0f)
			{
				signal = Primitive2D.GetValue(x, y);
				signal *= SpectralWeights[curOctave];
				signal *= remainder;
				value += signal;
			}

			return value;
		}

		
	    public float GetValue(float x, float y, float z)
		{
			float signal;
			int curOctave;

			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			// Initialize value : get first octave of function; later octaves are weighted
			var value = Primitive3D.GetValue(x, y, z) + Offset;
			var weight = Gain * value;

			x *= Lacunarity;
			y *= Lacunarity;
			z *= Lacunarity;

			// inner loop of spectral construction, where the fractal is built
			for (curOctave = 1; weight > 0.001 && curOctave < OctaveCount; curOctave++)
			{
				// prevent divergence
				if (weight > 1.0)
					weight = 1.0f;

				// get next higher frequency
				signal = (Offset + Primitive3D.GetValue(x, y, z)) * SpectralWeights[curOctave];

				// The weighting from the previous octave is applied to the signal.
				signal *= weight;

				// Add the signal to the output value.
				value += signal;

				// update the (monotonically decreasing) weighting value
				weight *= Gain * signal;

				// Go to the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
				z *= Lacunarity;
			}

			//take care of remainder in OctaveCount
			var remainder = OctaveCount - (int) OctaveCount;

			if (remainder > 0.0f)
			{
				signal = Primitive3D.GetValue(x, y, z);
				signal *= SpectralWeights[curOctave];
				signal *= remainder;
				value += signal;
			}

			return value;
		}
	}
}