
using System.Threading;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;


namespace Giraffe_2 {
	class Program {


		static void Main(string[] args) {
			//Regex Arguments = new Regex(@"(/c)");
			//string CoreOverRide = "";
			bool skipCore = false;
			bool PrevOnly = false;
			bool FinalOnly = false;
			int core = 1;
			string VOveride = "null";
			try {
				if (args.Length > 0) {
					for (int a = 0; a < args.Length; a++) {
						if (args[a] == "/c") {
							//CoreOverRide = args[a + 1];
							skipCore = true;
							core = Convert.ToInt32(args[a + 1]);
							Console.WriteLine(args[0] + " " + args[1]);
						} else if (args[a] == "/p") {
							Console.WriteLine("Rendering in Preview only mode.");
							PrevOnly = true;
						} else if (args[a] == "/f") {
							Console.WriteLine("Rendering in Final only mode.");
							FinalOnly = true;
						} else if (args[a] == "/v") {
							VOveride = args[a + 1];
						} else Console.WriteLine("test");
					}
				} else {
					Console.WriteLine("No Arguments");
				}

			} catch {
				if (args.Length > 0) {
					skipCore = false;
					PrevOnly = false;
					FinalOnly = false;
					Console.WriteLine("Argument syntax is incorect or you tried to disable both render modes, which would leave it doing nothing. Using default settings. \nYou can use /c + space + number for chosing thread number \n/f for final render only mode \n/p for preview only mdoe \n Press ENTER to continue.");
					Console.ReadLine();
				} else Console.WriteLine("Issue with the arguments");
			}
			if (PrevOnly == true && FinalOnly == true) {
				Console.WriteLine("You tried to disable both render modes which would cause the program to do nothing. \nSwitching to render all mode instead.");
				Thread.Sleep(5000);
				PrevOnly = false;
				FinalOnly = false;
			}
			string dir = search(VOveride);
			Console.WriteLine("Rendering with " + dir.Substring(29));
			dir = search(VOveride) + "\\Support Files\\AErender.exe";
			if (!skipCore) core = findcore();
			else Console.WriteLine("Overide thread count: " + core);
			Thread.Sleep(3000);
			for (; ; )
			{
				if (!FinalOnly) {
					PreviewRender(dir, core);
					clear();
					Console.WriteLine("Sleeping for 10 seconds between checking for renders");
					Thread.Sleep(10000);
				}
				if (!PrevOnly) {
					RenderFinal(dir, core);
					clear();
					Console.WriteLine("Sleeping for 10 seconds between checking for renders");
					Thread.Sleep(10000);
				}
			}
		}

		static string search(string VOveride) {

			string[] dirs = Directory.GetDirectories(@"c:\program files\Adobe", "*After effects*");
			string pattern = @"[0-9.]+";
			double largest = 0;
			double year;
			int index = -1;
			if (VOveride != "null") {
				Console.WriteLine("Attempting to run AfterEffects " + VOveride);
				return @"c:\program files\Adobe\Adobe After Effects " + VOveride;
			}

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
			string answer = "";
			Console.WriteLine("Auto selected instance count is: " + core + "\nOverwrite?");
			try {
				answer = Reader.ReadLine(5000);
			} catch (TimeoutException) {
				clear();
				Console.WriteLine("Using auto selected thread count");
			}
			// answer = Console.ReadLine();

			if (answer.ToLower() == "y" || answer == "yes") {
				Console.WriteLine("What should it be overwriten to? ");
				int OverwriteNum = 0;
				string Overwrite;
				Overwrite = Reader.ReadLine();
				bool nan = int.TryParse(Overwrite, out OverwriteNum);
				//Console.WriteLine(nan);
				if (nan) return OverwriteNum;
				else {
					while (!nan) {
						Console.WriteLine("You need to ender a number");
						Overwrite = Console.ReadLine();
						nan = int.TryParse(Overwrite, out OverwriteNum);
					}
					//Console.WriteLine(OverwriteNum);
					return OverwriteNum;
				}
			} else { //if (answer.ToLower() == "n" || answer == "no") 
				string name = System.Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
				//Console.WriteLine(name);
				if (name == "AMD64 Family 21 Model 1 Stepping 2, AuthenticAMD") {
					return 4;
				} else {
					return core;
				}
			}//else return core;
		}

