using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Core
{
	/// <summary>
	/// Узел переноса массы.
	/// </summary>
	public class MassNode : Component
	{
		/// <summary>
		/// Дочерние элементы.
		/// </summary>
		private readonly List<Component> _children = new List<Component>();

		/// <summary>
		/// Дочерние узлы.
		/// </summary>
		public override List<Component> Children
		{
			get
			{
				return _children;
			}
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="name">Название узла.</param>
		/// <param name="strategy">Обрабтчик прохождения зарытого узла.</param>
		public MassNode(string name, IStrategy strategy)
			: base(name, strategy)
		{

		}

		/// <summary>
		/// Добавить дочерний узел.
		/// </summary>
		/// <param name="component">Добавляемый узел.</param>
		public override void Add(Component component)
		{
			if (component.GetType() == typeof(FlowPath))
			{
				if (_children.All(x => x.Name != component.Name))
				{
					_children.Add(component);
					component.SetParent(this);
					component.SetLevel(Level + 1);
				}
			}
			else throw new Exception("К узлу переноса массы можно подключить только трубу в качестве дочернего элемента.");
		}

		/// <summary>
		/// Удалить выбранный дочерний узел.
		/// </summary>
		/// <param name="component">Удаляемый узел.</param>
		public override void Remove(Component component)
		{
			if (_children.Any(x => x.Name == component.Name))
			{
				component.SetParent(null);
				_children.Remove(component);
			}
		}

		/// <summary>
		/// Прямой проход.
		/// </summary>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public override BypassedArgument DirectBypass(BypassedArgument args)
		{
			if (Parent != null && _strategy != null)
			{
				OnDirectBypass(new BypassEventArgs(Name, Level, args));
				args = _strategy.DirectBypass(args);
				OnDirectBypassed(new BypassEventArgs(Name, Level, args));
			}
			return args;
		}

		/// <summary>
		/// Обратный проход с поднятием данных для прямого прохода.
		/// </summary>
		/// <remarks>Метод необходим для обработки дочерних узлов в прямом проходе,
		/// для соблюдения порядка вложенности.</remarks>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public override BypassedArgument RiseDirectBypass(BypassedArgument args)
		{
			if (Parent != null && _strategy != null)
			{
				OnDirectBypass(new BypassEventArgs(Name, Level, args));
				args = _strategy.RiseDirectBypass(args);
				OnDirectBypassed(new BypassEventArgs(Name, Level, args));
			}
			return args;
		}

		/// <summary>
		/// Обратный проход.
		/// </summary>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public override BypassedArgument ReverseBypass(BypassedArgument args)
		{
			if (_strategy != null)
			{
				OnDirectBypass(new BypassEventArgs(Name, Level, args));
				args = _strategy.ReverseBypass(args);
				OnDirectBypassed(new BypassEventArgs(Name, Level, args));
			}
			return args;
		}
	}
}
