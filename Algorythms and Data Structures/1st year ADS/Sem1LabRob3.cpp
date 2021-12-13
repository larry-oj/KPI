// Реализация пирамидальной сортировки на C++
#include <iostream>
#include <cstdlib>
#include <ctime> 
#include <time.h> 
#define N1 240
#define N2 2400
#define N3 24000
using namespace std;

void heapify(int arr[], int n, int i)
{
    int largest = i;
    int l = 2 * i + 1;
    int r = 2 * i + 2;
    if (l < n && arr[l] > arr[largest])
        largest = l;
    if (r < n && arr[r] > arr[largest])
        largest = r;
    if (largest != i)
    {
        swap(arr[i], arr[largest]);
        heapify(arr, n, largest);
    }
}

void heapSort(int arr[], int n)
{
    for (int i = n / 2 - 1; i >= 0; i--)
        heapify(arr, n, i);
    for (int i = n - 1; i >= 0; i--)
    {
        swap(arr[0], arr[i]);
        heapify(arr, i, 0);
    }
}

/* Вспомогательная функция для вывода на экран массива размера n*/
void printArray(int arr[], int n)
{
    for (int i = 0; i < n; ++i)
        cout << arr[i] << " ";
    cout << "\n";
}
const int n = 24;
int first, last;
//функция сортировки
int partition(int arr[], int low, int high)
{
    int pivot = arr[high];
    int i = (low - 1), temp;

    for (int j = low; j <= high - 1; j++)
    {
        if (arr[j] <= pivot)
        {
            i++;
            temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
    temp = arr[i + 1];
    arr[i + 1] = arr[high];
    arr[high] = temp;
    return (i + 1);
}
void quickSort(int arr[], int low, int high)
{
    if (low < high)
    {
        int pi = partition(arr, low, high);

        quickSort(arr, low, pi - 1);
        quickSort(arr, pi + 1, high);
    }
}
void Merge(int* A, int first, int last) {
    int middle, start, final, j;
    int* mas = new int[100];
    middle = (first + last) / 2;  //вычисление среднего элемента
    start = first;                //начало левой части
    final = middle + 1;           //начало правой части
    for (j = first; j <= last; j++)  //выполнять от начала до конца
        if ((start <= middle) && ((final > last) || (A[start] < A[final]))) {
            mas[j] = A[start];
            start++;
        }
        else {
            mas[j] = A[final];
            final++;
        }
    //возвращение результата в список
    for (j = first; j <= last; j++)
        A[j] = mas[j];
    delete[] mas;
};
//рекурсивная процедура сортировки
void MergeSort(int* A, int first, int last) {
    if (first < last) {
        MergeSort(A, first, (first + last) / 2);  //сортировка левой части
        MergeSort(A, (first + last) / 2 + 1, last);  //сортировка правой части
        Merge(A, first, last);  //слияние двух частей
    }
}
void full(int arr[], long N) {
    for (long i = 0; i < N; i++) {
        arr[i] = rand() % 20 * 24 + 1;
    }
}
void info(int arr[], long N) {
    for (long i = 0; i < N; i++) {
        cout << arr[i] << "  ";
    }
    cout << "\n";
}
void copyArray(int arr[], int arr2[], int n)
{
    for (int i = 0; i < n; i++)
    {
        arr2[i] = arr[i];
    }
}
int main()
{
    int time1, time2, time3 = 0;
    int* num_1 = new int[N1];
    int* num_2 = new int[N2];
    int* num_3 = new int[N3];
    full(num_1, N1);
    full(num_2, N2);
    full(num_3, N3);

    //heap sort
    cout << "Heap\t\t";
    //N1
    int* heap_arr_N1 = new int[N1];
    copyArray(num_1, heap_arr_N1, N1);
    time1 = clock();
    heapSort(heap_arr_N1, N1);
    time2 = clock();
    cout << time2 - time1 << "\t\t";

    delete[]heap_arr_N1;

    //N2
    int* heap_arr_N2 = new int[N2];
    copyArray(num_1, heap_arr_N2, N2);
    time1 = clock();
    heapSort(heap_arr_N2, N2);
    time2 = clock();
    cout << time2 - time1 << "\t\t";

    delete[]heap_arr_N2;

    //N3
    int* heap_arr_N3 = new int[N3];
    copyArray(num_1, heap_arr_N3, N3);
    time1 = clock();
    heapSort(heap_arr_N3, N3);
    time2 = clock();
    cout << time2 - time1 << "\t\t";

    delete[]heap_arr_N3;

    
    //quick sort
    cout << "Quick\t\t";
    //N1
    int* quick_arr_N3 = new int[N3];
    copyArray(num_1, quick_arr_N3, N3);
    time1 = clock();
    quickSort(quick_arr_N3, 0, N3 - 1);
    time2 = clock();
    cout << time2 - time1 << "\t\t";

    delete[]quick_arr_N3;
      
}
