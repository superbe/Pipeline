using System;
using System.Collections.Generic;

namespace Pipeline.Core
{
	/// <summary>
	/// Закрытый узел.
	/// </summary>
	public class CloseNode : Component
	{
		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="name">Название узла.</param>
		/// <param name="strategy">Обрабтчик прохождения зарытого узла.</param>
		public CloseNode(string name, IStrategy strategy)
			: base(name, strategy)
		{

		}

		/// <summary>
		/// Дочерние узлы.
		/// </summary>
		/// <remarks>У закрытого узла не дочених узлов. Поэтому возвращаем null.</remarks>
		public sealed override List<Component> Children { get { return null; } }

		/// <summary>
		/// Добавить дочерний узел.
		/// </summary>
		/// <param name="component">Добавляемый узел.</param>
		/// <remarks>
		/// К закрытому узлу нельзя добавлять дочерние элементы.
		/// </remarks>
		public sealed override void Add(Component component)
		{
			throw new Exception("К закрытому узлу нельзя добавлять дочерние элементы.");
		}

		/// <summary>
		/// Удалить выбранный дочерний узел.
		/// </summary>
		/// <param name="component">Удаляемый узел.</param>
		/// <remarks>
		/// У закрытого узла нельзя удалить дочерние элементы.
		/// </remarks>
		public sealed override void Remove(Component component)
		{
			throw new Exception("У закрытого узла нельзя удалить дочерние элементы.");
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
