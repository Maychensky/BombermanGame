namespace Bomberman.Enemy
{
    internal class Ballom : Enemy
    {
        const int POINTS_FOR_KILLS = 100;
        const bool IS_PASSES_THROUGH_WALLS = false; 
        internal Ballom () : base(TypeEnemy.Balloom, POINTS_FOR_KILLS, IntensityScale.Weak, IntensityScale.Weak, IS_PASSES_THROUGH_WALLS) { }
        protected override void PathAlgotithm() 
        {
            

            // ПУСКАЙ ИЩЕТ В ПРИДЕЛАХ ЧЕТВЕРТЕЙ КООРДИНАТ
            // ПОСМОТРЕТЬ ПУТИ ВЫХОДЫ
            // ОТНОСИТЕЛЬНО НИХ ВЫБРАТЬ ЧЕТВЕРТИ ДЛЯ ДВИЖЕНИЯ
            // В ВЫБРАННОЙ ЧЕТВЕРТИ ВЫБРАТЬ РАНДОМНЫЙ КВАДРАТ
            // В НЕМ СГЕНЕРИТЬ ПУТЬ 
            // И ПО НОВОЙ

            // движение будет зависить от характеристик персонажа
            // скороть и агрессия , возможность пролходить сквось стены
        }
    }
}