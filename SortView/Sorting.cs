namespace SortView
{
    public abstract class Sorting
    {
        private const char _columnChar = '█';
        protected const int Delay = 0;
        protected int[] _array;
        protected readonly int Size;
        protected readonly int Space;
        protected char[] _view;

        public Sorting(int size, int space = 2)
        {
            Size = size;
            Space = space;
            _array = new int[Size];
            _view = new char[Size * (Size * Space + 1)];
        }

        public abstract void Start(SortingAlgorithms algorithms);

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
                        _view[j] = _columnChar;
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
                    _view[j] = _columnChar;
                else
                    _view[j] = ' ';

                j += inc;
                c++;
            }

            Console.SetCursorPosition(0, 0);

            Console.WriteLine(_view);
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
                    _view[j] = _columnChar;
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
                    _view[j] = _columnChar;
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
