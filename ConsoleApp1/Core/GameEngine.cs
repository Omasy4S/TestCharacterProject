using System;
using System.Collections.Generic;
using TestCharacterProject.Models;
using TestCharacterProject.Weapons;

namespace TestCharacterProject.Core
{
    /// <summary>
    /// Главный игровой движок - управляет игровым процессом.
    /// 
    /// АРХИТЕКТУРНОЕ РЕШЕНИЕ:
    /// - GameEngine - фасад (Facade Pattern) для всей игровой логики
    /// - Инкапсулирует CombatSystem внутри себя
    /// - Предоставляет простой API для управления игрой
    /// - В будущем можно добавить систему квестов, инвентарь и т.д.
    /// 
    /// ПОЧЕМУ НЕ СТАТИЧЕСКИЙ:
    /// - Возможность создать несколько независимых игровых сессий
    /// - Легче тестировать (можно мокировать)
    /// - Поддержка состояния (список персонажей, история боев и т.д.)
    /// </summary>
    public class GameEngine
    {
        // Система боя - делегируем ей всю боевую механику
        private readonly CombatSystem _combatSystem;

        // Коллекция всех персонажей в игре (для возможного расширения)
        private readonly List<Character> _characters;

        /// <summary>
        /// Создает новый экземпляр игрового движка.
        /// Инициализирует все необходимые подсистемы.
        /// </summary>
        public GameEngine()
        {
            _combatSystem = new CombatSystem();
            _characters = new List<Character>();
        }

        /// <summary>
        /// Регистрирует персонажа в игре.
        /// Полезно для отслеживания всех персонажей и их состояния.
        /// </summary>
        public void RegisterCharacter(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character), "Персонаж не может быть null");
            }

            _characters.Add(character);
            Console.WriteLine($"✅ Персонаж {character.ClassName} зарегистрирован в игре");
        }

        /// <summary>
        /// Выполняет атаку. Делегирует работу CombatSystem.
        /// Этот метод - обертка для удобства использования.
        /// </summary>
        public void Attack(Character attacker, IWeapon weapon, Character target)
        {
            _combatSystem.InflictDamage(attacker, weapon, target);
        }

        /// <summary>
        /// Отображает состояние всех зарегистрированных персонажей.
        /// </summary>
        public void DisplayAllCharacters()
        {
            _combatSystem.DisplayCombatStatus(_characters.ToArray());
        }

        /// <summary>
        /// Отображает состояние конкретных персонажей.
        /// </summary>
        public void DisplayStatus(params Character[] characters)
        {
            _combatSystem.DisplayCombatStatus(characters);
        }
    }
}
