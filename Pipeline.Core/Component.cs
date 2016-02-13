using System.Collections.Generic;

namespace Pipeline.Core
{
	/// <summary>
	/// Component - объявляет интерфейс для компонуемых объектов;
	/// </summary>
	public abstract class Component
	{
		/// <summary>
		/// Событие прямого прохода перед обработкой узла.
		/// </summary>
		public event DirectBypassDelegate Direct;

		/// <summary>
		/// Событие прямого прохода после обработки узла.
		/// </summary>
		public event DirectBypassedDelegate Directed;

		/// <summary>
		/// Событие обратного прохода перед обработкой узла.
		/// </summary>
		public event ReverseBypassDelegate Reverse;

		/// <summary>
		/// Событие обратного прохода после обработки узла.
		/// </summary>
		public event ReverseBypassedDelegate Reversed;

		/// <summary>
		/// Обработчик обратного прохода перед обработкой узла.
		/// </summary>
		/// <param name="e">Данные события прохождения по элементу трубопровода.</param>
		protected virtual void OnReverseBypass(BypassEventArgs e)
		{
			ReverseBypassDelegate handler = Reverse;
			if (handler != null) handler(this, e);
		}

		/// <summary>
		/// Обработчик прямого прохода перед обработкой узла.
		/// </summary>
		/// <param name="e">Данные события прохождения по элементу трубопровода.</param>
		protected virtual void OnDirectBypass(BypassEventArgs e)
		{
			DirectBypassDelegate handler = Direct;
			if (handler != null) handler(this, e);
		}

		/// <summary>
		/// Обработчик обратного прохода после обработки узла.
		/// </summary>
		/// <param name="e">Данные события прохождения по элементу трубопровода.</param>
		protected virtual void OnReverseBypassed(BypassEventArgs e)
		{
			ReverseBypassedDelegate handler = Reversed;
			if (handler != null) handler(this, e);
		}

		/// <summary>
		/// Обработчик прямого прохода после обработки узла.
		/// </summary>
		/// <param name="e">Данные события прохождения по элементу трубопровода.</param>
		protected virtual void OnDirectBypassed(BypassEventArgs e)
		{
			DirectBypassedDelegate handler = Directed;
			if (handler != null) handler(this, e);
		}

		/// <summary>
		/// Родительский элемент.
		/// </summary>
		protected internal Component Parent;

		/// <summary>
		/// Уровень вложенности, используется для прямого и обратного прохода.
		/// </summary>
		/// <remarks>TODO: сделать пересчет уровня узла после полного 
		/// построения трубопровода.</remarks>
		protected internal int Level;

		/// <summary>
		/// Наименование компонента (должно быть уникальным).
		/// </summary>
		protected internal string Name;

		/// <summary>
		/// Обработчик прохождения элемента.
		/// </summary>
		protected IStrategy _strategy;

		/// <summary>
		/// Дочерние элементы.
		/// </summary>
		public abstract List<Component> Children { get; }

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="name">Название узла.</param>
		/// <param name="strategy">Обработчик прохождения элемента.</param>
		protected Component(string name, IStrategy strategy)
		{
			Name = name;
			SetStrategy(strategy);
		}

		/// <summary>
		/// Задать обработчик прохождения элемента.
		/// </summary>
		/// <param name="strategy">Обработчик прохождения элемента.</param>
		public void SetStrategy(IStrategy strategy)
		{
			_strategy = strategy;
			_strategy.SetComponent(this);
		}

		/// <summary>
		/// Добавить новый дочерний узел.
		/// </summary>
		/// <param name="component">Добавляемый узел.</param>
		public abstract void Add(Component component);

		/// <summary>
		/// Удалить выбранный дочерний узел.
		/// </summary>
		/// <param name="component">Удаляемый узел.</param>
		public abstract void Remove(Component component);

		/// <summary>
		/// Проход вперед.
		/// </summary>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public abstract BypassedArgument DirectBypass(BypassedArgument args);

		/// <summary>
		/// Обратный проход с подъемом.
		/// </summary>
		/// <remarks>Метод необходим для обработки дочерних узлов в прямом проходе,
		/// для соблюдения порядка вложенности.</remarks>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public abstract BypassedArgument RiseDirectBypass(BypassedArgument args);

		/// <summary>
		/// Обратный проход.
		/// </summary>
		/// <param name="args">Аргумент прохода по трубопроводу.</param>
		/// <returns>Аргумент прохода по трубопроводу.</returns>
		public abstract BypassedArgument ReverseBypass(BypassedArgument args);

		/// <summary>
		/// Задать родительский элемент.
		/// </summary>
		/// <param name="parent">Родительский узел.</param>
		public void SetParent(Component parent)
		{
			Parent = parent;
		}

		/// <summary>
		/// Задать уровень элемента.
		/// </summary>
		/// <param name="level">Уровень вложенности.</param>
		public void SetLevel(int level)
		{
			Level = level;
		}
	}
}
