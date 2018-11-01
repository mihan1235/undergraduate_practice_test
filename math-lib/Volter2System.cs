using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace math_lib
{
    /*
     * \left\{
     * \begin{aligned}
     * & g_1(t) = \int_0^t K_{11}(t,\tau)g_1(\tau) \diff \tau + \int_0^t K_{12}(t,\tau)
     * g_2(\tau) \diff \tau + \phi_1(t)\\
     * & g_2(t) = \int_0^t K_{21}(t,\tau) g_1(\tau) \diff \tau + \int_0^t K_{22}(t,\tau)
     * g_2(\tau) \diff \tau + \phi_2(t)\\
     * \end{aligned}
     * \right.
     * 
     * K_{11}, K_{22}, K_{21}, K{22}, \phi_1, \phi_2 are given.
     * 
     * we need to find g_1(t),g_2(t)
     * 
     * left rectangle formula:
     * 
     * g_1(t_0) = \phi_1(0)
     * g_2(t_0) = \phi_2(0)
     * 
     * \left\{
     * \begin{aligned}
     * & g_1(t_i) = \int_0^{t_i} K_{11} (t_i, \tau) g_1(\tau) \diff \tau + \int_0^{t_i} 
     * K_{12}(t_i,\tau) g_2(\tau) \diff \tau + \phi_1(t_i)\\
     * & g_2(t_i) = \int_0^{t_i} K_{21} (t_i, \tau) g_1(\tau) \diff \tau + \int_0^{t_i} 
     * K_{22}(t_i,\tau) g_2(\tau) \diff \tau + \phi_2(t_i)\\
     * \end{aligned}
     * \right.
     * 
     * \tiled g_1(t), \tilde g_2(t) are approximated functions
     * 
     * \left\{
     * \begin{aligned}
     * & \tilde g_1(t_i) = \sum_{k=0}^{i-1} K_{11} (t_i, t_k) \tilde g_1(t_k) h + \sum_{k=0}^{i-1} 
     * K_{12}(t_i,t_k) \tilde g_2(t_k) h + \phi_1(t_i)\\
     * & \tilde g_2(t_i) = \sum_{k=0}^{i-1} K_{21} (t_i, t_k) \tilde g_1(t_k) h + \sum_{k=0}^{i-1} 
     * K_{22}(t_i,t_k) \tilde g_2(t_k) h + \phi_2(t_i)\\
     * \end{aligned}
     * \right.
     */

    public class Volter2System
    {
        double h = 0.02f;
        public double GridSpacing
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
            }
        }
        double a = 2.0f;
        double b = 3.0f;
        public void SetTimeRange(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public function2 K11
        {
            get;
            set;
        }

        public function2 K12
        {
            get;
            set;
        }

        public function2 K21
        {
            get;
            set;
        }

        public function2 K22
        {
            get;
            set;
        }

        public function1 Phi1
        {
            get;
            set;
        }

        public function1 Phi2
        {
            get;
            set;
        }


        int num;

        double[] Make_t_Array()
        {
            double[] t = new double[(int)((b - a) / h + 1)];
            //Console.WriteLine(a);
            //Console.WriteLine(b);
            num = (int)((b - a) / h + 1);
            //Console.WriteLine(num);
            double tmp = a;
            for (int i = 0; i < num; i = i + 1)
            {

                t[i] = tmp;
                tmp += h;
            }
            return t;
        }

        public void Solve_1(out List<double> g_1, out List<double> g_2, out double[] t)
        {
            t = Make_t_Array();
            g_1 = new List<double>();
            g_2 = new List<double>();

            g_1.Add(Phi1(t[0]));
            g_2.Add(Phi2(t[0]));

            for (int i = 1; i < num; i = i + 1)
            {
                double sum_11 = 0;

                for (int k = 0; k <= i - 1; k = k + 1)
                {
                    sum_11 += K11(t[i], t[k]) * g_1.ElementAt(k) * h;
                }

                double sum_12 = 0;

                for (int k = 0; k <= i - 1; k++)
                {
                    sum_12 += K12(t[i], t[k]) * g_2.ElementAt(k) * h;
                }

                g_1.Add(sum_11 + sum_12 + Phi1(t[i]));

                double sum_21 = 0;

                for (int k = 0; k <= i - 1; k++)
                {
                    sum_21 += K21(t[i], t[k]) * g_1.ElementAt(k) * h;
                }

                double sum_22 = 0;

                for (int k = 0; k <= i - 1; k++)
                {
                    sum_22 += K22(t[i], t[k]) * g_2.ElementAt(k) * h;
                }

                g_2.Add(sum_21 + sum_22 + Phi2(t[i]));
            }
        }



        public void Solve_2(out List<double> g_1, out List<double> g_2, out double[] t)
        {
            t = Make_t_Array();
            g_1 = new List<double>();
            g_2 = new List<double>();

            double C(int i, ref double[] t_arr)
            {
                return 1 - 1 / 2 * K11(t_arr[i], t_arr[i]) * h;
            }

            double D(int i, ref double[] t_arr)
            {
                return -1 / 2 * K12(t_arr[i], t_arr[i]) * h;
            }

            double E(int i, ref double[] t_arr)
            {
                return 1 - 1 / 2 * K22(t_arr[i], t_arr[i]) * h;
            }

            double F(int i, ref double[] t_arr)
            {
                return -1 / 2 * K21(t_arr[i], t_arr[i]) * h;
            }

            double R1i(int i, ref List<double> g1, ref double[] t_arr)
            {
                double sum = 0;
                for (int k = 0; k <= i - 2; k++)
                {
                    sum += (K11(t_arr[i], t_arr[k]) * g1.ElementAt(k) + K11(t_arr[i], t_arr[k + 1]) * g1.ElementAt(k + 1))*h / 2 +
                        Phi1(t_arr[i]);
                }
                return sum;
            }

            double R2i(int i, ref List<double> g2, ref double[] t_arr)
            {
                double sum = 0;
                for (int k = 0; k <= i - 2; k++)
                {
                    sum += (K22(t_arr[i], t_arr[k]) * g2.ElementAt(k) + K22(t_arr[i], t_arr[k + 1]) * g2.ElementAt(k + 1)) * h / 2 +
                        Phi2(t_arr[i]);
                }
                return sum;
            }

            double Bi1(int i, ref List<double> g2, ref double[] t_arr)
            {
                double sum = 0;
                for (int k = 0; k <= i - 2; k++)
                {
                    sum += (K12(t_arr[i],t_arr[k])*g2.ElementAt(k)+K12(t_arr[i],t_arr[k+1])*g2.ElementAt(k+1))*h/2;
                }
                return sum;
            }

            double Bi2(int i, ref List<double> g1, ref double[] t_arr)
            {
                double sum = 0;
                for (int k = 0; k <= i - 2; k++)
                {
                    sum += (K21(t_arr[i],t_arr[k])*g1.ElementAt(k)+K21(t_arr[i],t_arr[k+1])*g1.ElementAt(k+1))*h/2;
                }
                return sum;
            }

            double G1(int i, ref List<double> g1, ref List<double> g2, ref double[] t_arr)
            {
                return 1 / 2 * K11(t_arr[i], t_arr[i - 1]) * g1.ElementAt(i - 1) * h +
                    1 / 2 * K12(t_arr[i], t_arr[i - 1]) * g2.ElementAt(i - 1) * h +
                    Bi1(i,ref g2,ref t_arr) + R1i(i,ref g1,ref t_arr);
            }

            double G2(int i, ref List<double> g1, ref List<double> g2, ref double[] t_arr)
            {
                return 1 / 2 * K22(t_arr[i], t_arr[i - 1]) * g2.ElementAt(i - 1) * h +
                       1 / 2 * K21(t_arr[i], t_arr[i - 1]) * g1.ElementAt(i - 1) * h +
                       Bi2(i,ref g1,ref t_arr) + R2i(i,ref g2,ref t_arr);

            }

            g_1.Add(1/(C(0, ref t) *E(0, ref t) -F(0, ref t) *D(0, ref t))*
                (Phi1(t[0])*E(0, ref t) - Phi2(t[0])* D(0, ref t)));
            g_2.Add(1/(D(0, ref t) *F(0, ref t) -E(0, ref t) *C(0, ref t))*
                (Phi1(t[0]) * F(0, ref t) - Phi2(t[0]) * C(0, ref t)));
            for (int i = 1; i < num; i = i + 1)
            {
                g_1.Add((G1(i,ref g_1,ref g_2,ref t)*E(i,ref t)-G2(i, ref g_1, ref g_2, ref t) *D(i, ref t))/
                    (C(i, ref t) *E(i, ref t) -F(i, ref t) *D(i, ref t)));
                g_2.Add((G1(i, ref g_1, ref g_2, ref t) *F(i, ref t) -G2(i, ref g_1, ref g_2, ref t) *C(i, ref t))/
                    (D(i, ref t) *F(i, ref t) -E(i, ref t) *C(i, ref t)));
            }
        }


        public void Solve_3(out List<double> g_1, out List<double> g_2, out double[] t)
        {
            t = Make_t_Array();
            g_1 = new List<double>();
            g_2 = new List<double>();

            for (int i = 0; i < num; i = i + 1)
            {

                ///////////////////////////////////////////
                ///Count g_1
                double g_1_old_result;
                double g_2_old_result;
                double g_1_result = Phi1(t[i]);
                double g_2_result = Phi2(t[i]);
                double t_i = t[i];
                double g1_bias;
                double g2_bias;
                do
                {
                    g_1_old_result = g_1_result;
                    g_2_old_result = g_2_result;
                    g_1_result = Int.Integrate((tau) =>
                    {
                        return g_1_old_result * K11(t_i, tau);
                    }, a, b, h) + Int.Integrate((tau) =>
                    {
                        return g_2_old_result * K12(t_i, tau);
                    }, a, b, h) + Phi1(t[i]);
                    g_2_result = Int.Integrate((tau) =>
                    {
                        return g_1_old_result * K21(t_i, tau);
                    }, a, b, h) + Int.Integrate((tau) =>
                    {
                        return g_2_old_result * K22(t_i, tau);
                    }, a, b, h) + Phi2(t[i]);
                    g1_bias = Math.Abs(g_1_result - g_1_old_result);
                    g2_bias = Math.Abs(g_2_result - g_2_old_result);
                } while ((g1_bias > h) || (g2_bias > h));

                g_1.Add(g_1_result);
                g_2.Add(g_2_result);
            }
        }
    }
}
