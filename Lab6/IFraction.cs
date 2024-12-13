namespace CombinedProject
{
    // Интерфейс для работы с дробями
    public interface IFraction
    {
        double GetRealValue(); // Получение вещественного значения дроби
        void SetNumerator(int numerator); // Установка числителя
        void SetDenominator(int denominator); // Установка знаменателя
    }
}
