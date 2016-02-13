using System.Collections.Generic;

namespace Pipeline.Core
{
	/// <summary>
	/// Аргумент прохода по трубопроводу.
	/// </summary>
	public class BypassedArgument
	{
		/// <summary>
		/// Трассировка прохода.
		/// </summary>
		public Dictionary<string, int> TransportTrace { get; set; }

		/// <summary>
		/// Путь прохода.
		/// </summary>
		public string Path
		{
			get
			{
				return string.Join("/", TransportTrace.Keys);
			}
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		public BypassedArgument()
		{
			TransportTrace = new Dictionary<string, int>();
		}

		/// <summary>
		/// Добавить наименование текущего узла.
		/// </summary>
		/// <param name="name">Наименование текущего узла.</param>
		/// <param name="level">Уровень текущего узла.</param>
		public void Add(string name, int level)
		{
			TransportTrace.Add(name, level);
		}

		/// <summary>
		/// Добавить массив узлов.
		/// </summary>
		/// <param name="trace">Массив узлов.</param>
		public void AddRange(Dictionary<string, int> trace)
		{
			foreach (KeyValuePair<string, int> item in trace)
				TransportTrace.Add(item.Key, item.Value);
		}
	}
}
