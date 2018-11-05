using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grammar;
using math_lib;
using static math_lib.Derivative;
using static math_lib.Int;
using System.ComponentModel;
using static System.Math;

namespace Problem
{
    public enum Mode
    {
        RiemannSum,
        TrapezoidalRule,
        IterativeRule,
    }

    public class InverseProblem : IDataErrorInfo
    {
        public string Error { get { return "Error Text"; } }
        public string this[string property]
        {
            get
            {
                string msg = null;
                void add_msg(string added_msg)
                {
                    if (added_msg != null)
                    {
                        if (msg != null)
                        {
                            msg += '\n' + added_msg;
                        }
                        else
                        {
                            msg = added_msg;
                        }
                    }

                }

                switch (property)
                {
                    case "T0":
                        if (T0 > T1)
                        {
                            add_msg("t0 must be less then t1");
                        }
                        if (T0 < 0)
                        {
                            add_msg("t0 must be positive");
                        }
                        break;
                    case "T1":
                        if (T1 < 0)
                        {
                            add_msg("t1 must be positive");
                        }
                        break;
                    case "GridSpacing":
                        if (GridSpacing <= 0)
                        {
                            add_msg("Grid spacing must be greater then 0");
                        }
                        break;
                    case "F1Inpit":
                        if (F1Inpit == null)
                        {
                            add_msg("f1(x) input must not be empty");
                            break;
                        }

                        if (F1X.IsError)
                        {
                            add_msg(F1X.LexerErrorMsg);
                            add_msg(F1X.SyntaxErrorMsg);
                        }
                        break;
                    case "F2Inpit":
                        if (F2Inpit == null)
                        {
                            add_msg("f2(x) input must not be empty");
                            break;
                        }

                        if (F2X.IsError)
                        {
                            add_msg(F2X.LexerErrorMsg);
                            add_msg(F2X.SyntaxErrorMsg);
                        }
                        break;
                    case "P1Inpit":
                        if (P1Inpit == null)
                        {
                            add_msg("p1(t) input must not be empty");
                            break;
                        }

                        if (P1T.IsError)
                        {
                            add_msg(P1T.LexerErrorMsg);
                            add_msg(P1T.SyntaxErrorMsg);
                        }
                        break;
                    case "P2Inpit":
                        if (P2Inpit == null)
                        {
                            add_msg("p2(t) input must not be empty");
                            break;
                        }

                        if (P2T.IsError)
                        {
                            add_msg(P2T.LexerErrorMsg);
                            add_msg(P2T.SyntaxErrorMsg);
                        }
                        break;
                    default:
                        break;
                }
                return msg;
            }
        }

        double h = 0.01;
        public double GridSpacing
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
                task.GridSpacing = h;
            }
        }

        double t0;
        double t1;

        public double T0
        {
            get
            {
                return t0;
            }
            set
            {
                t0 = value;
                task.SetTimeRange(t0, t1);
            }
        }

        public double T1
        {
            get
            {
                return t1;
            }
            set
            {
                t1 = value;
                task.SetTimeRange(t0, t1);
            }
        }

        public double X1
        {
            get;
            set;
        }

        public double X2
        {
            get;
            set;
        }

        public double A
        {
            get;
            set;
        }

        FunctionGrammar F1X = new FunctionGrammar();

        public string F1Inpit
        {
            get
            {
                return F1X.Input;
            }
            set
            {
                F1X.Input = value;
            }
        }

        public double F1(double x)
        {
            return F1X.Func(x);
        }

        FunctionGrammar F2X = new FunctionGrammar();

        public string F2Inpit
        {
            get
            {
                return F2X.Input;
            }
            set
            {
                F2X.Input = value;
            }
        }

        public double F2(double x)
        {
            return F2X.Func(x);
        }

        FunctionGrammar P1T = new FunctionGrammar();

        public string P1Inpit
        {
            get
            {
                return P1T.Input;
            }
            set
            {
                P1T.Input = value;
            }
        }

        public double P1(double t)
        {
            return P1T.Func(t);
        }

        FunctionGrammar P2T = new FunctionGrammar();

        public string P2Inpit
        {
            get
            {
                return P2T.Input;
            }
            set
            {
                P2T.Input = value;
            }
        }

        public double P2(double t)
        {
            return P2T.Func(t);
        }

        Volter2System task = new Volter2System();

        public void Solve(out List<double> g1, out List<double> g2, out double[] t_arr, Mode m)
        {
          
            task.Phi1 = (t) =>
            {
                return -Pow(t,3.0f)/ 6.0f - Pow(t, 2.0f) / 2.0f;
            };


            task.K11 = (t, tau) =>
            {
                return t-tau;
            };

            task.K12 = (t, tau) =>
            {
                return 1.0f;
            };

            task.Phi2 = (t) =>
            {
                //return t+1-5/3*Pow(t,3)-3/2*Pow(t,2);
                return -13.0f / 6.0f * Pow(t, 3.0f) - Pow(t, 2.0f) + t + 1.0f;
            };

            task.K21 = (t, tau) =>
            {
                return 2.0f * (t+tau);
            };

            task.K22 = (t, tau) =>
            {
                return t;
            };

            switch (m)
            {
                case Mode.RiemannSum:
                    task.Solve_1(out g1, out g2, out t_arr);
                    break;
                case Mode.TrapezoidalRule:
                    task.Solve_2(out g1, out g2, out t_arr);
                    break;
                case Mode.IterativeRule:
                    task.Solve_3(out g1, out g2, out t_arr);
                    break;
                default:
                    g1 = new List<double>();
                    g2 = new List<double>();
                    t_arr = new double[0];
                    break;
            } 
        }
    }
}
