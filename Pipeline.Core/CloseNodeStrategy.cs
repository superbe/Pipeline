﻿namespace Pipeline.Core
{
	/// <summary>
	/// Обработка прохода закрытого узла.
	/// </summary>
	public class CloseNodeStrategy : IStrategy
	{
		/// <summary>
		/// Компонент, над которым происходит обработка прохода.
		/// </summary>
		private Component _component;

		/// <summary>
		/// Задать компонент, над которым будет происходить рассчет.
		/// </summary>
		/// <param name="component">Компонент, над которым происходит обработка прохода.</param>
		public void SetComponent(Component component)
		{
			_component = component;
		}

		/// <summary>
		/// Обработка прямого прохода.
		/// </summary>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public BypassedArgument DirectBypass(BypassedArgument args)
		{
			args.Add(_component.Name, _component.Level);
			args = _component.Parent.DirectBypass(args);
			return args;
		}

		/// <summary>
		/// Обработка обратного прохода с поднятием данных для прямого прохода.
		/// </summary>
		/// <remarks>Метод необходим для обработки дочерних узлов в прямом проходе,
		/// для соблюдения порядка вложенности.</remarks>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public BypassedArgument RiseDirectBypass(BypassedArgument args)
		{
			args.Add(_component.Name, _component.Level);
			return args;
		}

		/// <summary>
		/// Обработка обратного прохода.
		/// </summary>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public BypassedArgument ReverseBypass(BypassedArgument args)
		{
			args.Add(_component.Name, _component.Level);
			return args;
		}
	}
}
