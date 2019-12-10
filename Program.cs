using System.Threading;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;


namespace Giraffe_2 {
	class Program {
		static void Main(string[] args) {


			string dir = search();
			Console.WriteLine("Rendering with " + dir.Substring(29));
			dir = search() + "\\Support Files\\AErender.exe";
			int core = findcore();
			

			for (;;)
			{
				var updated1 = System.IO.File.GetLastWriteTime("M:\\Render Watch folders\\AFX Render file\\AFX Render_1.aep");
				var updated2 = System.IO.File.GetLastWriteTime("M:\\Render Watch folders\\AFX Render file\\AFX Render_2.aep");
				var crtime = DateTime.Now;
				//Console.WriteLine(crtime);
				TimeSpan Render_1 = crtime - updated1;
				TimeSpan Render_2 = crtime - updated2;
				TimeSpan maxtime = new TimeSpan(0, 0, 30, 0);
				Console.WriteLine("Time since Render_1 was saved: " + Render_1);
				Console.WriteLine("Time since Render_2 was saved: " + Render_2);

				PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");


				if (maxtime >= Render_1 && maxtime >= Render_2) {
				Process[] processes = new Process[core];
				Process[] processes2 = new Process[core];
				
				Console.WriteLine("Starting Render_1");
				for (int i = 0; core > i; i++) {
					
						ProcessStartInfo info = new ProcessStartInfo();
						info.UseShellExecute = true;
						info.FileName = dir;
						Directory.SetCurrentDirectory("M:\\");
						info.Arguments = " -project \"M:\\Render Watch folders\\AFX Render file\\AFX Render_1.aep\"";
						processes[i] = Process.Start(info);
						// processes.StartInfo = info;
						// Process Run = Process.Start(info);
						Console.WriteLine(String.Format("Yay!! There are {0} cores!", core));
					}

					Thread.Sleep(12000);

					while(true) {
						bool doBreak = true;
						string usage = cpuCounter.NextValue().ToString("0");
						Thread.Sleep(1000);
						usage = cpuCounter.NextValue().ToString("0");
						Console.WriteLine("CPU usage:"+usage+"%");
						int usageD = Convert.ToInt16(usage);

							foreach (Process process in processes) {
								if (!process.HasExited) {
									doBreak = false;
									break;
								}
							}

							if (doBreak) break;
							else if (usageD <= 5) break;						
							else Thread.Sleep(9000);
							usage = cpuCounter.NextValue().ToString("0");
							Thread.Sleep(1000);
							usage = cpuCounter.NextValue().ToString("0");
						
					}
					Console.WriteLine("Starting Render_2");
					for (int i = 0; core > i; i++) {	
						ProcessStartInfo info = new ProcessStartInfo();
						info.UseShellExecute = true;
						info.FileName = dir;
						Directory.SetCurrentDirectory("M:\\");
						info.Arguments = " -project \"M:\\Render Watch folders\\AFX Render file\\AFX Render_2.aep\"";
						processes2[i] = Process.Start(info);
						// processes.StartInfo = info;
						// Process Run = Process.Start(info);
						Console.WriteLine(String.Format("Yay!! There are {0} cores!", core));
					}

					Thread.Sleep(12000);

					while(true) 
					{
						bool doBreak2 = true;
						string usage = cpuCounter.NextValue().ToString("0");
						Thread.Sleep(1000);
						usage = cpuCounter.NextValue().ToString("0");
						Console.WriteLine("CPU usage:"+usage+"%");
						int usageD = Convert.ToInt16(usage);

							foreach (Process process2 in processes2) {
								if (!process2.HasExited) {
									doBreak2 = false;
									break;
								}
							}

								if (doBreak2) break;
								else if (usageD <= 10) break;						
								else Thread.Sleep(9000);
								usage = cpuCounter.NextValue().ToString("0");
								Thread.Sleep(1000);
								usage = cpuCounter.NextValue().ToString("0");
					}
				}	
				else if (maxtime >= Render_1)
				{
				Process[] processes = new Process[core];
				Console.WriteLine("Starting Render_1");
				
				for (int i = 0; core > i; i++) {
					
						ProcessStartInfo info = new ProcessStartInfo();
						info.UseShellExecute = true;
						info.FileName = dir;
						Directory.SetCurrentDirectory("M:\\");
						info.Arguments = " -project \"M:\\Render Watch folders\\AFX Render file\\AFX Render_1.aep\"";
						processes[i] = Process.Start(info);
						// processes.StartInfo = info;
						// Process Run = Process.Start(info);
						Console.WriteLine(String.Format("Yay!! There are {0} cores!", core));
					}

					Thread.Sleep(12000);

					while(true) {
						bool doBreak = true;
						string usage = cpuCounter.NextValue().ToString("0");
						Thread.Sleep(1000);
						usage = cpuCounter.NextValue().ToString("0");
						Console.WriteLine("CPU usage:"+usage+"%");
						var usageD = Convert.ToInt16(usage);

							foreach (Process process in processes) {
								if (!process.HasExited) {
									doBreak = false;
									break;
								}
							}

							if (doBreak) break;
							else if (usageD <= 5) break;						
							else Thread.Sleep(9000);
							usage = cpuCounter.NextValue().ToString("0");
							Thread.Sleep(1000);
							usage = cpuCounter.NextValue().ToString("0");
						
					} 	
				}
				else if (maxtime >= Render_2)
				{
				Process[] processes2 = new Process[core];
				Console.WriteLine("Starting Render_2");

					for (int i = 0; core > i; i++) {	
						ProcessStartInfo info = new ProcessStartInfo();
						info.UseShellExecute = true;
						info.FileName = dir;
						Directory.SetCurrentDirectory("M:\\");
						info.Arguments = " -project \"M:\\Render Watch folders\\AFX Render file\\AFX Render_2.aep\"";
						processes2[i] = Process.Start(info);
						// processes.StartInfo = info;
						// Process Run = Process.Start(info);
						Console.WriteLine(String.Format("Yay!! There are {0} cores!", core));
					}

					Thread.Sleep(12000);

					while(true) 
					{
						bool doBreak2 = true;
						string usage = cpuCounter.NextValue().ToString("0");
						Thread.Sleep(1000);
						usage = cpuCounter.NextValue().ToString("0");
						Console.WriteLine("CPU usage:"+usage+"%");
						int usageD = Convert.ToInt16(usage);

							foreach (Process process2 in processes2) {
								if (!process2.HasExited) {
									doBreak2 = false;
									break;
								}
							}

								if (doBreak2) break;
								else if (usageD <= 10) break;						
								else Thread.Sleep(9000);
								usage = cpuCounter.NextValue().ToString("0");
								Thread.Sleep(1000);
								usage = cpuCounter.NextValue().ToString("0");
					}
				}
				else {
					Thread.Sleep(9000);
					string usage = cpuCounter.NextValue().ToString("0");
					Thread.Sleep(1000);
					usage = cpuCounter.NextValue().ToString("0");
					Console.WriteLine("CPU usage:"+usage + "%");
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
			return dirs[index];
		}

		static int findcore() {
			int hcore = Environment.ProcessorCount;
			int core = hcore / 4;
			return core;
		}

}
}



