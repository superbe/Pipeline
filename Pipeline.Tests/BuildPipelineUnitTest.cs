using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pipeline.Core;

namespace Pipeline.Tests
{
	[TestClass]
	public class BuildPipelineUnitTest
	{
		private string directBypass;
		private string directBypassed;

		[TestMethod]
		public void BranchBypassPipelineTest()
		{
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
			mass_4.Add(flowPath_5_0);
			flowPath_5_0.Add(mass_6_0);
			mass_6_0.Add(flowPath_7_0);
			flowPath_7_0.Add(mass_8_0);
			mass_8_0.Add(flowPath_9_0);
			flowPath_9_0.Add(close_10_0);
			mass_4.Add(flowPath_5_1);
			flowPath_5_1.Add(mass_6_1);
			mass_6_1.Add(flowPath_7_1_0);
			mass_6_1.Add(flowPath_7_1_1);
			flowPath_7_1_0.Add(close_8_1_0);
			flowPath_7_1_1.Add(close_8_1_1);

			BypassedArgument reverseResult = new BypassedArgument();
			reverseResult = root.ReverseBypass(reverseResult);

			BypassedArgument result = new BypassedArgument();
			result = close_8_1_0.DirectBypass(result);

			Assert.AreEqual("Pressure_0/FlowPath_1/Mass_2/FlowPath_3/Mass_4/FlowPath_5_0/Mass_6_0/FlowPath_7_0/Mass_8_0/FlowPath_9_0/Close_10_0/FlowPath_5_1/Mass_6_1/FlowPath_7_1_0/Close_8_1_0/FlowPath_7_1_1/Close_8_1_1", reverseResult.Path);
			Assert.AreEqual("Close_8_1_0/FlowPath_7_1_0/Close_8_1_1/FlowPath_7_1_1/Mass_6_1/FlowPath_5_1/Close_10_0/FlowPath_9_0/Mass_8_0/FlowPath_7_0/Mass_6_0/FlowPath_5_0/Mass_4/FlowPath_3/Mass_2/FlowPath_1/Pressure_0", result.Path);
		}

		[TestMethod]
		public void SympleBypassPipelineTest()
		{
			PressureNode rootNode = new PressureNode("PressureRootNode", new PressureNodeStrategy());
			FlowPath firstFlowPath = new FlowPath("FirstFlowPath", new FlowPathStrategy());
			MassNode massNode = new MassNode("MassNode", new SympleMassNodeStrategy());
			FlowPath secondFlowPath = new FlowPath("SecondFlowPath", new FlowPathStrategy());
			CloseNode endNode = new CloseNode("EndNode", new CloseNodeStrategy());

			secondFlowPath.Add(endNode);
			massNode.Add(secondFlowPath);
			firstFlowPath.Add(massNode);
			rootNode.Add(firstFlowPath);

			BypassedArgument reverseResult = new BypassedArgument();
			reverseResult = rootNode.ReverseBypass(reverseResult);

			BypassedArgument result = new BypassedArgument();
			result = endNode.DirectBypass(result);

			Assert.AreEqual("PressureRootNode/FirstFlowPath/MassNode/SecondFlowPath/EndNode", reverseResult.Path);
			Assert.AreEqual("EndNode/SecondFlowPath/MassNode/FirstFlowPath/PressureRootNode", result.Path);
		}

		[TestMethod]
		public void SympleEventPipelineTest()
		{
			PressureNode rootNode = new PressureNode("PressureRootNode", new PressureNodeStrategy());
			rootNode.Directed += rootNode_Directed;
			rootNode.Direct += rootNode_Direct;
			FlowPath firstFlowPath = new FlowPath("FirstFlowPath", new FlowPathStrategy());
			MassNode massNode = new MassNode("MassNode", new SympleMassNodeStrategy());
			FlowPath secondFlowPath = new FlowPath("SecondFlowPath", new FlowPathStrategy());
			CloseNode endNode = new CloseNode("EndNode", new CloseNodeStrategy());


			secondFlowPath.Add(endNode);
			massNode.Add(secondFlowPath);
			firstFlowPath.Add(massNode);
			rootNode.Add(firstFlowPath);
			BypassedArgument result = new BypassedArgument();
			result = endNode.DirectBypass(result);
			Assert.AreEqual("EndNode/SecondFlowPath/MassNode/FirstFlowPath", directBypass);
			Assert.AreEqual("EndNode/SecondFlowPath/MassNode/FirstFlowPath/PressureRootNode", directBypassed);
			Assert.AreEqual("EndNode/SecondFlowPath/MassNode/FirstFlowPath/PressureRootNode", result.Path);
		}

		private void rootNode_Direct(object sender, BypassEventArgs e)
		{
			directBypass = e.Args.Path;
			Assert.AreEqual("EndNode/SecondFlowPath/MassNode/FirstFlowPath", e.Args.Path);
		}

		private void rootNode_Directed(object sender, BypassEventArgs e)
		{
			directBypassed = e.Args.Path;
			Assert.AreEqual("EndNode/SecondFlowPath/MassNode/FirstFlowPath/PressureRootNode", e.Args.Path);
		}
	}
}
