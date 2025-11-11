using System;

namespace TestCharacterProject.Models
{
    /// <summary>
    /// Базовый абстрактный класс для всех персонажей в игре.
    /// Сделан абстрактным, чтобы предотвратить создание "безымянных" персонажей.
    /// Все конкретные персонажи (Warrior, Mage) должны наследоваться от этого класса.
    /// </summary>
    public abstract class Character
    {
        // Backing fields для свойств - позволяют контролировать изменение значений
        private int _strength;
        private int _magic;
        private int _health;
        private int _maxHealth;

        /// <summary>
        /// Физическая сила персонажа. Влияет на урон физического оружия.
        /// Не может быть отрицательной (защита от некорректных значений).
        /// </summary>
        public int Strength
        {
            get => _strength;
            set => _strength = Math.Max(0, value); // Не позволяем отрицательную силу
        }

        /// <summary>
        /// Магическая сила персонажа. Влияет на урон магического оружия.
        /// Не может быть отрицательной (защита от некорректных значений).
        /// </summary>
        public int Magic
        {
            get => _magic;
            set => _magic = Math.Max(0, value); // Не позволяем отрицательную магию
        }

        /// <summary>
        /// Текущее здоровье персонажа.
        /// Автоматически ограничивается диапазоном [0, MaxHealth].
        /// </summary>
        public int Health
        {
            get => _health;
            set => _health = Math.Max(0, Math.Min(value, _maxHealth)); // Здоровье в пределах [0, MaxHealth]
        }

        /// <summary>
        /// Максимальное здоровье персонажа.
        /// Определяется при создании и не может быть отрицательным.
        /// </summary>
        public int MaxHealth
        {
            get => _maxHealth;
            private set => _maxHealth = Math.Max(1, value); // Минимум 1 HP
        }

        /// <summary>
        /// Имя класса персонажа (например, "Warrior", "Mage").
        /// Используется для отображения информации.
        /// </summary>
        public string ClassName => GetType().Name;

        /// <summary>
        /// Проверяет, жив ли персонаж (Health > 0).
        /// </summary>
        public bool IsAlive => Health > 0;

        /// <summary>
        /// Конструктор базового класса персонажа.
        /// Защищенный (protected), т.к. напрямую создавать Character нельзя (абстрактный класс).
        /// </summary>
        /// <param name="strength">Начальная сила</param>
        /// <param name="magic">Начальная магия</param>
        /// <param name="health">Начальное и максимальное здоровье</param>
        protected Character(int strength, int magic, int health)
        {
            // Используем свойства, а не поля напрямую, чтобы сработала валидация
            Strength = strength;
            Magic = magic;
            MaxHealth = health;
            Health = health; // Начинаем с полным здоровьем
        }

        /// <summary>
        /// Возвращает детальную информацию о персонаже.
        /// </summary>
        public override string ToString()
        {
            return $"{ClassName}: Сила={Strength}, Магия={Magic}, HP={Health}/{MaxHealth}";
        }
    }
}
