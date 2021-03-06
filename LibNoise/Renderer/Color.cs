﻿using System;

namespace LibNoise.Renderer
{
    /// <summary>
    ///     Defines a color.
    ///     A color object contains four 8-bit channels: red, green, blue, and an
    ///     alpha (transparency) channel.  Channel values range from 0 to 255.
    ///     The alpha channel defines the transparency of the color.  If the alpha
    ///     channel has a value of 0, the color is completely transparent.  If the
    ///     alpha channel has a value of 255, the color is completely opaque.
    /// </summary>
    public class Color : IEquatable<Color>, IColor
	{
		/// <summary>
	    /// </summary>
	    /// <param name="other"></param>
	    /// <returns></returns>
	    public bool Equals(Color other)
		{
			return _red == other.Red
			       && _green == other.Green
			       && _blue == other.Blue
			       && _alpha == other.Alpha;
		}

		/// <summary>
	    ///     Compute a grayscale value from the source color.
	    /// </summary>
	    /// <param name="color">The source color.</param>
	    /// <returns>The computed channel value.</returns>
	    public delegate byte GrayscaleStrategy(IColor color);

		/// <summary>
	    ///     Static Random generator.
	    /// </summary>
	    private static readonly Random Rnd = new Random(666);

	    /// <summary>
	    ///     Internal hashcode
	    /// </summary>
	    private readonly int _hashcode;

	    /// <summary>
	    ///     Value of the alpha (transparency) channel.
	    /// </summary>
	    private byte _alpha = 255;

	    /// <summary>
	    ///     Value of the blue channel.
	    /// </summary>
	    private byte _blue;

	    /// <summary>
	    ///     Value of the green channel.
	    /// </summary>
	    private byte _green;

	    /// <summary>
	    ///     Value of the red channel.
	    /// </summary>
	    private byte _red;

		/// <summary>
	    ///     The red channel.
	    /// </summary>
	    public byte Red
		{
			get => _red;
			set => _red = value;
		}

	    /// <summary>
	    ///     The green channel.
	    /// </summary>
	    public byte Green
		{
			get => _green;
			set => _green = value;
		}

	    /// <summary>
	    ///     The blue channel.
	    /// </summary>
	    public byte Blue
		{
			get => _blue;
			set => _blue = value;
		}

	    /// <summary>
	    ///     The alpha channel.
	    /// </summary>
	    public byte Alpha
		{
			get => _alpha;
			set => _alpha = value;
		}

		/// <summary>
	    ///     0-args constructor, a solid black color (0, 0, 0, 255).
	    /// </summary>
	    public Color()
		{
			_hashcode = (_red + _green + _blue) ^ Rnd.Next();
		}

	    /// <summary>
	    ///     Create a new Color.
	    /// </summary>
	    /// <param name="r">Value of the red channel.</param>
	    /// <param name="g">Value of the green channel.</param>
	    /// <param name="b">Value of the blue channel.</param>
	    /// <param name="a">Value of the alpha channel.</param>
	    public Color(byte r, byte g, byte b, byte a)
			: this()
		{
			_red = r;
			_green = g;
			_blue = b;
			_alpha = a;
		}

	    /// <summary>
	    ///     Create a new Color.
	    /// </summary>
	    /// <param name="r">Value of the red channel.</param>
	    /// <param name="g">Value of the green channel.</param>
	    /// <param name="b">Value of the blue channel.</param>
	    public Color(byte r, byte g, byte b)
			: this()
		{
			_red = r;
			_green = g;
			_blue = b;
			_alpha = 255;
		}

		/// <summary>
	    ///     Performs linear interpolation between two colors only with rgb channels.
	    /// </summary>
	    /// <param name="color0">The first color.</param>
	    /// <param name="color1">The second color.</param>
	    /// <param name="t">the amount to interpolate between the two colors.</param>
	    /// <param name="withAlphaChannel">Flag indicates if this method also interpolate alpha channel.</param>
	    /// <returns>The interpolated color, with the same type of color0.</returns>
	    public static IColor Lerp(IColor color0, IColor color1, float t, bool withAlphaChannel = true)
		{
			var color = (IColor) Activator.CreateInstance(color0.GetType());
			color.Red = Libnoise.Lerp(color0.Red, color1.Red, t);
			color.Green = Libnoise.Lerp(color0.Green, color1.Green, t);
			color.Blue = Libnoise.Lerp(color0.Blue, color1.Blue, t);
			color.Alpha = withAlphaChannel ? Libnoise.Lerp(color0.Alpha, color1.Alpha, t) : (byte) 255;

			return color;
		}

	    /// <summary>
	    ///     Performs linear interpolation between two colors, including alpha channel.
	    /// </summary>
	    /// <param name="color0">The first color.</param>
	    /// <param name="color1">The second color.</param>
	    /// <param name="t">the amount to interpolate between the two colors.</param>
	    /// <returns>The interpolated color.</returns>
	    public static IColor Lerp32(IColor color0, IColor color1, float t)
		{
			return Lerp(color0, color1, t, true);
		}

	    /// <summary>
	    ///     Performs linear interpolation between two colors only with rgb channels.
	    /// </summary>
	    /// <param name="color0">The first color.</param>
	    /// <param name="color1">The second color.</param>
	    /// <param name="t">The amount to interpolate between the two colors.</param>
	    /// <returns>The interpolated color.</returns>
	    public static IColor Lerp24(IColor color0, IColor color1, float t)
		{
			return Lerp(color0, color1, t, false);
		}

