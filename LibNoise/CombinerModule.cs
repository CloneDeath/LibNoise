namespace LibNoise
{
    /// <summary>
    /// </summary>
    public abstract class CombinerModule : IModule
	{
		#region Fields

	    /// <summary>
	    ///     The left input module
	    /// </summary>
	    protected IModule _leftModule;

	    /// <summary>
	    ///     The right input module
	    /// </summary>
	    protected IModule _rightModule;

		#endregion

		#region Accessors

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

		#endregion

		#region Ctor/Dtor

		public CombinerModule()
		{
		}


		public CombinerModule(IModule left, IModule right)
		{
			_leftModule = left;
			_rightModule = right;
		}

		#endregion
	}
}