namespace LibNoise
{
    /// <summary>
    ///     Base class for source module modifiers
    /// </summary>
    public abstract class ModifierModule : IModule
	{
		#region Fields

	    /// <summary>
	    ///     The source input module
	    /// </summary>
	    protected IModule _sourceModule;

		#endregion

		#region Accessors

	    /// <summary>
	    ///     Gets or sets the source module
	    /// </summary>
	    public IModule SourceModule
		{
			get => _sourceModule;
			set => _sourceModule = value;
		}

		#endregion

		#region Ctor/Dtor

	    /// <summary>
	    /// </summary>
	    public ModifierModule()
		{
		}


	    /// <summary>
	    /// </summary>
	    /// <param name="source"></param>
	    public ModifierModule(IModule source)
		{
			_sourceModule = source;
		}

		#endregion
	}
}