using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
     * K_{11}, K_{22}, K_{21}, K{22} are given.
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

    class Volter2System
    {
    }
}
