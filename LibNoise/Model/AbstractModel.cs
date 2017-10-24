namespace LibNoise.Model
{
    /// <summary>
    /// Abstract base class for all Model
    ///
    /// Model must defined their own GetValue() method
    /// </summary>
    public class AbstractModel
    {
        #region Fields

        /// <summary>
        /// The source input module.
        /// </summary>
        protected IModule PSourceModule;

        #endregion

        #region Accessors

        /// <summary>
        /// Gets or sets the source module.
        /// </summary>
        public IModule SourceModule
        {
            get { return PSourceModule; }
            set { PSourceModule = value; }
        }

        #endregion

        #region Ctor/Dtor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AbstractModel()
        {
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="module">The noise module that is used to generate the output values</param>
        public AbstractModel(IModule module)
        {
            PSourceModule = module;
        }

        #endregion
    }
}
