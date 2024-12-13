using System;

namespace CombinedProject
{
    // Класс-обертка для отслеживания количества мяуканий
    public class MeowCounter : IMeowable
    {
        private readonly IMeowable _meowable;
        public int MeowCount { get; private set; }

        public MeowCounter(IMeowable meowable)
        {
            _meowable = meowable;
            MeowCount = 0;
        }

        public void Meow()
        {
            _meowable.Meow();
            MeowCount++;
        }

        public void Meow(int times)
        {
            _meowable.Meow(times);
            MeowCount += times;
        }
    }
}
