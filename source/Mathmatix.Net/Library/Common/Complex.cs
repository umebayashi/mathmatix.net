using System;
using System.Collections.Generic;

namespace Mathmatix.Common
{
	public class Complex<T> : IComparable, IComparable<Complex<T>> where T: IComparable
	{
		protected bool Equals(Complex<T> other)
		{
			return EqualityComparer<T>.Default.Equals(Real, other.Real) && EqualityComparer<T>.Default.Equals(Imaginary, other.Imaginary);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (EqualityComparer<T>.Default.GetHashCode(Real)*397) ^ EqualityComparer<T>.Default.GetHashCode(Imaginary);
			}
		}

		#region constructor

		public Complex() : this(default(T), default(T))
		{
		}

		public Complex(T real, T imaginary)
		{
			Real = real;
			Imaginary = imaginary;
		}

		#endregion

		#region field / property

		/// <summary>
		/// 実数部
		/// </summary>
		public T Real { get; private set; }

		/// <summary>
		/// 虚数部
		/// </summary>
		public T Imaginary { get; private set; }

		#endregion

		#region instance method

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Complex<T>) obj);
		}

		public override string ToString()
		{
			return string.Format("{0} {1}i", Real, Imaginary);
		}

		public int CompareTo(object obj)
		{
			return CompareTo((Complex<T>)obj);
		}

		public int CompareTo(Complex<T> other)
		{
			return !Real.Equals(other.Real) ? Real.CompareTo(other.Real) : Imaginary.CompareTo(other.Imaginary);
		}

		#endregion
	}

	public class ComplexD : Complex<double>
	{
		protected bool Equals(ComplexD other)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() == GetType()) return false;
			return Equals((ComplexD) obj);
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		#region constructor

		public ComplexD() : base() { }

		public ComplexD(double real, double imaginary) : base(real, imaginary) { }

		#endregion

		#region static method

		public static ComplexD Add(ComplexD x, ComplexD y)
		{
			return new ComplexD(x.Real + y.Real, x.Imaginary + y.Imaginary);
		}

		public static ComplexD Subtract(ComplexD x, ComplexD y)
		{
			return new ComplexD(x.Real - y.Real, x.Imaginary - y.Imaginary);
		}

		public static ComplexD Multiply(ComplexD x, ComplexD y)
		{
			return new ComplexD(x.Real * y.Real - x.Imaginary * y.Imaginary, x.Real * y.Imaginary + x.Imaginary * y.Real);
		}

		public static ComplexD Divide(ComplexD x, ComplexD y)
		{
			var real = (x.Real * y.Real + x.Imaginary * y.Imaginary) / (y.Real * y.Real + y.Imaginary * y.Imaginary);
			var imaginary = (x.Imaginary * y.Real - x.Real * y.Imaginary) / (y.Real * y.Real + y.Imaginary * y.Imaginary);
			return new ComplexD(real, imaginary);
		}

		#endregion

		#region operator overload

		public static ComplexD operator +(ComplexD x, ComplexD y)
		{
			return Add(x, y);
		}

		public static ComplexD operator -(ComplexD x, ComplexD y)
		{
			return Subtract(x, y);
		}

		public static ComplexD operator *(ComplexD x, ComplexD y)
		{
			return Multiply(x, y);
		}

		public static ComplexD operator /(ComplexD x, ComplexD y)
		{
			return Divide(x, y);
		}

		public static bool operator ==(ComplexD x, ComplexD y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(ComplexD x, ComplexD y)
		{
			return !x.Equals(y);
		}

		#endregion
	}
}