	    /// <summary>
	    ///     Compute a grayscale value from the source color using Color.GrayscaleLuminosityStrategy.
	    /// </summary>
	    /// <param name="color">The source color.</param>
	    /// <returns>The grayscale color with the same type as color.</returns>
	    public static IColor Grayscale(IColor color)
		{
			var returnColor = (IColor) Activator.CreateInstance(color.GetType());
			returnColor.Red = returnColor.Green = returnColor.Blue = GrayscaleLuminosityStrategy(color);
			returnColor.Alpha = 255;
			return returnColor;
		}

	    /// <summary>
	    ///     Compute a grayscale value from the source color using the given Strategy.
	    /// </summary>
	    /// <param name="color">The source color.</param>
	    /// <param name="strategy">The grayscale strategy.</param>
	    /// <returns>The grayscale color with the same type as color.</returns>
	    public static IColor Grayscale(IColor color, GrayscaleStrategy strategy)
		{
			var returnColor = (IColor) Activator.CreateInstance(color.GetType());
			var val = strategy(color);
			returnColor.Red = returnColor.Green = returnColor.Blue = val;
			returnColor.Alpha = 255;
			return returnColor;
		}

	    /// <summary>
	    ///     A GrayscaleStrategy implementation
	    ///     The lightness strategy averages the most prominent and least prominent colors:
	    ///     (max(R, G, B) + min(R, G, B)) / 2.
	    /// </summary>
	    /// <param name="color">The source color.</param>
	    /// <returns>The computed channel value.</returns>
	    public static byte GrayscaleLightnessStrategy(IColor color)
		{
			return (byte) (
				(
					Math.Max(color.Red, Math.Max(color.Green, color.Blue)) +
					Math.Min(color.Red, Math.Max(color.Green, color.Blue))
				) / 2
			);
		}

	    /// <summary>
	    ///     A GrayscaleStrategy implementation
	    ///     The average strategy simply averages the values: (R + G + B) / 3.
	    /// </summary>
	    /// <param name="color">The source color.</param>
	    /// <returns>the computed channel value.</returns>
	    public static byte GrayscaleAverageStrategy(IColor color)
		{
			return (byte) ((color.Red + color.Green + color.Blue) / 3);
		}

	    /// <summary>
	    ///     A GrayscaleStrategy implementation
	    ///     The luminosity strategy averages the values, but it forms a weighted average to account
	    ///     for human perception. We’re more sensitive to green than other colors,
	    ///     so green is weighted most heavily.
	    ///     The formula for luminosity is 0.21f *R + 0.71f *G + 0.07f *B.
	    /// </summary>
	    /// <param name="color">The source color.</param>
	    /// <returns>The computed channel value.</returns>
	    public static byte GrayscaleLuminosityStrategy(IColor color)
		{
			return (byte) (0.21f * color.Red + 0.71f * color.Green + 0.07f * color.Blue);
		}

		/// <summary>
	    /// </summary>
	    /// <returns></returns>
	    public override string ToString()
		{
			return string.Format("Color({0},{1},{2},{3})", Red, Green, Blue, Alpha);
		}

	    /// <summary>
	    /// </summary>
	    /// <param name="other"></param>
	    /// <returns></returns>
	    public override bool Equals(object other)
		{
			if (other is IColor)
				return
					_red == ((IColor) other).Red
					&& _green == ((IColor) other).Green
					&& _blue == ((IColor) other).Blue
					&& _alpha == ((IColor) other).Alpha;
			return false;
		}

	    /// <summary>
	    /// </summary>
	    /// <returns></returns>
	    public override int GetHashCode()
		{
			return _hashcode;
		}

	    /// <summary>
	    ///     Overloading '==' operator:
	    /// </summary>
	    /// <param name="a"></param>
	    /// <param name="b"></param>
	    /// <returns></returns>
	    public static bool operator ==(Color a, IColor b)
		{
			return a.Equals(b);
		}

	    /// <summary>
	    ///     Overloading '!=' operator:
	    /// </summary>
	    /// <param name="a"></param>
	    /// <param name="b"></param>
	    /// <returns></returns>
	    public static bool operator !=(Color a, IColor b)
		{
			return !a.Equals(b);
		}

	    /// <summary>
	    ///     Overloading '>' operator:
	    /// </summary>
	    /// <param name="a"></param>
	    /// <param name="b"></param>
	    /// <returns></returns>
	    public static bool operator >(Color a, IColor b)
		{
			return
				a._red > b.Red
				&& a._green > b.Green
				&& a._blue > b.Blue
				&& a._alpha > b.Alpha;
		}

	    /// <summary>
	    ///     Overloading '<' operator:
	    /// </summary>
	    /// <param name="a"></param>
	    /// <param name="b"></param>
	    /// <returns></returns>
	    public static bool operator <(Color a, IColor b)
		{
			return
				a._red < b.Red
				&& a._green < b.Green
				&& a._blue < b.Blue
				&& a._alpha < b.Alpha;
		}

	    /// <summary>
	    ///     Overloading '>=' operator:
	    /// </summary>
	    /// <param name="a"></param>
	    /// <param name="b"></param>
	    /// <returns></returns>
	    public static bool operator >=(Color a, IColor b)
		{
			return a > b || a == b;
		}

	    /// <summary>
	    ///     Overloading '<=' operator:
	    /// </summary>
	    /// <param name="a"></param>
	    /// <param name="b"></param>
	    /// <returns></returns>
	    public static bool operator <=(Color a, IColor b)
		{
			return a < b || a == b;
		}
	}
}