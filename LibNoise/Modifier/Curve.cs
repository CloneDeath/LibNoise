using System;
using System.Collections.Generic;

namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that maps the output value from a source module onto an arbitrary function curve.
    /// </summary>
    public class Curve : IModule3D
	{
		protected IModule3D Source { get; set; }
	    protected readonly List<ControlPoint> ControlPoints = new List<ControlPoint>(4);

	    public float GetValue(float x, float y, float z)
		{
			// Get the output value from the source module.
			var sourceModuleValue = Source.GetValue(x, y, z);

			// Find the first element in the control point array that has an input value
			// larger than the output value from the source module.
			int indexPos;
			for (indexPos = 0; indexPos < ControlPoints.Count; indexPos++)
				if (sourceModuleValue < ControlPoints[indexPos].Input)
					break;

			// Find the four nearest control points so that we can perform cubic
			// interpolation.
			var index0 = Libnoise.Clamp(indexPos - 2, 0, ControlPoints.Count - 1);
			var index1 = Libnoise.Clamp(indexPos - 1, 0, ControlPoints.Count - 1);
			var index2 = Libnoise.Clamp(indexPos, 0, ControlPoints.Count - 1);
			var index3 = Libnoise.Clamp(indexPos + 1, 0, ControlPoints.Count - 1);

			// If some control points are missing (which occurs if the value from the
			// source module is greater than the largest input value or less than the
			// smallest input value of the control point array), get the corresponding
			// output value of the nearest control point and exit now.
			if (index1 == index2)
				return ControlPoints[index1].Output;

			// Compute the alpha value used for cubic interpolation.
			var input0 = ControlPoints[index1].Input;
			var input1 = ControlPoints[index2].Input;
			var alpha = (sourceModuleValue - input0) / (input1 - input0);

			// Now perform the cubic interpolation given the alpha value.
			return Libnoise.Cerp(
				ControlPoints[index0].Output,
				ControlPoints[index1].Output,
				ControlPoints[index2].Output,
				ControlPoints[index3].Output,
				alpha);
		}

	    protected void SortControlPoints()
		{
			ControlPoints.Sort(delegate(ControlPoint p1, ControlPoint p2)
			{
				if (p1.Input > p2.Input)
					return 1;
				if (p1.Input < p2.Input)
					return -1;
				return 0;
			});
		}

		public Curve(IModule3D source)
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
	    /// <param name="output">The output value stored in the control point.</param>
	    public void AddControlPoint(float input, float output)
		{
			AddControlPoint(new ControlPoint(input, output));
		}


	    /// <summary>
	    ///     Adds a control point to the curve.
	    ///     No two control points have the same input value.
	    ///     @throw System.ArgumentException if two control points have the same input value.
	    ///     It does not matter which order these points are added.
	    /// </summary>
	    /// <param name="point"></param>
	    public void AddControlPoint(ControlPoint point)
		{
			if (ControlPoints.Contains(point))
			{
				throw new ArgumentException(
					string.Format(
						"Cannont insert ControlPoint({0}, {1}) : Each control point is required to contain a unique input value",
						point.Input, point.Output));
			}
			ControlPoints.Add(point);
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
	    public IList<ControlPoint> GetControlPoints()
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
	}
}