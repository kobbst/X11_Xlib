// =====================
// The "Roma Widget Set"
// =====================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: April 2015
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 * In case of problems with .NEt see: .NET Reference Source, http://referencesource-beta.microsoft.com/
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2015 Steffen Ploetz
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// This copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// //////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;

using X11;

namespace Mathematics
{
    /// <summary>Calculate a multi-dimensional (max. 32) bezier curve.</summary>
    /// <remarks>Taken from the article "Bezier Curves Made Simple" by Tolga Birdal at
    /// http://www.codeproject.com/Articles/25237/Bezier-Curves-Made-Simple </remarks>
    public class BezierCurve2D
    {
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "BezierCurve2D";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
        /// <summary>The factorial lookup table.</summary>
        private static double[] _factorialLookup = null;

		#endregion Attributes

        /// <summary>The default constructor.</summary>
        private BezierCurve2D()
        {
        }

        /// <summary>Get the factorial lookup table.</summary>
        private static double[] FactorialLookup
        {   get
            {   if (_factorialLookup == null)
                    CreateFactorialTable();
                return _factorialLookup;
            }
        }

        /// <summary>Get the indicated factorial lookup table entry.</summary>
        private static double Factorial(int n)
        {
            if (n <  0) { throw new Exception("n is less than 0"); }
            if (n > 32) { throw new Exception("n is greater than 32"); }

            return FactorialLookup[n]; /* returns the value n! as a SUMORealing point number */
        }

        /// <summary>Create the lookup table for fast factorial calculation.</summary>
        private static void CreateFactorialTable()
        {
            // fill untill n=32. The rest is too high to represent
            double[] a = new double[33]; 
            a[0] = 1.0;
            a[1] = 1.0;
            a[2] = 2.0;
            a[3] = 6.0;
            a[4] = 24.0;
            a[5] = 120.0;
            a[6] = 720.0;
            a[7] = 5040.0;
            a[8] = 40320.0;
            a[9] = 362880.0;
            a[10] = 3628800.0;
            a[11] = 39916800.0;
            a[12] = 479001600.0;
            a[13] = 6227020800.0;
            a[14] = 87178291200.0;
            a[15] = 1307674368000.0;
            a[16] = 20922789888000.0;
            a[17] = 355687428096000.0;
            a[18] = 6402373705728000.0;
            a[19] = 121645100408832000.0;
            a[20] = 2432902008176640000.0;
            a[21] = 51090942171709440000.0;
            a[22] = 1124000727777607680000.0;
            a[23] = 25852016738884976640000.0;
            a[24] = 620448401733239439360000.0;
            a[25] = 15511210043330985984000000.0;
            a[26] = 403291461126605635584000000.0;
            a[27] = 10888869450418352160768000000.0;
            a[28] = 304888344611713860501504000000.0;
            a[29] = 8841761993739701954543616000000.0;
            a[30] = 265252859812191058636308480000000.0;
            a[31] = 8222838654177922817725562880000000.0;
            a[32] = 263130836933693530167218012160000000.0;
            _factorialLookup = a;
        }

        private static double Ni(int n, int i)
        {
            double ni;
            double a1 = Factorial(n);
            double a2 = Factorial(i);
            double a3 = Factorial(n - i);
            ni =  a1/ (a2 * a3);
            return ni;
        }

        /// <summary>Calculate Bernstein basis.</summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static double Bernstein(int n, int i, double t)
        {
            double basis;
            double ti; /* t^i */
            double tni; /* (1 - t)^i */

            /* Prevent problems with pow */

            if (t == 0.0 && i == 0) 
                ti = 1.0; 
            else 
                ti = Math.Pow(t, i);

            if (n == i && t == 1.0) 
                tni = 1.0; 
            else 
                tni = Math.Pow((1 - t), (n - i));

            //Bernstein basis
            basis = Ni(n, i) * ti * tni; 
            return basis;
        }
		
		/// <summary>Calculate the preferred number of result points.</summary>
		/// <param name="inputPoints">The bezier points [Start][Control1]...[End].<see cref="System.Windows.Point[]"/></param>
        /// <returns>The preferred number of result points.<see cref="System.Int32"/></returns>
		public static int CalculateNumberOfResultPoints (System.Windows.Point[] inputPoints)
		{
            double dX = inputPoints[inputPoints.Length - 1].X - inputPoints[0].X;
            double dY = inputPoints[inputPoints.Length - 1].Y - inputPoints[0].Y;
            double distance = Math.Sqrt(dX * dX + dY * dY);
			
            int resultPointNumber = 2 + (int)(Math.Sqrt(distance));
            resultPointNumber = Math.Min(512, Math.Max(4, resultPointNumber));
			return resultPointNumber;
		}
		
        /// <summary>Calculate the interpolation list for a bezier curve.</summary>
        /// <param name="inputPoints">The bezier points [Start][Control1]...[End].<see cref="System.Windows.Point[]"/></param>
        /// <param name="resultPointNumber">The number of requested result points.<see cref="System.Int32"/></param>
        /// <param name="resultPoints">The calculated result points.<see cref="System.Windows.Point[]"/></param>
        public static void CalculateInterpolationPoints(System.Windows.Point[] inputPoints, int resultPointNumber, System.Windows.Point[] resultPoints)
        {
			if (resultPointNumber <= 0)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CalculateInterpolationPoints () Requires a positive number of result points!");
				return;
			}
			
            int inputPointLength = (inputPoints.Length);
            double step, t;

            t = 0;
            step = (double)1.0 / (resultPointNumber - 1);

            for (int resultPointIndex = 0; resultPointIndex < resultPointNumber; resultPointIndex++)
            { 
                if ((1.0 - t) < 5e-6) 
                    t = 1.0;

                resultPoints[resultPointIndex].X = 0.0;
                resultPoints[resultPointIndex].Y = 0.0;
                for (int i = 0; i < inputPointLength; i++)
                {
                    double basis = Bernstein(inputPointLength - 1, i, t);
                    resultPoints[resultPointIndex].X += basis * inputPoints[i].X;
                    resultPoints[resultPointIndex].Y += basis * inputPoints[i].Y;
                }
                t += step;
            }
        }
    }
}