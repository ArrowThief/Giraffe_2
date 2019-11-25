using System.Threading;
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


			for (;;)
			{
				var updated = System.IO.File.GetLastWriteTime("M:\\Render Watch folders\\AFX Render file\\AFX Render_1.aep");
				Console.WriteLine(updated);
				var crtime = DateTime.Now;
				Console.WriteLine(crtime);
				TimeSpan interval = crtime - updated;
				TimeSpan maxtime = new TimeSpan(0, 0, 10, 0);
				Console.WriteLine(interval);


				if (maxtime >= interval) {
					// Process process = new Process();
					Process[] processes = new Process[core];



					for (int i = 0; core > i; i++) {


						ProcessStartInfo info = new ProcessStartInfo();
						info.UseShellExecute = true;
						info.FileName = dir;
						//info.WorkingDirectory = "M:\\";
						Directory.SetCurrentDirectory("M:\\");
						info.Arguments = " -project \"M:\\Render Watch folders\\AFX Render file\\AFX Render_1.aep\"";
						processes[i] = Process.Start(info);
						// processes.StartInfo = info;
						// Process Run = Process.Start(info);
						Console.WriteLine(String.Format("Yay!! There are {0} cores!", core));

					}

					while(true) {
						bool doBreak = true;

						foreach (Process process in processes) {
							if (!process.HasExited) {
								doBreak = false;
								break;
							}
						}

						if (doBreak) break;
						else Thread.Sleep(10000);
					}
						
				} else {
					Thread.Sleep(10000);
				}
			}
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
				if (largest < year) {
					largest = year;
					index = i;
				}

			}
			Console.WriteLine("You are using AFX CC " + dirs);
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