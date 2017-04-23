using System;
using System.Management;
using System.Text;

namespace ProcessorView
{
	public class Program
	{

		private static ConsoleColor defaultForegroundColor;

		public static void Main()
		{
			Console.Title = "Processor View";
			defaultForegroundColor = Console.ForegroundColor;
			WriteProcessorsInfo(GetProcessors());
		}

		private static ManagementObjectCollection GetProcessors()
		{
			return new ManagementObjectSearcher(@"Root\CIMV2", "SELECT * FROM Win32_Processor").Get();
		}

		private static void WriteProcessorsInfo(ManagementObjectCollection processorCollection)
		{
			foreach (var processor in processorCollection)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				WriteGeneralProcessorInfo(processor);
				Console.ForegroundColor = defaultForegroundColor;

				WriteFullLine('-');

				Console.ForegroundColor = ConsoleColor.Green;
				WriteClockSpeedInfo(processor);
				Console.ForegroundColor = defaultForegroundColor;

				WriteFullLine('-');

				Console.ForegroundColor = ConsoleColor.Yellow;
				WriteVoltageInfo(processor);
				Console.ForegroundColor = defaultForegroundColor;

				WriteFullLine('-');

				Console.ForegroundColor = ConsoleColor.Magenta;
				WriteCacheInfo(processor);
				Console.ForegroundColor = defaultForegroundColor;

				WriteFullLine('-');

				Console.ForegroundColor = ConsoleColor.Cyan;
				WriteExtraInformation(processor);
				Console.ForegroundColor = defaultForegroundColor;

				WriteFullLine('-');
			}
		}

		private static void WriteGeneralProcessorInfo(ManagementBaseObject processor)
		{
			Console.WriteLine("Name: " + processor["Name"]);
			Console.WriteLine("Manufacturer: " + processor["Manufacturer"]);
			Console.WriteLine("Device ID: " + processor["DeviceID"]);
		}

		private static void WriteClockSpeedInfo(ManagementBaseObject processor)
		{
			Console.WriteLine("Current Clock Speed: {0}MHz", processor["CurrentClockSpeed"]);
			Console.WriteLine("Maximum Clock Speed: {0}MHz", processor["MaxClockSpeed"]);
		}

		private static void WriteVoltageInfo(ManagementBaseObject processor)
		{
			Console.WriteLine("Current Voltage: {0} tenth-volts", processor["CurrentVoltage"]);
			Console.WriteLine("Voltage Caps: {0}", processor["VoltageCaps"] == null ? "Unknown" : processor["VoltageCaps"] + " volts");
		}

		private static void WriteCacheInfo(ManagementBaseObject processor)
		{
			Console.WriteLine("L2 Cache Size: " + processor["L2CacheSize"]);
			Console.WriteLine("L3 Cache Size: " + processor["L3CacheSize"]);
		}

		private static void WriteExtraInformation(ManagementBaseObject processor)
		{
			Console.WriteLine("Processor Type: " + Enum.GetName(typeof(ProcessorType), Convert.ToUInt16(processor["ProcessorType"])));
			Console.WriteLine("Health / Status: " + processor["Status"]);
		}

		private static void WriteFullLine(char c)
		{
			StringBuilder sb = new StringBuilder(Console.WindowWidth);
			sb.Append(c, Console.WindowWidth);
			Console.Write(sb.ToString());
		}
	}
}
