using System;
using System.Collections.Generic;

namespace CombinedProject
{
    // Класс для кеширования вещественных значений дробей
    public class FractionCache
    {
        private readonly Dictionary<(int, int), double> _cache = new Dictionary<(int, int), double>(); // Словарь для хранения кеша

        public double GetRealValue(Fraction fraction)
        {
            var key = (fraction.Numerator, fraction.Denominator);
            if (!_cache.TryGetValue(key, out double realValue))
            {
                realValue = (double)fraction.Numerator / fraction.Denominator;
                _cache[key] = realValue; // Сохранение значения в кеш
            }
            return realValue;
        }

        public void Invalidate(Fraction fraction)
        {
            var key = (fraction.Numerator, fraction.Denominator);
            _cache.Remove(key); // Удаление значения из кеша
        }
    }
}
