namespace LibNoise.Transformer
{
    /// <summary>
    ///     Noise module that uses three source modules to displace each
    ///     coordinate of the input value before returning the output value from
    ///     a source module.
    ///     Roles in the displacement operation:
    ///     - SourceModule outputs a value.
    ///     - XDisplaceModule specifies the offset to
    ///     apply to the x coordinate of the input value.
    ///     - YDisplaceModule specifies the
    ///     offset to apply to the y coordinate of the input value.
    ///     - YDisplaceModule specifies the offset
    ///     to apply to the z coordinate of the input value.
    ///     The GetValue() method modifies the ( x, y, z ) coordinates of
    ///     the input value using the output values from the three displacement
    ///     modules before retrieving the output value from the source module.
    ///     The Turbulence noise module is a special case of the
    ///     displacement module; internally, there are three Perlin-noise modules
    ///     that perform the displacement operation.
    /// </summary>
    public class Displace : TransformerModule, IModule3D
	{
		
	    public float GetValue(float x, float y, float z)
		{
			// Get the output values from the three displacement modules.  Add each
			// value to the corresponding coordinate in the input value.
			var xDisplace = x + ((IModule3D) _xDisplaceModule).GetValue(x, y, z);
			var yDisplace = y + ((IModule3D) _yDisplaceModule).GetValue(x, y, z);
			var zDisplace = z + ((IModule3D) _zDisplaceModule).GetValue(x, y, z);

			// Retrieve the output value using the offsetted input value instead of
			// the original input value.
			return ((IModule3D) _sourceModule).GetValue(xDisplace, yDisplace, zDisplace);
		}

		/// <summary>
	    ///     The source input module
	    /// </summary>
	    protected IModule _sourceModule;

	    /// <summary>
	    ///     Displacement module that displaces the x coordinate.
	    /// </summary>
	    protected IModule _xDisplaceModule;

	    /// <summary>
	    ///     Displacement module that displaces the y coordinate.
	    /// </summary>
	    protected IModule _yDisplaceModule;

	    /// <summary>
	    ///     Displacement module that displaces the z coordinate.
	    /// </summary>
	    protected IModule _zDisplaceModule;

		/// <summary>
	    ///     Gets or sets the source module
	    /// </summary>
	    public IModule SourceModule
		{
			get => _sourceModule;
			set => _sourceModule = value;
		}

	    /// <summary>
	    ///     Gets or sets the displacement module that displaces the x coordinate.
	    /// </summary>
	    public IModule XDisplaceModule
		{
			get => _xDisplaceModule;
			set => _xDisplaceModule = value;
		}

	    /// <summary>
	    ///     Gets or sets the displacement module that displaces the y coordinate.
	    /// </summary>
	    public IModule YDisplaceModule
		{
			get => _yDisplaceModule;
			set => _yDisplaceModule = value;
		}

	    /// <summary>
	    ///     Gets or sets the displacement module that displaces the z coordinate.
	    /// </summary>
	    public IModule ZDisplaceModule
		{
			get => _zDisplaceModule;
			set => _zDisplaceModule = value;
		}

		/// <summary>
	    ///     Create a new noise module with default values
	    /// </summary>
	    public Displace()
		{
		}


	    /// <summary>
	    ///     Create a new noise module with the given values
	    /// </summary>
	    /// <param name="source">the source module</param>
	    /// <param name="xDisplaceModule">the displacement module that displaces the x coordinate</param>
	    /// <param name="yDisplaceModule">the displacement module that displaces the y coordinate</param>
	    /// <param name="zDisplaceModule">the displacement module that displaces the z coordinate</param>
	    public Displace(IModule source, IModule xDisplaceModule, IModule yDisplaceModule, IModule zDisplaceModule)
		{
			_sourceModule = source;
			_xDisplaceModule = xDisplaceModule;
			_yDisplaceModule = yDisplaceModule;
			_zDisplaceModule = zDisplaceModule;
		}
	}
}