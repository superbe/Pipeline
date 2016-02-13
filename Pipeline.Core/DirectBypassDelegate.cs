using System;

namespace Pipeline.Core
{
	/// <summary>
	/// Делегат прямого прохода узла перед обработкой.
	/// </summary>
	/// <param name="sender">Элемент вызвавший обработчик.</param>
	/// <param name="e">Данные события прохождения по элементу трубопровода.</param>
	public delegate void DirectBypassDelegate(Object sender, BypassEventArgs e);
}
