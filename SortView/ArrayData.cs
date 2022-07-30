namespace SortView
{
    public class ArrayData : Sorting
    {
        public ArrayData(int size) : base(size)
        {
        }

        public ArrayData(int size, int space) : base(size, space)
        {
        }

        public override void Start(SortingAlgorithms algorithms)
        {
            Console.CursorVisible = false;

#pragma warning disable CA1416 // Checking platform compatibility
            Console.SetWindowSize(Size * Space, Size + 3);
#pragma warning restore CA1416 // Checking platform compatibility

            Fill();
            Thread.Sleep(1000);

            Shuffle();
            Thread.Sleep(1000);

            switch (algorithms)
            {
                case SortingAlgorithms.BubbleSort:
                    BubbleSort();
                    break;
                case SortingAlgorithms.BeadSort:
                    BeadSort();
                    break;
                case SortingAlgorithms.EvenOddSort:
                    EvenOddSort();
                    break;
                case SortingAlgorithms.QuickSort:
                    QuickSort(_array, 0, _array.Length - 1);
                    break;
                case SortingAlgorithms.RadixSort:
                    RadixSort();
                    break;
                case SortingAlgorithms.ShellSort:
                    ShellSort();
                    break;
                case SortingAlgorithms.SelectionSort:
                    SelectionSort();
                    break;
                case SortingAlgorithms.HeapSort:
                    HeapSort();
                    break;
                case SortingAlgorithms.InsertionSort:
                    InsertionSort();
                    break;
            }
            Render();
        }

        private void BeadSort()
        {
            int i, j, max, sum;
            byte[] beads;

            for (i = 1, max = _array[0]; i < _array.Length; ++i)
                if (_array[i] > max)
                    max = _array[i];

            beads = new byte[max * _array.Length];

            for (i = 0; i < _array.Length; ++i)
            {
                for (j = 0; j < _array[i]; ++j)
                {
                    beads[i * max + j] = 1;
                }
            }

            for (j = 0; j < max; ++j)
            {
                for (sum = i = 0; i < _array.Length; ++i)
                {
                    sum += beads[i * max + j];

                    beads[i * max + j] = 0;
                }

                for (i = _array.Length - sum; i < _array.Length; ++i)
                    beads[i * max + j] = 1;
            }

            for (i = 0; i < _array.Length; ++i)
            {
                for (j = 0; j < max && Convert.ToBoolean(beads[i * max + j]); ++j)
                {
                    RenderCurrent(i, j);
                    _array[i] = j;
                }
            }
        }

        private void EvenOddSort()
        {
            bool isSorted = false;

            while (!isSorted)
            {
                isSorted = true;

                for (int i = 1; i <= _array.Length - 2; i += 2)
                {
                    if (_array[i] > _array[i + 1])
                    {
                        int temp = _array[i];
                        RenderSwap(i, i + 1);
                        _array[i] = _array[i + 1];
                        _array[i + 1] = temp;
                        isSorted = false;
                    }
                }

                for (int i = 0; i <= _array.Length - 2; i += 2)
                {
                    if (_array[i] > _array[i + 1])
                    {
                        RenderSwap(i, i + 1);

                        int temp = _array[i];
                        _array[i] = _array[i + 1];
                        _array[i + 1] = temp;
                        isSorted = false;
                    }
                }
            }
        }

        private void QuickSort(int[] arr, int left, int right)
        {
            int Partition(int[] arr, int left, int right)
            {
                int pivot;
                pivot = arr[left];
                while (true)
                {
                    while (arr[left] < pivot)
                    {
                        left++;
                    }
                    while (arr[right] > pivot)
                    {
                        right--;
                    }
                    if (left < right)
                    {
                        RenderSwap(right, left);
                        int temp = arr[right];
                        arr[right] = arr[left];
                        arr[left] = temp;
                    }
                    else
                    {
                        return right;
                    }
                }
            }

            int pivot;

            if (left < right)
            {
                pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, pivot + 1, right);
                }
            }
        }

        private void BubbleSort()
        {
            int temp;
            for (int i = 0; i < _array.Length - 1; i++)
            {
                for (int j = i + 1; j < _array.Length; j++)
                {
                    if (_array[i] > _array[j])
                    {
                        RenderSwap(i, j);

                        temp = _array[i];
                        _array[i] = _array[j];
                        _array[j] = temp;
                    }
                }
            }
        }

        private void RadixSort()
        {
            int i, j;
            int[] tmp = new int[_array.Length];

            for (int shift = 31; shift > -1; --shift)
            {
                j = 0;

                for (i = 0; i < _array.Length; ++i)
                {
                    bool move = (_array[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                    {
                        RenderCurrent(i - j, _array[i]);

                        _array[i - j] = _array[i];
                    }
                    else
                    {
                        tmp[j++] = _array[i];
                    }
                }


                for (i = 0; i < j; i++)
                {
                    RenderCurrent(_array.Length - j + i, tmp[i]);
                    _array[_array.Length - j + i] = tmp[i];
                }
            }
        }

        private void ShellSort()
        {
            void Swap(ref int a, ref int b)
            {
                var t = a;
                a = b;
                b = t;
            }

            var d = _array.Length / 2;

            while (d >= 1)
            {
                for (var i = d; i < _array.Length; i++)
                {
                    var j = i;

                    while ((j >= d) && (_array[j - d] > _array[j]))
                    {
                        RenderSwap(j, j - d);

                        Swap(ref _array[j], ref _array[j - d]);
                        j -= d;
                    }
                }

                d /= 2;
            }
        }

        private void SelectionSort()
        {
            for (var i = 0; i < _array.Length; i++)
            {
                var min = i;

                for (var j = i + 1; j < _array.Length; j++)
                {
                    Render(j * Space);

                    if (_array[min] > _array[j])
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    Render(min * Space);
                    var lowerValue = _array[min];
                    _array[min] = _array[i];
                    _array[i] = lowerValue;
                }
            }
        }

        private void HeapSort()
        {
            int n = _array.Length;

            void Heapify(int[] arr, int n, int i)
            {
                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                if (left < n && arr[left] > arr[largest])
                    largest = left;
                if (right < n && arr[right] > arr[largest])
                    largest = right;
                if (largest != i)
                {
                    RenderSwap(i, largest);

                    int swap = arr[i];
                    arr[i] = arr[largest];
                    arr[largest] = swap;
                    Heapify(arr, n, largest);
                }
            }

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(_array, n, i);
            for (int i = n - 1; i >= 0; i--)
            {
                RenderSwap(0, i);
                int temp = _array[0];
                _array[0] = _array[i];
                _array[i] = temp;
                Heapify(_array, i, 0);
            }
        }

        private void InsertionSort()
        {
            for (int i = 0; i < _array.Count(); i++)
            {
                var item = _array[i];
                var currentIndex = i;
                Render(i * Space);

                while (currentIndex > 0 && _array[currentIndex - 1] > item)
                {
                    _array[currentIndex] = _array[currentIndex - 1];
                    currentIndex--;

                    Render(currentIndex * Space);
                }

                _array[currentIndex] = item;
            }
        }
    }
}
