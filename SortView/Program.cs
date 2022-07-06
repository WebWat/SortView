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

        //_startTime = DateTime.UtcNow;
        BeadSort(_array);
        //_endTime = DateTime.UtcNow;

        Render();
        //RenderTime();
    }

    private void BeadSort(int[] data)
    {
        int i, j, max, sum;
        byte[] beads;

        for (i = 1, max = data[0]; i < data.Length; ++i)
            if (data[i] > max)
                max = data[i];

        beads = new byte[max * data.Length];

        for (i = 0; i < data.Length; ++i)
        {
            for (j = 0; j < data[i]; ++j)
            {
                beads[i * max + j] = 1;
            }
        }

        for (j = 0; j < max; ++j)
        {
            for (sum = i = 0; i < data.Length; ++i)
            {
                sum += beads[i * max + j];

                beads[i * max + j] = 0;
            }

            for (i = data.Length - sum; i < data.Length; ++i)
                beads[i * max + j] = 1;
        }

        for (i = 0; i < data.Length; ++i)
        {
            for (j = 0; j < max && Convert.ToBoolean(beads[i * max + j]); ++j)
            {
                RenderCurrent(i, j);
                data[i] = j;
            }
        }
    }

    private void EvenOddSort(int[] array, int length)
    {
        bool isSorted = false;

        while (!isSorted)
        {
            isSorted = true;

            //Swap i and i+1 if they are out of order, for i == odd numbers
            for (int i = 1; i <= length - 2; i = i + 2)
            {
                if (array[i] > array[i + 1])
                {
                    int temp = array[i];
                    RenderSwap(i, i + 1);
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                    isSorted = false;
                }
            }

            //Swap i and i+1 if they are out of order, for i == even numbers
            for (int i = 0; i <= length - 2; i = i + 2)
            {
                if (array[i] > array[i + 1])
                {
                    RenderSwap(i, i + 1);

                    int temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                    isSorted = false;
                }
            }
        }
        return;
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