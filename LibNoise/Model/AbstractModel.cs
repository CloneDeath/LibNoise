namespace LibNoise.Model
{
    /// <summary>
    ///     Abstract base class for all Model
    ///     Model must defined their own GetValue() method
    /// </summary>
    public class AbstractModel
	{
		/// <summary>
	    ///     The source input module.
	    /// </summary>
	    protected IModule PSourceModule;

		/// <summary>
	    ///     Gets or sets the source module.
	    /// </summary>
	    public IModule SourceModule
		{
			get => PSourceModule;
			set => PSourceModule = value;
		}

		/// <summary>
	    ///     Default constructor
	    /// </summary>
	    public AbstractModel()
		{
		}


	    /// <summary>
	    ///     Constructor
	    /// </summary>
	    /// <param name="module">The noise module that is used to generate the output values</param>
	    public AbstractModel(IModule module)
		{
			PSourceModule = module;
		}
	}
}