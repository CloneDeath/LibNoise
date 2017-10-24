namespace LibNoise.Model
{
    /// <summary>
    ///     Model that defines the displacement of a line segment.
    ///     This model returns an output value from a noise module given the
    ///     one-dimensional coordinate of an input value located on a line
    ///     segment, which can be used as displacements.
    ///     This class is useful for creating:
    ///     - roads and rivers
    ///     - disaffected college students
    ///     To generate an output value, pass an input value between 0.0 and 1.0
    ///     to the GetValue() method.  0.0 represents the start position of the
    ///     line segment and 1.0 represents the end position of the line segment.
    /// </summary>
    public class Line : AbstractModel
	{
		/// <summary>
	    ///     Gets or sets a value indicating whether the output value is to be attenuated
	    ///     (moved toward 0.0) as the ends of the line segment are approached.
	    /// </summary>
	    public bool Attenuate
		{
			get => attenuate;

			set => attenuate = value;
		}

		/// <summary>
	    ///     Internal struct that represent a 3D position
	    /// </summary>
	    protected struct Position
		{
			/// <summary>
		    ///     x coordinate of a position.
		    /// </summary>
		    public float x;

		    /// <summary>
		    ///     y coordinate of a position.
		    /// </summary>
		    public float y;

		    /// <summary>
		    ///     z coordinate of a position.
		    /// </summary>
		    public float z;

			/// <summary>Initializes a new instance of the <see cref="Position" /> struct.</summary>
		    /// <param name="x">The x.</param>
		    /// <param name="y">The y.</param>
		    /// <param name="z">The z.</param>
		    public Position(float x, float y, float z)
			{
				this.x = x;
				this.y = y;
				this.z = z;
			}
		}

		/// <summary>
	    ///     A flag indicating that the output value is to be attenuated
	    ///     (moved toward 0.0) as the ends of the line segment are approached.
	    /// </summary>
	    private bool attenuate = true;

	    /// <summary>
	    ///     The position of the end of the line segment.
	    /// </summary>
	    private Position endPosition = new Position(0, 0, 0);

	    /// <summary>
	    ///     The position of the start of the line segment.
	    /// </summary>
	    private Position startPosition = new Position(0, 0, 0);

		/// <summary>
	    ///     Initializes a new instance of the <see cref="Line" /> class.
	    ///     Default constructor
	    /// </summary>
	    public Line()
		{
		}

	    /// <summary>Initializes a new instance of the <see cref="Line" /> class. Constructor</summary>
	    /// <param name="module">The noise module that is used to generate the output values</param>
	    public Line(IModule module)
			: base(module)
		{
		}

		/// <summary>
	    ///     Returns the output value from the noise module given the
	    ///     one-dimensional coordinate of the specified input value located
	    ///     on the line segment. This value may be attenuated (moved toward
	    ///     0.0) as p approaches either end of the line segment; this is
	    ///     the default behavior.
	    ///     If the value is not to be attenuated, p can safely range
	    ///     outside the 0.0 to 1.0 range; the output value will be
	    ///     extrapolated along the line that this segment is part of.
	    /// </summary>
	    /// <param name="p">The distance along the line segment (ranges from 0.0 to 1.0)</param>
	    /// <returns>The output value from the noise module</returns>
	    public float GetValue(float p)
		{
			var x = (endPosition.x - startPosition.x) * p + startPosition.x;
			var y = (endPosition.y - startPosition.y) * p + startPosition.y;
			var z = (endPosition.z - startPosition.z) * p + startPosition.z;

			var value = ((IModule3D) PSourceModule).GetValue(x, y, z);

			if (attenuate)
				return p * (1.0f - p) * 4.0f * value;
			return value;
		}

	    /// <summary>
	    ///     Sets the position ( x, y, z ) of the end of the line
	    ///     segment to choose values along.
	    /// </summary>
	    /// <param name="x">x coordinate of the end position</param>
	    /// <param name="y">y coordinate of the end position</param>
	    /// <param name="z">z coordinate of the end position</param>
	    public void SetEndPoint(float x, float y, float z)
		{
			endPosition.x = x;
			endPosition.y = y;
			endPosition.z = z;
		}

	    /// <summary>
	    ///     Sets the position ( x, y, z ) of the start of the line
	    ///     segment to choose values along.
	    /// </summary>
	    /// <param name="x">x coordinate of the start position</param>
	    /// <param name="y">y coordinate of the start position</param>
	    /// <param name="z">z coordinate of the start position</param>
	    public void SetStartPoint(float x, float y, float z)
		{
			startPosition.x = x;
			startPosition.y = y;
			startPosition.z = z;
		}
	}
}