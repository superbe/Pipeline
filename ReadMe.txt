Тестовое задание (Прямой и обратный проход по нефтетрубопроводу).  

1. Проекты:
	Pipeline.Core - основной проект, в котором реализована бизнес-модель 
		трубопровода.
	Pipeline.Tests - модульные тесты (тесты сделал по минимуму).
	Pipeline - консольное приложение (сделано по минимуму, в теле приложения
		формируется структура трубопровода, а результаты прямого и 
		обратного проходов сохраняются в текстовые файлы).
2. В проекте используется 4 основных узла:
	PressureNode - узел давления.
	FlowPath - труба.
	MassNode - узел переноса массы.
	CloseNode - закрытый узел. Закрытый узел не имеет дочерних элементов.
При создании элемента в конструктор передается наименование элемента и стратегия
обработки узла:
	PressureNode root = new PressureNode("Pressure_0", new PressureNodeStrategy());
В проекте определены следующие стратегии обработки элементов:
	PressureNodeStrategy - обработка узла давления.
	FlowPathStrategy - обработка трубы.
	SympleMassNodeStrategy - обработка простого узла переноса массы без
		ветвления.
	BranchMassNodeStrategy - обработка сложного узла переноса массы с
		ветвлением.
Для элемента можно отдельно задать стратегию обработки узла посредством метода
SetStrategy:
	root.SetStrategy(new PressureNodeStrategy());
Соединение элементов происходит с помощью метода Add
	PressureNode root = new PressureNode("root", new PressureNodeStrategy());
	FlowPath flowPath = new FlowPath("FlowPath", new FlowPathStrategy());
	root.Add(flowPath);
Здесь элемент flowPath выступает в качестве дочернего элемента для корневого
элемента root.
"Разъединение" элементов происходит посредством удаления дочернего элемента
	root.Remove(flowPath);
Прямой проход осуществляется с помощью метода DirectBypass
	BypassedArgument result = closeNode.DirectBypass(new BypassedArgument());
Обратный проход осуществляется с помощью метода ReverseBypass
	BypassedArgument result = closeNode.ReverseBypass(new BypassedArgument());
Здесь BypassedArgument - класс аргумента прохода, а result.TransportTrace - 
коллекция результата обхода, содержит список имен пройденных элементов в 
порядке обхода, result.Path - полный путь обхода.

ToDo: Сделать аналогичный проект для AI.
