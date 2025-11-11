using TestCharacterProject.Models;

namespace TestCharacterProject.Weapons
{
    /// <summary>
    /// Магический посох - оружие для магов.
    /// Урон зависит от магической силы атакующего.
    /// Имеет базовый урон +2, поэтому даже персонажи без магии нанесут минимальный урон.
    /// </summary>
    public class MagicStaff : IWeapon
    {
        public string Name => "Магический посох";

        /// <summary>
        /// Рассчитывает урон магического посоха.
        /// Формула: (Магия × 2) + 2
        /// Базовый урон +2 гарантирует минимальный урон даже без магии.
        /// Для мага с магией 2: (2 × 2) + 2 = 6 урона.
        /// </summary>
        public int CalculateDamage(Character attacker)
        {
            return attacker.Magic * 2 + 2;
        }
    }
}
