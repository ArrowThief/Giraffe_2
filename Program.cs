using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;


namespace Giraffe_2 {
	class Program {
		static void Main(string[] args) {


			string dir = search() + "\\Support Files\\AErender.exe";

			Console.WriteLine(dir);
			int core = findcore();

			for (int i = 0; core > i; i++) {
				Process process = new Process();
				ProcessStartInfo info = new ProcessStartInfo();
				info.UseShellExecute = true;
				info.FileName = dir;
				// info.WorkingDirectory = "M:\\";
				info.Arguments = " -project \"M:\\Render Watch folder\\AFX Render file\\AFX Render_1.aep\"";
				process.StartInfo = info;
				process.Start();
				Console.WriteLine("Yay!!");


			}
			//Console.ReadLine();

		}

		static string search() {

			string[] dirs = Directory.GetDirectories(@"c:\program files\Adobe", "*After effects*");
			string pattern = @"[0-9.]+";
			double largest = 0;
			double year;
			int index = -1;

			Regex exp = new Regex(pattern);

			for (int i = 0; i < dirs.Length; i++) {
				string dir = dirs[i];
				foreach (Match m in exp.Matches(dir)) {
					dir = m.Value;
				}
				year = Convert.ToDouble(dir);
				//Console.WriteLine(year);
				if (largest < year) {
					largest = year;
					index = i;
				}

			}
			return dirs[index];
		}

		static int findcore() {
			int hcore = Environment.ProcessorCount;
			int core = hcore / 4;
			return core;
			//bob
		}

	}
}