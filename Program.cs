using System;
using System.Diagnostics;

namespace Giraffe_2 {
	class Program {
		static void Main(string[] args) {
			int core = findcore();

			for (int i = 0; core > i; i++) {
				Console.WriteLine("Yay!!");
			}
			//Console.ReadLine();

		}

		static int findcore() {
			int hcore = Environment.ProcessorCount;
			int core = hcore / 4;
			return core;
			//bob
		}

		static void Render() {
			Process.Start("C:\\");
		}
	}



}

