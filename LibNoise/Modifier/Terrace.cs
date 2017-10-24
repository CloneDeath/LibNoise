using System;
using System.Collections.Generic;

namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that maps the output value from a source module onto a terrace-forming curve.
    /// </summary>
    public class Terrace : IModule3D
	{
		protected IModule3D Source { get; set; }
		
		/// <summary>
	    ///     Enables or disables the inversion of the terrace-forming curve between the control points.
	    /// </summary>
	    public bool Invert { get; set; }

	    public float GetValue(float x, float y, float z)
		{
			// Get the output value from the source module.
			var sourceModuleValue = Source.GetValue(x, y, z);

			// Find the first element in the control point array that has a value
			// larger than the output value from the source module.
			int indexPos;
			for (indexPos = 0; indexPos < ControlPoints.Count; indexPos++)
				if (sourceModuleValue < ControlPoints[indexPos])
					break;

			// Find the two nearest control points so that we can map their values
			// onto a quadratic curve.
			var index0 = Libnoise.Clamp(indexPos - 1, 0, ControlPoints.Count - 1);
			var index1 = Libnoise.Clamp(indexPos, 0, ControlPoints.Count - 1);

			// If some control points are missing (which occurs if the output value from
			// the source module is greater than the largest value or less than the
			// smallest value of the control point array), get the value of the nearest
			// control point and exit now.
			if (index0 == index1)
				return ControlPoints[index1];

			// Compute the alpha value used for linear interpolation.
			var value0 = ControlPoints[index0];
			var value1 = ControlPoints[index1];
			var alpha = (sourceModuleValue - value0) / (value1 - value0);

			if (Invert)
			{
				alpha = 1.0f - alpha;
				Libnoise.SwapValues(ref value0, ref value1);
			}

			// Squaring the alpha produces the terrace effect.
			alpha *= alpha;

			// Now perform the linear interpolation given the alpha value.
			return Libnoise.Lerp(value0, value1, alpha);
		}

	    protected void SortControlPoints()
		{
			ControlPoints.Sort(delegate(float p1, float p2)
			{
				if (p1 > p2)
					return 1;
				if (p1 < p2)
					return -1;
				return 0;
			});
		}
		
	    protected readonly List<float> ControlPoints = new List<float>(2);

		public Terrace(IModule3D source)
		{
			Source = source;
		}

		/// <summary>
	    ///     Adds a control point to the curve.
	    ///     No two control points have the same input value.
	    ///     @throw System.ArgumentException if two control points have the same input value.
	    ///     It does not matter which order these points are added.
	    /// </summary>
	    /// <param name="input">The input value stored in the control point.</param>
	    public void AddControlPoint(float input)
		{
			if (ControlPoints.Contains(input))
			{
				throw new ArgumentException(
					string.Format(
						"Cannont insert ControlPoint({0}) : Each control point is required to contain a unique input value",
						input));
			}
			ControlPoints.Add(input);
			SortControlPoints();
		}


	    /// <summary>
	    ///     Return the size of the ControlPoint list
	    /// </summary>
	    /// <returns>The number of ControlPoint in the list</returns>
	    public int CountControlPoints()
		{
			return ControlPoints.Count;
		}


	    /// <summary>
	    ///     Returns a read-only IList&lt;ControlPoint&gt; wrapper for the current ControlPoint list.
	    /// </summary>
	    /// <returns>The read only list</returns>
	    public IList<float> GetControlPoints()
		{
			return ControlPoints.AsReadOnly();
		}


	    /// <summary>
	    ///     Deletes all the control points on the curve.
	    /// </summary>
	    public void ClearControlPoints()
		{
			ControlPoints.Clear();
		}


	    /// <summary>
	    ///     Creates a number of equally-spaced control points that range from
	    ///     -1 to +1.
	    ///     The number of control points must be greater than or equal to 2
	    ///     The previous control points on the terrace-forming curve are deleted.
	    ///     Two or more control points define the terrace-forming curve.  The
	    ///     start of this curve has a slope of zero; its slope then smoothly
	    ///     increases.  At the control points, its slope resets to zero.
	    ///     @throw ArgumentException if an invalid parameter was
	    ///     specified
	    /// </summary>
	    /// <param name="controlPointCount">The number of control points to generate.</param>
	    public void MakeControlPoints(int controlPointCount)
		{
			if (controlPointCount < 2)
				throw new ArgumentException("Two or more control points must be specified.");

			ClearControlPoints();

			var terraceStep = 2.0f / (controlPointCount - 1.0f);
			var curValue = -1.0f;
			for (var i = 0; i < controlPointCount; i++)
			{
				AddControlPoint(curValue);
				curValue += terraceStep;
			}
		}
	}
}