		static void clear() {
			try {
				Console.Clear();
			} catch { Console.WriteLine("No console to clear."); };
		}

		static void PreviewRender(string dir, int core) {

			double extraCore = core * 1.75;
			//extraCore = Math.Ceiling(extraCore);
			core = Convert.ToInt16(Math.Ceiling(extraCore));
			string[] filepaths = Filepath(core);
			int count = 0;
			if (filepaths == null) {
				clear();
				Console.WriteLine("No previews to render");
				Thread.Sleep(4000);
				return;
			}
			for (int c = 0; c < filepaths.Length; c++) {
				if (filepaths[c] == null) break;
				count++;
			}

			Process[] Previews = new Process[count];

			if (filepaths != null) {

				Console.WriteLine("Starting preview render");
				Console.WriteLine(String.Format("Launching {0} Instaces of AfterFX", count));
				Console.WriteLine("Debug: This is the number of filepath.lenght: " + filepaths.Length);


				for (int i = 0; i < count; i++) {
					if (filepaths[i] == null) break;
					ProcessStartInfo PreviewInfo = new ProcessStartInfo();
					string tempFilepath = "\"" + filepaths[i] + "\"";
					PreviewInfo.UseShellExecute = true;
					PreviewInfo.FileName = dir;
					Directory.SetCurrentDirectory("M:\\");
					PreviewInfo.Arguments = "-project " + tempFilepath;
					Previews[i] = Process.Start(PreviewInfo);

				}
				Thread.Sleep(12000);
				Waiter(Previews);

			}
		}
		static void RenderFinal(string dir, int core) {
			var updated1 = System.IO.File.GetLastWriteTime("M:\\Render Watch folders\\AFX Render file\\AFX Render_1.aep");
			var updated2 = System.IO.File.GetLastWriteTime("M:\\Render Watch folders\\AFX Render file\\AFX Render_2.aep");
			var crtime = DateTime.Now;
			//Console.WriteLine(crtime);
			TimeSpan Render_1 = crtime - updated1;
			TimeSpan Render_2 = crtime - updated2;
			TimeSpan maxtime = new TimeSpan(0, 0, 20, 0);
			clear();
			Console.WriteLine("Time since Render_1 was saved: " + Render_1);
			Console.WriteLine("Time since Render_2 was saved: " + Render_2);
			Thread.Sleep(4000);

			PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

			//render Both Render 1 and Render 2

			if (maxtime >= Render_1 && maxtime >= Render_2) {
				Process[] processes = new Process[core];
				Process[] processes2 = new Process[core];
				clear();
				Console.WriteLine("Time since Render_1 was saved: " + Render_1);
				Console.WriteLine("Time since Render_2 was saved: " + Render_2);
				Console.WriteLine("Starting Render_1 and Render_2, begining with Render_1");
				Console.WriteLine(String.Format("Launching {0} Instaces of AfterFX", core));
				for (int i = 0; core > i; i++) {
					Thread.Sleep(500);
					ProcessStartInfo info = new ProcessStartInfo();
					info.UseShellExecute = true;
					info.FileName = dir;
					Directory.SetCurrentDirectory("M:\\");
					info.Arguments = " -project \"M:\\Render Watch folders\\AFX Render file\\AFX Render_1.aep\"";
					processes[i] = Process.Start(info);
					// processes.StartInfo = info;
					// Process Run = Process.Start(info);

				}

				Thread.Sleep(12000);
				//int breaker = 0;
				Waiter(processes);

				clear();
				Console.WriteLine("Starting Render_2");
				for (int i = 0; core > i; i++) {
					Thread.Sleep(500);
					ProcessStartInfo info = new ProcessStartInfo();
					info.UseShellExecute = true;
					info.FileName = dir;
					Directory.SetCurrentDirectory("M:\\");
					info.Arguments = " -project " + "M:\\Render Watch folders\\AFX Render file\\AFX Render_2.aep\"";
					processes2[i] = Process.Start(info);
					// processes.StartInfo = info;
					// Process Run = Process.Start(info);
					//Console.WriteLine(String.Format("Yay!! There are {0} cores!", core));
				}

				Thread.Sleep(12000);
				Waiter(processes2);

				//Render only Render 1
			} else if (maxtime >= Render_1) {
				clear();
				Console.WriteLine("Time since Render_1 was saved: " + Render_1);
				Console.WriteLine("Starting Render_1");
				Console.WriteLine(String.Format("Launching {0} Instaces of AfterFX", core));
				Process[] processes = new Process[core];


				for (int i = 0; core > i; i++) {
					Thread.Sleep(500);
					ProcessStartInfo info = new ProcessStartInfo();
					info.UseShellExecute = true;
					info.FileName = dir;
					Directory.SetCurrentDirectory("M:\\");
					info.Arguments = " -project \"M:\\Render Watch folders\\AFX Render file\\AFX Render_1.aep\"";
					processes[i] = Process.Start(info);
					// processes.StartInfo = info;
					// Process Run = Process.Start(info);
					//Console.WriteLine(String.Format("Yay!! There are {0} cores!", core));
				}

				//Thread.Sleep(12000);
				Waiter(processes);

				//Render_2 
			} else if (maxtime >= Render_2) {
				clear();
				Console.WriteLine("Time since Render_2 was saved: " + Render_2);
				Console.WriteLine("Starting Render_2");
				Console.WriteLine(String.Format("Launching {0} Instaces of AfterFX", core));
				Process[] processes2 = new Process[core];


				for (int i = 0; core > i; i++) {
					Thread.Sleep(500);
					ProcessStartInfo info = new ProcessStartInfo();
					info.UseShellExecute = true;
					info.FileName = dir;
					Directory.SetCurrentDirectory("M:\\");
					info.Arguments = " -project \"M:\\Render Watch folders\\AFX Render file\\AFX Render_2.aep\"";
					processes2[i] = Process.Start(info);
				}

				Thread.Sleep(12000);
				Waiter(processes2);
			} else {
				//Thread.Sleep(9000);
				return;
			}
		}

