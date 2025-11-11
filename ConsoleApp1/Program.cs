using System;
using TestCharacterProject.Core;
using TestCharacterProject.Models;
using TestCharacterProject.Weapons;

namespace TestCharacterProject
{
    /// <summary>
    /// Точка входа в приложение - демонстрирует работу игровой системы.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Настраиваем консоль для корректного отображения эмодзи и кириллицы
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║   🎮 СИСТЕМА БОЕВОЙ МЕХАНИКИ RPG v2.0 🎮    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            Console.WriteLine();

            // Создаём игровой движок (фасад для всей игровой логики)
            var engine = new GameEngine();

            // ═══════════════════════════════════════════════════════
            // СОЗДАНИЕ ПЕРСОНАЖЕЙ
            // ═══════════════════════════════════════════════════════
            Console.WriteLine("📝 ИНИЦИАЛИЗАЦИЯ ПЕРСОНАЖЕЙ");
            Console.WriteLine("═══════════════════════════════════════");

            var warrior = new Warrior();
            var mage = new Mage();

            // Регистрируем персонажей в движке
            engine.RegisterCharacter(warrior);
            engine.RegisterCharacter(mage);

            Console.WriteLine();

            // ═══════════════════════════════════════════════════════
            // СОЗДАНИЕ ОРУЖИЯ
            // ═══════════════════════════════════════════════════════
            Console.WriteLine("⚔️  АРСЕНАЛ ОРУЖИЯ");
            Console.WriteLine("═══════════════════════════════════════");

            var sword = new Sword();
            var magicStaff = new MagicStaff();
            var foolStaff = new FoolStaff();

            Console.WriteLine($"✓ {sword.Name} - физическое оружие (урон = Сила × 3)");
            Console.WriteLine($"✓ {magicStaff.Name} - магическое оружие (урон = Магия × 2 + 2)");
            Console.WriteLine($"✓ {foolStaff.Name} - хаотичное оружие (меняет характеристики мага, урон 0-10)");
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════
            // НАЧАЛЬНОЕ СОСТОЯНИЕ
            // ═══════════════════════════════════════════════════════
            engine.DisplayStatus(warrior, mage);

            // ═══════════════════════════════════════════════════════
            // БОЙ 1: Воин атакует мага мечом
            // ═══════════════════════════════════════════════════════
            Console.WriteLine("🎯 РАУНД 1: Воин с мечом VS Маг");
            Console.WriteLine();
            engine.Attack(warrior, sword, mage);

            // ═══════════════════════════════════════════════════════
            // БОЙ 2: Маг атакует воина магическим посохом
            // ═══════════════════════════════════════════════════════
            Console.WriteLine("🎯 РАУНД 2: Маг с посохом VS Воин");
            Console.WriteLine();
            engine.Attack(mage, magicStaff, warrior);

            // ═══════════════════════════════════════════════════════
            // БОЙ 3: Маг атакует воина жезлом дурака (1-я атака)
            // ВАЖНО: Характеристики мага изменятся!
            // ═══════════════════════════════════════════════════════
            Console.WriteLine("🎯 РАУНД 3: Маг с Жезлом Дурака VS Воин (1-я атака)");
            Console.WriteLine("⚠️  ВНИМАНИЕ: Жезл Дурака изменит характеристики мага!");
            Console.WriteLine();
            engine.Attack(mage, foolStaff, warrior);

            // ═══════════════════════════════════════════════════════
            // БОЙ 4: Маг снова атакует жезлом дурака (2-я атака)
            // ВАЖНО: Характеристики вернутся обратно!
            // ═══════════════════════════════════════════════════════
            Console.WriteLine("🎯 РАУНД 4: Маг с Жезлом Дурака VS Воин (2-я атака)");
            Console.WriteLine("⚠️  ВНИМАНИЕ: Характеристики вернутся к исходным!");
            Console.WriteLine();
            engine.Attack(mage, foolStaff, warrior);

            // ═══════════════════════════════════════════════════════
            // ФИНАЛЬНОЕ СОСТОЯНИЕ
            // ═══════════════════════════════════════════════════════
            Console.WriteLine("🏁 ИТОГОВОЕ СОСТОЯНИЕ ПОСЛЕ БОЕВ");
            Console.WriteLine();
            engine.DisplayAllCharacters();

            // Ждем нажатия клавиши перед закрытием
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}