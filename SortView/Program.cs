using System.Text;
using SortView;

var a = new ArrayData();

a.Start();

Console.ReadKey(true);

public class ArrayData : Sorting
{
    public override void Start()
    {
        Console.CursorVisible = false;

#pragma warning disable CA1416 // Проверка совместимости платформы
        Console.SetWindowSize(Size * Space, Size + 3);
#pragma warning restore CA1416 // Проверка совместимости платформы

        Fill();

        Thread.Sleep(1000);

        Shuffle();

        Thread.Sleep(1000);

        _startTime = DateTime.UtcNow;
        ShellSort();
        _endTime = DateTime.UtcNow;

        Render();
        RenderTime();
    }

    static public int Partition(int[] arr, int left, int right)
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

    private void Bubble()
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
    private void HeapSort(int[] arr, int n)
    {
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
            Heapify(arr, n, i);
        for (int i = n - 1; i >= 0; i--)
        {
            RenderSwap(0, i);
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            Heapify(arr, i, 0);
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