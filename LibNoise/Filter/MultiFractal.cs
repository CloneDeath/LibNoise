//
// The following code is based on Ken Musgrave's explanations and sample
// source code in the book "Texturing and Modelling: A procedural approach"

namespace LibNoise.Filter
{
    /// <summary>
    ///     Noise module that outputs 3-dimensional MultiFractal noise.
    ///     The multifractal algorithm differs from the Fractal brownian motion in that perturbations are combined
    ///     multiplicatively and introduces an offset parameter. The perturbation at each frequency is computed as
    ///     in the fBM algorithm, but offset is finally added to the value.
    ///     The role of offset is to emphasize the final perturbation value.
    ///     Multiplicative combination of perturbation, in turn, emphasizes the "mountain-like-aspect" of the landscape,
    ///     so that between mountains a sort of slopes are generated
    ///     (From http://meshlab.sourceforge.net/wiki/index.php/Fractal_Creation )
    /// </summary>
    public class MultiFractal : FilterModule, IModule3D, IModule2D
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

			// Initialize value
			var value = 1.0f;

			// inner loop of spectral construction, where the fractal is built
			for (curOctave = 0; curOctave <  OctaveCount; curOctave++)
			{
				// Get the coherent-noise value.
				var signal = Offset + Primitive2D.GetValue(x, y) * SpectralWeights[curOctave];

				// Add the signal to the output value.
				value *= signal;

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
			int curOctave;

			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			// Initialize value
			var value = 1.0f;

			// inner loop of spectral construction, where the fractal is built
			for (curOctave = 0; curOctave < OctaveCount; curOctave++)
			{
				// Get the coherent-noise value.
				var signal = Offset + Primitive3D.GetValue(x, y, z) * SpectralWeights[curOctave];

				// Add the signal to the output value.
				value *= signal;

				// Go to the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
				z *= Lacunarity;
			}

			//take care of remainder in OctaveCount
			var remainder = OctaveCount - (int) OctaveCount;

			if (remainder > 0.0f)
				value += remainder * Primitive2D.GetValue(x, y) * SpectralWeights[curOctave];

			return value;
		}
	}
}