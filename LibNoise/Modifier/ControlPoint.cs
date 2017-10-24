using System;

namespace LibNoise.Modifier
{
    /// <summary>
    ///     This structure defines a control point.
    ///     Control points are used for defining splines.
    /// </summary>
    public struct ControlPoint : IEquatable<ControlPoint>
	{
		/// <summary>
	    ///     The input value stored in the control point
	    /// </summary>
	    public float Input { get; }

	    /// <summary>
	    ///     The output value stored in the control point
	    /// </summary>
	    public float Output { get; }

		/// <summary>
	    ///     Create a new ControlPoint with given values
	    /// </summary>
	    /// <param name="input">The input value stored in the control point</param>
	    /// <param name="output">The output value stored in the control point</param>
	    public ControlPoint(float input, float output)
		{
			Input = input;
			Output = output;
		}

		public bool Equals(ControlPoint other)
		{
			return Input == other.Input;
		}
	}
}