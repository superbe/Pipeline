1. �������:
	Pipeline.Core - �������� ������, � ������� ����������� ������-������ 
		������������.
	Pipeline.Tests - ��������� ����� (����� ������ �� ��������).
	Pipeline - ���������� ���������� (������� �� ��������, � ���� ����������
		����������� ��������� ������������, � ���������� ������� � 
		��������� �������� ����������� � ��������� �����).
2. � ������� ������������ 4 �������� ����:
	PressureNode - ���� ��������.
	FlowPath - �����.
	MassNode - ���� �������� �����.
	CloseNode - �������� ����. �������� ���� �� ����� �������� ���������.
��� �������� �������� � ����������� ���������� ������������ �������� � ���������
��������� ����:
	PressureNode root = new PressureNode("Pressure_0", new PressureNodeStrategy());
� ������� ���������� ��������� ��������� ��������� ���������:
	PressureNodeStrategy - ��������� ���� ��������.
	FlowPathStrategy - ��������� �����.
	SympleMassNodeStrategy - ��������� �������� ���� �������� ����� ���
		���������.
	BranchMassNodeStrategy - ��������� �������� ���� �������� ����� �
		����������.
��� �������� ����� �������� ������ ��������� ��������� ���� ����������� ������
SetStrategy:
	root.SetStrategy(new PressureNodeStrategy());
���������� ��������� ���������� � ������� ������ Add
	PressureNode root = new PressureNode("root", new PressureNodeStrategy());
	FlowPath flowPath = new FlowPath("FlowPath", new FlowPathStrategy());
	root.Add(flowPath);
����� ������� flowPath ��������� � �������� ��������� �������� ��� ���������
�������� root.
"������������" ��������� ���������� ����������� �������� ��������� ��������
	root.Remove(flowPath);
������ ������ �������������� � ������� ������ DirectBypass
	BypassedArgument result = closeNode.DirectBypass(new BypassedArgument());
�������� ������ �������������� � ������� ������ ReverseBypass
	BypassedArgument result = closeNode.ReverseBypass(new BypassedArgument());
����� BypassedArgument - ����� ��������� �������, � result.TransportTrace - 
��������� ���������� ������, �������� ������ ���� ���������� ��������� � 
������� ������, result.Path - ������ ���� ������.