using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = new Process();
            p.StartInfo.FileName =Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\The Chosen Sausage\NoLogin\NoLogin.exe";
            p.Start();
        }
    }
}
