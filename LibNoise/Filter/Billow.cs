//
// The following code is based on Ken Musgrave's explanations and sample
// source code in the book "Texturing and Modelling: A procedural approach"

namespace LibNoise.Filter
{
    /// <summary>
    ///     Noise module that outputs three-dimensional "billowy" noise
    ///     Hit snoise is also known as Turbulence fBM and generates "billowy"
    ///     noise suitable for clouds and rocks.
    ///     This noise module is nearly identical to SumFractal except
    ///     this noise module modifies each octave with an absolute-value
    ///     function. Optionally, a scaling factor and a bias addition can be applied
    ///     each octave.
    ///     The original noise::module::billow has scale of 2 and a bias of -1.
    /// </summary>
    public class Billow : FilterModule, IModule3D, IModule2D
	{
		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y)
		{
			float signal;
			float value;
			int curOctave;

			x *= Frequency;
			y *= Frequency;

			// Initialize value, fBM starts with 0
			value = 0;

			// Inner loop of spectral construction, where the fractal is built

			for (curOctave = 0; curOctave < OctaveCount; curOctave++)
			{
				// Get the coherent-noise value.
				signal = Primitive2D.GetValue(x, y) * SpectralWeights[curOctave];

				if (signal < 0.0f)
					signal = -signal;

				// Add the signal to the output value.
				value += signal * PScale + PBias;

				// Go to the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
			}

			//take care of remainder in OctaveCount
			var remainder = OctaveCount - (int) OctaveCount;
			if (remainder > 0)
				value += PScale * remainder * Primitive2D.GetValue(x, y) * SpectralWeights[curOctave] + PBias;

			return value;
		}


		
	    public float GetValue(float x, float y, float z)
		{
			int curOctave;

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

				if (signal < 0.0f)
					signal = -signal;

				// Add the signal to the output value.
				value += signal * PScale + PBias;

				// Go to the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
				z *= Lacunarity;
			}

			//take care of remainder in OctaveCount
			var remainder = OctaveCount - (int) OctaveCount;
			if (remainder > 0.0f)
				value += PScale * remainder * Primitive3D.GetValue(x, y, z) * SpectralWeights[curOctave] + PBias;

			return value;
		}

		/// <summary>
	    ///     Default scale
	    ///     noise module.
	    /// </summary>
	    public const float DefaultScale = 1.0f;

	    /// <summary>
	    ///     Default bias
	    ///     noise module.
	    /// </summary>
	    public const float DefaultBias = 0.0f;

		/// <summary>
	    ///     the bias to apply to the scaled output value from the source module.
	    /// </summary>
	    protected float PBias = DefaultBias;

	    /// <summary>
	    ///     the scaling factor to apply to the output value from the source module.
	    /// </summary>
	    protected float PScale = DefaultScale;

		/// <summary>
	    ///     Gets or sets the scale value.
	    /// </summary>
	    public float Scale
		{
			get => PScale;
			set => PScale = value;
		}

	    /// <summary>
	    ///     Gets or sets the bias value.
	    /// </summary>
	    public float Bias
		{
			get => PBias;
			set => PBias = value;
		}
	}
}