using System;

namespace Pipeline.Core
{
	/// <summary>
	/// Делегат прямого прохода узла после обработки.
	/// </summary>
	/// <param name="sender">Элемент вызвавший обработчик.</param>
	/// <param name="e">Данные события прохождения по элементу трубопровода.</param>
	public delegate void DirectBypassedDelegate(Object sender, BypassEventArgs e);
}
