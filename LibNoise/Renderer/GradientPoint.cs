using System;

namespace LibNoise.Renderer
{
    /// <summary>
    ///     Defines a point used to build a color gradient.
    ///     A color gradient is a list of gradually-changing colors.  A color
    ///     gradient is defined by a list of <i>gradient points</i>.  Each
    ///     gradient point has a position and a color.  In a color gradient, the
    ///     colors between two adjacent gradient points are linearly interpolated.
    ///     The ColorGradient class defines a color gradient by a list of these
    ///     objects.
    /// </summary>
    public struct GradientPoint : IEquatable<GradientPoint>
	{
		#region fields

	    /// <summary>
	    ///     Internal hashcode
	    /// </summary>
	    private readonly int _hashcode;

	    /// <summary>
	    ///     The color of this gradient point.
	    /// </summary>
	    public IColor Color;

	    /// <summary>
	    ///     The position of this gradient point.
	    /// </summary>
	    public float Position;

		#endregion

		#region Ctor/Dtor

		public GradientPoint(float position, IColor color)
		{
			Color = color;
			Position = position;
			_hashcode = (int) Position ^ Color.GetHashCode();
		}

		#endregion

		#region Interface implementation

		public bool Equals(GradientPoint other)
		{
			return Position == other.Position;
		}

		#endregion

		#region Overloading

	    /// <summary>
	    /// </summary>
	    /// <returns></returns>
	    public override int GetHashCode()
		{
			return _hashcode;
		}

		#endregion
	}
}