		static string[] Filepath(int core) {
			string[] Previews = Directory.GetFiles(@"M:\Render Watch folders\Preview watch folder\");
			string[] coreCount = new string[Previews.Length];
			string doneFolder = @"M:\Render Watch folders\Preview watch folder\Done\";


			if (Previews.Length >= 1 && Previews.Length < core) {
				for (int i = 0; i < Previews.Length; i++) {
					coreCount[i] = doneFolder + Path.GetFileName(Previews[i]);
					try {
						File.Move(Previews[i], coreCount[i]);
					} catch {
						File.Delete(coreCount[i]);
						i--;
						Console.WriteLine("File already exists in Done folder. Deleting and moving new file");
					}
				}
				return coreCount;

			} else if (Previews.Length >= 1 && Previews.Length >= core) {
				for (int i = 0; i < core; i++) {
					coreCount[i] = doneFolder + Path.GetFileName(Previews[i]);
					try {
						File.Move(Previews[i], coreCount[i]);
					} catch {
						File.Delete(coreCount[i]);
						i--;
						Console.WriteLine("File already exists in Done folder. Deleting and moving new file");
					};
				}
				return coreCount;

			} else {
				//Console.WriteLine("No files in folder");
				return null;
			}

		}

		static void Waiter(Process[] Processes) {
			PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			int breaker = 0;
			while (true) {
				bool doBreak = true;
				string usage = cpuCounter.NextValue().ToString("0");
				Thread.Sleep(1000);
				usage = cpuCounter.NextValue().ToString("0");
				clear();
				Console.WriteLine("CPU usage:" + usage + "%");
				int usageD = Convert.ToInt16(usage);

				foreach (Process process in Processes) {
					if (!process.HasExited) {
						doBreak = false;
						break;
					}
				}

				if (doBreak) break;
				else if (usageD <= 5) {
					Console.WriteLine("Breaker is now" + breaker + "it will resume the ");
					breaker++;
				} else if (breaker == 30) break;
				else Thread.Sleep(100);
				usage = cpuCounter.NextValue().ToString("0");
				Thread.Sleep(100);
				usage = cpuCounter.NextValue().ToString("0");


			}
		}



	}
}
