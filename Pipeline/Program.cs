using Pipeline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pipeline
{
	class Program
	{
		static void Main(string[] args)
		{
			// Наполняем структуру.
			PressureNode root = new PressureNode("Pressure_0", new PressureNodeStrategy());
			FlowPath flowPath_1 = new FlowPath("FlowPath_1", new FlowPathStrategy());
			MassNode mass_2 = new MassNode("Mass_2", new SympleMassNodeStrategy());
			FlowPath flowPath_3 = new FlowPath("FlowPath_3", new FlowPathStrategy());
			MassNode mass_4 = new MassNode("Mass_4", new BranchMassNodeStrategy());
			FlowPath flowPath_5_0 = new FlowPath("FlowPath_5_0", new FlowPathStrategy());
			MassNode mass_6_0 = new MassNode("Mass_6_0", new SympleMassNodeStrategy());
			FlowPath flowPath_7_0 = new FlowPath("FlowPath_7_0", new FlowPathStrategy());
			MassNode mass_8_0 = new MassNode("Mass_8_0", new SympleMassNodeStrategy());
			FlowPath flowPath_9_0 = new FlowPath("FlowPath_9_0", new FlowPathStrategy());
			CloseNode close_10_0 = new CloseNode("Close_10_0", new CloseNodeStrategy());
			FlowPath flowPath_5_1 = new FlowPath("FlowPath_5_1", new FlowPathStrategy());
			MassNode mass_6_1 = new MassNode("Mass_6_1", new BranchMassNodeStrategy());
			FlowPath flowPath_7_1_0 = new FlowPath("FlowPath_7_1_0", new FlowPathStrategy());
			CloseNode close_8_1_0 = new CloseNode("Close_8_1_0", new CloseNodeStrategy());
			FlowPath flowPath_7_1_1 = new FlowPath("FlowPath_7_1_1", new FlowPathStrategy());
			CloseNode close_8_1_1 = new CloseNode("Close_8_1_1", new CloseNodeStrategy());

			root.Add(flowPath_1);
			flowPath_1.Add(mass_2);
			mass_2.Add(flowPath_3);
			flowPath_3.Add(mass_4);
			mass_4.Add(flowPath_5_1);
			mass_4.Add(flowPath_5_0);
			flowPath_5_0.Add(mass_6_0);
			mass_6_0.Add(flowPath_7_0);
			flowPath_7_0.Add(mass_8_0);
			mass_8_0.Add(flowPath_9_0);
			flowPath_9_0.Add(close_10_0);
			flowPath_5_1.Add(mass_6_1);
			mass_6_1.Add(flowPath_7_1_1);
			mass_6_1.Add(flowPath_7_1_0);
			flowPath_7_1_0.Add(close_8_1_0);
			flowPath_7_1_1.Add(close_8_1_1);

			BypassedArgument reverseResult = new BypassedArgument();
			BypassedArgument result = new BypassedArgument();
			// Распараллеливаем прямой и обратный проходы.
			Parallel.Invoke(() =>
			{
				reverseResult = root.ReverseBypass(reverseResult);
			}, () =>
			{
				result = close_8_1_0.DirectBypass(result);
			});
			// Сохраняем результат прямого прохода.
			StringBuilder resultBuilder = new StringBuilder();
			resultBuilder.AppendLine("Название узла \tПорядковый номер в проходе \tУровень");
			int i = 0;
			foreach (KeyValuePair<string, int> item in result.TransportTrace)
				resultBuilder.AppendLine(string.Format("{0} \t{1} \t{2}", item.Key, i++, item.Value));
			File.WriteAllText("DirectBypass.txt", resultBuilder.ToString());

			// Сохраняем результат обратного прохода.
			StringBuilder reverseResultBuilder = new StringBuilder();
			reverseResultBuilder.AppendLine("Название узла \tПорядковый номер в проходе \tУровень");
			int j = 0;
			foreach (KeyValuePair<string, int> item in reverseResult.TransportTrace)
				reverseResultBuilder.AppendLine(string.Format("{0} \t{1} \t{2}", item.Key, j++, item.Value));
			File.WriteAllText("ReverseBypass.txt", reverseResultBuilder.ToString());
		}
	}
}
