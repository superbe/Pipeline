using System;

namespace Pipeline.Core
{
	/// <summary>
	/// Делегат обратного прохода узла после обработки.
	/// </summary>
	/// <param name="sender">Элемент вызвавший обработчик.</param>
	/// <param name="e">Данные события прохождения по элементу трубопровода.</param>
	public delegate void ReverseBypassedDelegate(Object sender, BypassEventArgs e);
}
