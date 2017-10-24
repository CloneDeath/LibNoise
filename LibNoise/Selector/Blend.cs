namespace LibNoise.Modifier
{
    /// <summary>
    ///     Noise module that outputs a weighted blend of the output values from
    ///     two source modules given the output value supplied by a control module.
    ///     - LeftModule outputs one of the values to blend.
    ///     - RightModule outputs one of the values to blend.
    ///     - ControlModule is known as the
    ///     <i>
    ///         control
    ///         module
    ///     </i>
    ///     .  The control module determines the weight of the
    ///     blending operation.  Negative values weigh the blend towards the
    ///     output value from the LeftModule.
    ///     Positive values weigh the blend towards the output value from the
    ///     ReftModule.
    ///     This noise module uses linear interpolation to perform the blending
    ///     operation.
    /// </summary>
    public class Blend : SelectorModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			var v0 = ((IModule3D) _leftModule).GetValue(x, y, z);
			var v1 = ((IModule3D) _rightModule).GetValue(x, y, z);
			var alpha = (((IModule3D) _controlModule).GetValue(x, y, z) + 1.0f) / 2.0f;
			return Libnoise.Lerp(v0, v1, alpha);
		}

		/// <summary>
	    ///     The control module
	    /// </summary>
	    protected IModule _controlModule;

	    /// <summary>
	    ///     The left input module
	    /// </summary>
	    protected IModule _leftModule;

	    /// <summary>
	    ///     The right input module
	    /// </summary>
	    protected IModule _rightModule;

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
	    public IModule ControlModule
		{
			get => _controlModule;
			set => _controlModule = value;
		}

		public Blend()
		{
		}


		public Blend(IModule controlModule, IModule rightModule, IModule leftModule)
		{
			_controlModule = controlModule;
			_leftModule = leftModule;
			_rightModule = rightModule;
		}
	}
}