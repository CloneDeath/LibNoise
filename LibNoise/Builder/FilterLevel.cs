namespace LibNoise.Builder
{
	/// <summary>
	///     Filter level.
	/// </summary>
	public enum FilterLevel
	{
		/// <summary>
		///     Caller should use Constant property.
		/// </summary>
		Constant,

		/// <summary>
		///     Caller should use source module value.
		/// </summary>
		Source,

		/// <summary>
		///     Caller should use FilterValue method.
		/// </summary>
		Filter
	}
}