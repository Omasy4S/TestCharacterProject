using System;
using TestCharacterProject.Models;
using TestCharacterProject.Weapons;

namespace TestCharacterProject.Core
{
    /// <summary>
    /// Система боя - отвечает за нанесение урона и логику сражений.
    /// 
    /// ПОЧЕМУ ОТДЕЛЬНЫЙ КЛАСС:
    /// - Разделение ответственности (Single Responsibility Principle)
    /// - GameEngine управляет общим состоянием игры
    /// - CombatSystem занимается только боевой механикой
    /// - Легко тестировать и расширять
    /// </summary>
    public class CombatSystem
    {
        /// <summary>
        /// Выполняет атаку одного персонажа на другого с использованием оружия.
        /// 
        /// ПРОЦЕСС АТАКИ:
        /// 1. Проверяем, что оба персонажа живы
        /// 2. Сохраняем характеристики атакующего (для отображения изменений)
        /// 3. Рассчитываем урон через оружие (может изменить характеристики!)
        /// 4. Наносим урон цели
        /// 5. Выводим детальную информацию о бое
        /// </summary>
        /// <param name="attacker">Персонаж, который атакует</param>
        /// <param name="weapon">Оружие, которым атакуют</param>
        /// <param name="target">Персонаж, который получает урон</param>
        public void InflictDamage(Character attacker, IWeapon weapon, Character target)
        {
            // Валидация: проверяем, что персонажи живы
            if (!attacker.IsAlive)
            {
                Console.WriteLine($"❌ {attacker.ClassName} мертв и не может атаковать!");
                return;
            }

            if (!target.IsAlive)
            {
                Console.WriteLine($"❌ {target.ClassName} уже мертв!");
                return;
            }

            Console.WriteLine("⚔️  НАЧАЛО АТАКИ");
            Console.WriteLine("═══════════════════════════════════════");

            // Сохраняем характеристики ДО атаки для отображения изменений
            int strengthBefore = attacker.Strength;
            int magicBefore = attacker.Magic;

            // Рассчитываем урон (ВНИМАНИЕ: может изменить характеристики атакующего!)
            int damage = weapon.CalculateDamage(attacker);

            // Проверяем, изменились ли характеристики атакующего
            bool statsChanged = (strengthBefore != attacker.Strength) || (magicBefore != attacker.Magic);

            // Отображаем информацию об атаке
            Console.WriteLine($"🗡️  {attacker.ClassName} атакует оружием: {weapon.Name}");
            
            if (statsChanged)
            {
                Console.WriteLine($"✨ ХАРАКТЕРИСТИКИ ИЗМЕНЕНЫ!");
                Console.WriteLine($"   Сила: {strengthBefore} → {attacker.Strength}");
                Console.WriteLine($"   Магия: {magicBefore} → {attacker.Magic}");
            }

            Console.WriteLine($"💥 Нанесено урона: {damage}");

            // Применяем урон к цели (свойство Health автоматически ограничит значение)
            int healthBefore = target.Health;
            target.Health -= damage;
            int actualDamage = healthBefore - target.Health;

            Console.WriteLine($"🎯 Цель: {target.ClassName}");
            Console.WriteLine($"   HP: {healthBefore} → {target.Health} (-{actualDamage})");

            // Проверяем, убита ли цель
            if (!target.IsAlive)
            {
                Console.WriteLine($"💀 {target.ClassName} повержен!");
            }

            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine();
        }

        /// <summary>
        /// Выводит подробную информацию о состоянии всех персонажей.
        /// Полезно для отладки и понимания текущей ситуации в бою.
        /// </summary>
        /// <param name="characters">Массив персонажей для отображения</param>
        public void DisplayCombatStatus(params Character[] characters)
        {
            Console.WriteLine("📊 ТЕКУЩЕЕ СОСТОЯНИЕ ПЕРСОНАЖЕЙ");
            Console.WriteLine("═══════════════════════════════════════");

            foreach (var character in characters)
            {
                string status = character.IsAlive ? "✅ ЖИВ" : "💀 МЕРТВ";
                Console.WriteLine($"{status} | {character}");
            }

            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine();
        }
    }
}
