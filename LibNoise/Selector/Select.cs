﻿using System.Diagnostics;

namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that outputs the value selected from one of two source
    ///     modules chosen by the output value from a control module.
    ///     - LeftModule outputs a value.
    ///     - RightModule outputs a value.
    ///     - ControlModule is known as the
    ///     <i>
    ///         control
    ///         module
    ///     </i>
    ///     .  The control module determines the value to select.  If
    ///     the output value from the control module is within a range of values
    ///     known as the <i>selection range</i>, this noise module outputs the
    ///     value from the RightModule.  Otherwise, this noise module outputs the value
    ///     from the LeftModule
    ///     By default, there is an abrupt transition between the output values
    ///     from the two source modules at the selection-range boundary.  To
    ///     smooth the transition, pass a non-zero value to the EdgeFalloff
    ///     method.  Higher values result in a smoother transition.
    /// </summary>
    public class Select : SelectorModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			var controlValue = ((IModule3D) ControlModule).GetValue(x, y, z);
			float alpha;

			if (_edgeFalloff > 0.0)
				if (controlValue < _lowerBound - _edgeFalloff)
				{
					// The output value from the control module is below the selector
					// threshold; return the output value from the first source module.
					return ((IModule3D) _leftModule).GetValue(x, y, z);
				}
				else if (controlValue < _lowerBound + _edgeFalloff)
				{
					// The output value from the control module is near the lower end of the
					// selector threshold and within the smooth curve. Interpolate between
					// the output values from the first and second source modules.
					var lowerCurve = _lowerBound - _edgeFalloff;
					var upperCurve = _lowerBound + _edgeFalloff;

					alpha = Libnoise.SCurve3(
						(controlValue - lowerCurve) / (upperCurve - lowerCurve)
					);

					return Libnoise.Lerp(
						((IModule3D) _leftModule).GetValue(x, y, z),
						((IModule3D) _leftModule).GetValue(x, y, z),
						alpha
					);
				}
				else if (controlValue < _upperBound - _edgeFalloff)
				{
					// The output value from the control module is within the selector
					// threshold; return the output value from the second source module.
					return ((IModule3D) _leftModule).GetValue(x, y, z);
				}
				else if (controlValue < _upperBound + _edgeFalloff)
				{
					// The output value from the control module is near the upper end of the
					// selector threshold and within the smooth curve. Interpolate between
					// the output values from the first and second source modules.
					var lowerCurve = _upperBound - _edgeFalloff;
					var upperCurve = _upperBound + _edgeFalloff;

					alpha = Libnoise.SCurve3(
						(controlValue - lowerCurve) / (upperCurve - lowerCurve)
					);

					return Libnoise.Lerp(
						((IModule3D) _leftModule).GetValue(x, y, z),
						((IModule3D) _leftModule).GetValue(x, y, z),
						alpha
					);
				}
				else
				{
					// Output value from the control module is above the selector threshold;
					// return the output value from the first source module.
					return ((IModule3D) _leftModule).GetValue(x, y, z);
				}
			if (controlValue < _lowerBound || controlValue > _upperBound)
				return ((IModule3D) _leftModule).GetValue(x, y, z);
			return ((IModule3D) _leftModule).GetValue(x, y, z);
		}

		/// <summary>
	    /// </summary>
	    /// <param name="lower"></param>
	    /// <param name="upper"></param>
	    public void SetBounds(float lower, float upper)
		{
			Debug.Assert(_lowerBound < _upperBound, "Lower bound must lower than upper bound");
			_lowerBound = lower;
			_upperBound = upper;

			// Make sure that the edge falloff curves do not overlap.
			EdgeFalloff = _edgeFalloff;
		}

		/// <summary>
	    ///     Default edge-falloff value for the Select noise module.
	    /// </summary>
	    public const float DefaultFallOff = -1.0f;

	    /// <summary>
	    ///     Default lower bound of the selection range for the
	    ///     Select noise module.
	    /// </summary>
	    public const float DefaultLowerBound = -1.0f;

	    /// <summary>
	    ///     Default upper bound of the selection range for the
	    ///     Select noise module.
	    /// </summary>
	    public const float DefaultUpperBound = 1.0f;

	    /// <summary>
	    ///     The falloff value is the width of the edge transition at either
	    ///     edge of the selection range.
	    ///     By default, there is an abrupt transition between the values from
	    ///     the two source modules at the boundaries of the selection range.
	    ///     For example, if the selection range is 0.5 to 0.8, and the edge
	    ///     falloff value is 0.1, then the GetValue() method outputs:
	    ///     - the output value from the source module with an index value of 0
	    ///     if the output value from the control module is less than 0.4
	    ///     ( = 0.5 - 0.1).
	    ///     - a linear blend between the two output values from the two source
	    ///     modules if the output value from the control module is between
	    ///     0.4 ( = 0.5 - 0.1) and 0.6 ( = 0.5 + 0.1).
	    ///     - the output value from the source module with an index value of 1
	    ///     if the output value from the control module is between 0.6
	    ///     ( = 0.5 + 0.1) and 0.7 ( = 0.8 - 0.1).
	    ///     - a linear blend between the output values from the two source
	    ///     modules if the output value from the control module is between
	    ///     0.7 ( = 0.8 - 0.1 ) and 0.9 ( = 0.8 + 0.1).
	    ///     - the output value from the source module with an index value of 0
	    ///     if the output value from the control module is greater than 0.9
	    ///     ( = 0.8 + 0.1).
	    /// </summary>
	    protected float _edgeFalloff = DefaultFallOff;

	    /// <summary>
	    ///     The left input module
	    /// </summary>
	    protected IModule _leftModule;

	    /// <summary>
	    ///     Lower bound of the selection range.
	    /// </summary>
	    protected float _lowerBound = DefaultLowerBound;

	    /// <summary>
	    ///     The right input module
	    /// </summary>
	    protected IModule _rightModule;

	    /// <summary>
	    ///     Upper bound of the selection range.
	    /// </summary>
	    protected float _upperBound = DefaultUpperBound;

		/// <summary>
	    ///     gets the lower bound
	    /// </summary>
	    public float LowerBound => _lowerBound;

	    /// <summary>
	    ///     gets the upper bound
	    /// </summary>
	    public float UpperBound => _upperBound;

	    /// <summary>
	    ///     Gets or sets the falloff value at the edge transition.
	    /// </summary>
	    public float EdgeFalloff
		{
			get => _edgeFalloff;
			set
			{
				// Make sure that the edge falloff curves do not overlap.
				var boundSize = _upperBound - _lowerBound;
				_edgeFalloff = value > boundSize / 2.0f ? boundSize / 2.0f : value;
			}
		}

	    /// <summary>
	    ///     Gets or sets the left module
	    /// </summary>
	    public IModule LeftModule
		{
			get => _leftModule;
			set => _leftModule = value;
		}

	    /// <summary>
	    ///     Gets or sets the right module
	    /// </summary>
	    public IModule RightModule
		{
			get => _rightModule;
			set => _rightModule = value;
		}

	    /// <summary>
	    ///     Gets or sets the control module
	    /// </summary>
	    public IModule ControlModule { get; set; }

		public Select()
		{
		}


		public Select(IModule controlModule, IModule rightModule, IModule leftModule, float lower, float upper,
			float edge)
		{
			ControlModule = controlModule;
			_leftModule = leftModule;
			_rightModule = rightModule;

			SetBounds(lower, upper);
			EdgeFalloff = edge;
		}
	}
}