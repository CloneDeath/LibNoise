namespace LibNoise
{
    /// <summary>
    /// </summary>
    public abstract class CombinerModule : IModule
	{
	    /// <summary>
	    ///     Gets or sets the left module
	    /// </summary>
	    public IModule LeftModule { get; set; }

	    /// <summary>
	    ///     Gets or sets the right module
	    /// </summary>
	    public IModule RightModule { get; set; }

		protected CombinerModule()
		{
		}

		protected CombinerModule(IModule left, IModule right)
		{
			LeftModule = left;
			RightModule = right;
		}
	}
}