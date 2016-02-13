using System;

namespace Pipeline.Core
{
	/// <summary>
	/// Данные события прохождения по элементу трубопровода.
	/// </summary>
	public class BypassEventArgs : EventArgs
	{
		/// <summary>
		/// Наименование элемента.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Порядковое число прохода.
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// Аргумент прохода.
		/// </summary>
		public BypassedArgument Args { get; set; }

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="name">Наименование элемента.</param>
		/// <param name="number">Порядковое число прохода.</param>
		/// <param name="args">Аргументы прохода.</param>
		public BypassEventArgs(string name, int number, BypassedArgument args)
		{
			Name = name;
			Number = number;
			Args = args;
		}
	}
}
