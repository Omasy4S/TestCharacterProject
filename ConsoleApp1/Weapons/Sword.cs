using TestCharacterProject.Models;

namespace TestCharacterProject.Weapons
{
    /// <summary>
    /// Меч - классическое физическое оружие.
    /// Урон зависит только от силы атакующего.
    /// Идеально подходит для воинов с высокой силой.
    /// </summary>
    public class Sword : IWeapon
    {
        public string Name => "Меч";

        /// <summary>
        /// Рассчитывает урон меча.
        /// Формула: Сила × 3
        /// Множитель 3 делает меч мощным оружием для воинов (5 силы = 15 урона).
        /// </summary>
        public int CalculateDamage(Character attacker)
        {
            return attacker.Strength * 3;
        }
    }
}
