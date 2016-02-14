using System;
using System.Collections.Generic;

namespace Pipeline.Core
{
	/// <summary>
	/// Труба.
	/// </summary>
	public class FlowPath : Component
	{
		/// <summary>
		/// Дочерний узел. В трубу может входить только один узел.
		/// </summary>
		private Component _children;

		/// <summary>
		/// Дочерний узел.
		/// </summary>
		public override List<Component> Children
		{
			get
			{
				return _children == null ? (List<Component>)null : new List<Component> { _children };
			}
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="name">Название узла.</param>
		/// <param name="strategy">Обрабтчик прохождения зарытого узла.</param>
		public FlowPath(string name, IStrategy strategy)
			: base(name, strategy)
		{

		}

		/// <summary>
		/// Добавить новый дочерний узел.
		/// </summary>
		/// <param name="component">Добавляемый узел.</param>
		public override void Add(Component component)
		{
			if (component.GetType() != typeof(PressureNode))
			{
				_children = component;
				component.SetParent(this);
				component.SetLevel(Level + 1);
			}
			else throw new Exception("Нельзя подключать к трубе узел давления в качестве дочернего элемента.");
		}

		/// <summary>
		/// Удалить выбранный дочерний узел.
		/// </summary>
		/// <param name="component">Удаляемый узел.</param>
		public override void Remove(Component component)
		{
			if (_children == component)
			{
				component.SetParent(null);
				_children = null;
			}
		}

		/// <summary>
		/// Проход вперед.
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