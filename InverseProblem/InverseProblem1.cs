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

namespace InverseProblem
{
    public class InverseProblem1: IDataErrorInfo
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
                    case "PhiInpit":
                        if (PhiInpit == null)
                        {
                            add_msg("Phi input must not be empty");
                            break;
                        }

                        if (PhiX.IsError)
                        {
                            add_msg(PhiX.LexerErrorMsg);
                            add_msg(PhiX.SyntaxErrorMsg);
                        }
                        break;
                    case "PsiInpit":
                        if (PsiInpit == null)
                        {
                            add_msg("Psi input must not be empty");
                            break;
                        }

                        if (PsiX.IsError)
                        {
                            add_msg(PsiX.LexerErrorMsg);
                            add_msg(PsiX.SyntaxErrorMsg);
                        }
                        break;
                    case "FInpit":
                        if (FInpit == null)
                        {
                            add_msg("f(x) input must not be empty");
                            break;
                        }

                        if (FX.IsError)
                        {
                            add_msg(FX.LexerErrorMsg);
                            add_msg(FX.SyntaxErrorMsg);
                        }
                        break;
                    case "PInpit":
                        if (PInpit == null)
                        {
                            add_msg("p(t) input must not be empty");
                            break;
                        }

                        if (PT.IsError)
                        {
                            add_msg(PT.LexerErrorMsg);
                            add_msg(PT.SyntaxErrorMsg);
                        }
                        break;
                    default:
                        break;
                }
                return msg;
            }
        }

        double h=0.01;
        public double GridSpacing
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
                volter_int.GridSpacing = h;
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
                volter_int.SetTimeRange(t0, t1);                
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
                volter_int.SetTimeRange(t0, t1);                
            }
        }

        public double X0
        {
            get;
            set;
        }

        public double A
        {
            get;
            set;
        }

        FunctionGrammar FX = new FunctionGrammar();
        FunctionGrammar PhiX = new FunctionGrammar();
        FunctionGrammar PsiX = new FunctionGrammar();

        public string FInpit
        {
            get
            {
                return FX.Input;
            }
            set
            {
                FX.Input = value;
            }
        }

        public double F(double x)
        {
            return FX.Func(x);
        }

        public string PhiInpit
        {
            get
            {
                return PhiX.Input;
            }
            set
            {
                PhiX.Input = value;
            }
        }

        public double Phi(double x)
        {
            return PhiX.Func(x);
        }

        public string PsiInpit
        {
            get
            {
                return PsiX.Input;
            }
            set
            {
                PsiX.Input = value;
            }
        }

        public double Psi(double x)
        {
            return PsiX.Func(x);
        }

        FunctionGrammar PT = new FunctionGrammar();

        public string PInpit
        {
            get
            {
                return PT.Input;
            }
            set
            {
                PT.Input = value;
            }
        }

        public double P(double t)
        {
            return PT.Func(t);
        }

        //double p_1(double t)
        //{
        //    double ans = P(t) - (Phi(X0 + A * t) + Phi(X0 - A * t)) / 2;
        //    ans -= 1 / (2 * A) * Integrate(Psi,X0 - A*t, X0 + A*t, h * h / 2);
        //    return ans;
        //}

        VolterII volter_int = new VolterII();

        double p_2(double t)
        {
            double ans = SecondDerivative(P, t, h);
            ans -= (SecondDerivative(Phi, X0 + A * t,h)*Math.Pow(A,2)
                + Math.Pow(A, 2)* SecondDerivative(Phi, X0 - A * t, h)) / 2;
            ans -= 1 / (2 * A) * (FirstDerivative(Psi,X0+A*t,h) * Math.Pow(A, 2)
                - Math.Pow(A, 2) * FirstDerivative(Psi, X0 - A * t, h));
            return ans;
        }

        public void Solve(out List<double> g, out double[] t_arr)
        {
            //volter_int.F = (t) => SecondDerivative(p_1,t,h) / F(X0);
            volter_int.F = (t) => p_2(t) / F(X0);
            volter_int.Lambda = 1 / 2 * F(X0);
            volter_int.K = (t, tau) => FirstDerivative(F,X0 + A *(t - tau),h) * A
                           - FirstDerivative(F, X0 - A * (t - tau), h) * A;
            volter_int.Solve(out g, out t_arr);
        }
    }        
}
