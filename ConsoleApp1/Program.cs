using System;

public class GameEngine
{
    public abstract class Character
    {
        public int Strength { get; set; }
        public int Magic { get; set; }
        public int Health { get; set; }

        public Character(int strength, int magic, int health)
        {
            Strength = strength;
            Magic = magic;
            Health = health;
        }
    }
    public class Warrior : Character
    {
        public Warrior() : base(5, 0, 100) { }
    }

    public class Mage : Character
    {
        public Mage() : base(1, 2, 50) { }
    }
    public interface IWeapon
    {
        int CalcDamage(Character attack);
    }
    public class Sword : IWeapon
    {
        public int CalcDamage(Character attack) => attack.Strength * 3;
    }
    public class MagicStaff : IWeapon
    {
        public int CalcDamage(Character attack) => attack.Magic * 2 + 2;
    }
    public class FoolStaff : IWeapon
    {
        public int CalcDamage(Character attacker)
        {
            // Если это маг — меняем силу и магию местами
            if (attacker is Mage)
            {
                // Меняем местами характеристики
                var temp = attacker.Strength;
                attacker.Strength = attacker.Magic;
                attacker.Magic = temp;
            }

            // Генерируем случайный урон от 0 до 10
            Random random = new Random();
            int damage = random.Next(0, 11); // 0..10 включительно

            return damage;
        }
    }
    public void InflictDamage(Character attack, IWeapon weapon, Character target)
    {
        int Damage = weapon.CalcDamage(attack);
        Console.WriteLine("Начало битвы\n==========");
        Console.WriteLine($"Нанесено Урона {Damage}");
        target.Health = Math.Max(0,target.Health - Damage);
    }

    public static void Main(string[] args)
    {
        var engine = new GameEngine();

        // Создаём персонажей
        var warrior = new Warrior();
        var mage = new Mage();

        // Создаём оружие
        var sword = new Sword();
        var staff = new MagicStaff();
        var foolStaff = new FoolStaff();

        //Тестовый код для проверок

        // Воин атакует мага мечом
        engine.InflictDamage(warrior, sword, mage);
        WtiteInfo(warrior, mage);

        // Маг атакует воина посохом
        engine.InflictDamage(mage, staff, warrior);
        WtiteInfo(mage, warrior);

        // Маг атакует воина жезлом дурака (смена характеристик + случайный урон)
        engine.InflictDamage(mage, foolStaff, warrior);
        WtiteInfo(mage, warrior);

        // Повторная атака жезлом дурака (смена характеристик + случайный урон)
        engine.InflictDamage(mage, foolStaff, warrior);
        WtiteInfo(mage, warrior);

    }
    static void WtiteInfo(Character characterInf, Character targetInf)
    {
        
        Console.WriteLine("Атакующий");
        Console.WriteLine(characterInf.Strength);
        Console.WriteLine(characterInf.Magic);
        Console.WriteLine(characterInf.Health);
        Console.WriteLine("\nЦель");
        Console.WriteLine(targetInf.Strength);
        Console.WriteLine(targetInf.Magic);
        Console.WriteLine(targetInf.Health);

        Console.WriteLine("==========\nКонец битвы\n");
    }
}