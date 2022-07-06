namespace SortView
{
    public abstract class Sorting
    {
        protected int[] _array = new int[Size];
        protected const int Size = 70;
        protected DateTime _startTime;
        protected DateTime _endTime;
        protected const int Delay = 0;
        protected const int Space = 2;
        protected char[] _view = new char[Size * (Size * Space + 1)];

        public abstract void Start();

        protected void Fill()
        {
            for (int i = 0; i < _view.Length; i++)
            {
                if (i < _array.Length)
                    _array[i] = i + 1;

                if ((i + 1) % (Size * Space + 1) == 0)
                    _view[i] = '\n';
                else
                    _view[i] = ' ';
            }

            Render();
        }

        protected void Shuffle()
        {
            Random ran = new();

            int count = 1;

            while (count-- > 0)
            {
                int n = _array.Length;

                while (n > 1)
                {
                    int k = ran.Next(n--);

                    RenderSwap(n, k);

                    int temp = _array[n];

                    _array[n] = _array[k];
                    _array[k] = temp;
                }
            }
        }


        protected void Render(int _currentIndex = -1, int delay = Delay)
        {
            Thread.Sleep(delay);

            int j = 0;
            int temp = 0;
            int n = 0;
            for (int i = 0; i < Size; i++)
            {
                j = i * (Size * Space + 1);

                temp = j + Size * Space;

                while (j < temp)
                {
                    if (_array[n++] >= Math.Abs(i - Size))
                    {
                        _view[j] = '█';
                    }
                    else
                    {
                        _view[j] = _view[j] == '\n' ? '\n' : ' ';
                    }

                    j += Space;
                }

                n = 0;
            }

            Console.SetCursorPosition(0, 0);

            Console.WriteLine(_view);
        }

        protected void RenderCurrent(int currentIndex, int value, int delay = Delay)
        {
            Thread.Sleep(delay);

            int c = 0;
            int inc = (Size * Space + 1);

            int j = currentIndex * Space;
            int temp = inc * Size;

            while (j < temp)
            {
                if (value >= Math.Abs(c - Size))
                    _view[j] = '█';
                else
                    _view[j] = ' ';

                j += inc;
                c++;
            }

            Console.SetCursorPosition(0, 0);

            Console.WriteLine(_view);
        }

        protected void RenderTime()
        {
            Console.SetCursorPosition(0, 0);

            using var writer = new StreamWriter("result.txt");

            writer.WriteLine($"Total time: {((_endTime - _startTime).TotalMilliseconds / 1000d):f3} s");
        }


        protected void RenderSwap(int a, int b, int delay = Delay)
        {
            Thread.Sleep(delay);

            int j = 0;
            int temp = 0;
            int c = 0;
            int inc = (Size * Space + 1);

            j = a * Space;

            temp = inc * Size;

            while (j < temp)
            {
                if (_array[b] >= Math.Abs(c - Size))
                    _view[j] = '█';
                else
                    _view[j] = ' ';

                j += inc;
                c++;
            }

            c = 0;

            j = b * Space;

            temp = inc * Size;

            while (j < temp)
            {
                if (_array[a] >= Math.Abs(c - Size))
                    _view[j] = '█';
                else
                    _view[j] = ' ';

                j += inc;
                c++;
            }

            Console.SetCursorPosition(0, 0);

            Console.WriteLine(_view);
        }
    }
}
