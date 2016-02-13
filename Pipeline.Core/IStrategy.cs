namespace Pipeline.Core
{
	/// <summary>
	/// Интерфейс стратегии обрабтки обхода узла.
	/// </summary>
	public interface IStrategy
	{
		/// <summary>
		/// Задать объект стратегии.
		/// </summary>
		/// <param name="component">Узел.</param>
		void SetComponent(Component component);

		/// <summary>
		/// Прямой проход.
		/// </summary>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		BypassedArgument DirectBypass(BypassedArgument args);

		/// <summary>
		/// Обратный проход с подъемом.
		/// </summary>
		/// <remarks>Метод необходим для обработки дочерних узлов в прямом проходе,
		/// для соблюдения порядка вложенности.</remarks>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		BypassedArgument RiseDirectBypass(BypassedArgument args);

		/// <summary>
		/// Обратный проход.
		/// </summary>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		BypassedArgument ReverseBypass(BypassedArgument args);
	}
}
