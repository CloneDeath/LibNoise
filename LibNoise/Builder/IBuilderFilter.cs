namespace LibNoise.Builder
{
	/// <summary>
	///     Interface for builder filter.
	/// </summary>
	public interface IBuilderFilter
	{
		/// <summary>
		///     Gets or sets constant value.
		/// </summary>
		float ConstantValue { get; set; }

		/// <summary>Filter value.</summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="source">The source.</param>
		/// <returns>Filtered value.</returns>
		float FilterValue(int x, int y, float source);

		/// <summary>Is filtered.</summary>
		/// <param name="x">The X.</param>
		/// <param name="y">The Y.</param>
		/// <returns>Filter level.</returns>
		FilterLevel IsFiltered(int x, int y);
	}
}