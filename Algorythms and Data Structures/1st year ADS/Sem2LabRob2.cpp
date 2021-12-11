#include <iostream>
#include <stdio.h>
#include "windows.h"
#include <time.h>
#define N 16

using namespace std;


void heapify(int arr[], int n, int i)
{
    int largest = i; // Initialize largest as root
    int l = 2 * i + 1; // left = 2*i + 1
    int r = 2 * i + 2; // right = 2*i + 2

    // If left child is larger than root
    if (l < n && arr[l] > arr[largest])
        largest = l;

    // If right child is larger than largest so far
    if (r < n && arr[r] > arr[largest])
        largest = r;

    // If largest is not root
    if (largest != i) {
        swap(arr[i], arr[largest]);

        // Recursively heapify the affected sub-tree
        heapify(arr, n, largest);
    }
}
void heapSort(int arr[], int n)
{
    for (int i = n / 2 - 1; i >= 0; i--)
        heapify(arr, n, i);

    for (int i = n - 1; i > 0; i--)
    {
        swap(arr[0], arr[i]);
        heapify(arr, i, 0);
    }
}
void printArray(int arr[], int n)
{
    for (int i = 0; i < n; ++i) cout << arr[i] << " ";
    cout << endl;
}
void randomize(int arr[], int n)
{
    srand(time(NULL));

    for (int i = 0; i < n; i++) {
        arr[i] = rand() % (20 * N) + 1;
    }
}
//void quickSort(int arr[], int n) {
//    int i = 0, j = n, temp, p;
//    p = arr[n >> 1];
//    do {
//        while (arr[i] < p) i++;
//        while (arr[j] > p) j--;
//        if (i <= j)
//        {
//            temp = arr[i];
//            arr[i] = arr[j];
//            arr[j] = temp;
//            i++;
//            j--;
//        }
//    } while (i <= j);
//    if (j > 0) quickSort(arr, j);
//    if (n > i) quickSort(arr + i, N - i);
//}
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
void merge(int arr[], int p, int q, int r) 
{
    int n1 = q - p + 1;
    int n2 = r - q;
    int* L = new int[n1]; 
    int* M = new int[n2];
    for (int i = 0; i < n1; i++) L[i] = arr[p + i];
    for (int j = 0; j < n2; j++) M[j] = arr[q + 1 + j];
    int i, j, k;
    i = 0;
    j = 0;
    k = p;
    while (i < n1 && j < n2) 
    {
        if (L[i] <= M[j]) 
        {
            arr[k] = L[i];
            i++;
        }
        else {
            arr[k] = M[j];
            j++;
        }
        k++;
    }
    while (i < n1) 
    {
        arr[k] = L[i];
        i++;
        k++;
    }
    while (j < n2) 
    {
        arr[k] = M[j];
        j++;
        k++;
    }
    delete[] L;
    delete[] M;
}
void mergeSort(int arr[], int l, int r) 
{
    if (l < r) 
    {
        int m = l + (r - l) / 2;
        mergeSort(arr, l, m);
        mergeSort(arr, m + 1, r);
        merge(arr, l, m, r);
    }
}
void copyArray(int arr[], int arr2[], int n) 
{
    for(int i = 0; i < n; i++)
    {
        arr2[i] = arr[i];
    }
}


int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    unsigned int start_time, end_time;

    int* arr_100n = new int[100 * N];
    int* arr_1000n = new int[1000 * N];
    int* arr_10000n = new int[10000 * N];
    randomize(arr_100n, 100 * N);
    randomize(arr_1000n, 1000 * N);
    randomize(arr_10000n, 10000 * N);


    cout << endl << "Type\t\t" << "N*100\t\t" << "N*1000\t\t" << "N*10000\t\t" << "Memory\t\t" << "Tenacity\t" << "Naturalness" << endl;


    


    // heap sort
    cout << "Heap\t\t";

    // 100 * N
    int* heap_arr_100n = new int[100 * N];
    copyArray(arr_100n, heap_arr_100n, 100 * N);

    start_time = clock();
    heapSort(heap_arr_100n, 100 * N);
    end_time = clock();
    unsigned int heap_100n = end_time - start_time;
    cout << heap_100n << "\t\t";

    delete[] heap_arr_100n;

    // 1000 * N
    int* heap_arr_1000n = new int[1000 * N];
    copyArray(arr_1000n, heap_arr_1000n, 1000 * N);

    start_time = clock();
    heapSort(heap_arr_1000n, 1000 * N);
    end_time = clock();
    unsigned int heap_1000n = end_time - start_time;
    cout << heap_1000n << "\t\t";

    delete[] heap_arr_1000n;

    // 10000 * N
    int* heap_arr_10000n = new int[10000 * N];
    copyArray(arr_10000n, heap_arr_10000n, 10000 * N);

    start_time = clock();
    heapSort(heap_arr_10000n, 10000 * N);
    end_time = clock();
    unsigned int heap_10000n = end_time - start_time;
    cout << heap_10000n << "\t\t";

    delete[] heap_arr_10000n;

    cout << "None\t\t" << "Unstable\t" << "Unnatural\t\t" << endl;





    // quick sort
    cout << "Quick\t\t";

    // 100 * N
    int* quick_arr_100n = new int[100 * N];
    copyArray(arr_100n, quick_arr_100n, 100 * N);

    start_time = clock();
    quickSort(quick_arr_100n, 0, (100 * N) - 1);
    end_time = clock();
    unsigned int quick_100n = end_time - start_time;
    cout << quick_100n << "\t\t";

    delete[] quick_arr_100n;


    // 1000 * N
    int* quick_arr_1000n = new int[1000 * N];
    copyArray(arr_1000n, quick_arr_1000n, 1000 * N);

    start_time = clock();
    quickSort(quick_arr_1000n, 0, (1000 * N) - 1);
    end_time = clock();
    unsigned int quick_1000n = end_time - start_time;
    cout << quick_1000n << "\t\t";

    delete[] quick_arr_1000n;


    // 10000 * N
    int* quick_arr_10000n = new int[10000 * N];
    copyArray(arr_10000n, quick_arr_10000n, 10000 * N);

    start_time = clock();
    quickSort(quick_arr_10000n, 0, (10000 * N) - 1);
    end_time = clock();
    unsigned int quick_10000n = end_time - start_time;
    cout << quick_10000n << "\t\t";

    delete[] quick_arr_10000n;

    cout << "None\t\t" << "Unstable\t" << "Unnatural\t\t" << endl;


    




    // merge sort
    cout << "Merge\t\t";

    // 100 * N
    int* merge_arr_100n = new int[100 * N];
    copyArray(arr_100n, merge_arr_100n, 100 * N);

    start_time = clock();
    mergeSort(merge_arr_100n, 0, (100 * N) - 1);
    end_time = clock();
    unsigned int merge_100n = end_time - start_time;
    cout << merge_100n << "\t\t";

    delete[] merge_arr_100n;


    // 1000 * N
    int* merge_arr_1000n = new int[1000 * N];
    copyArray(arr_1000n, merge_arr_1000n, 1000 * N);

    start_time = clock();
    mergeSort(merge_arr_1000n, 0, (1000 * N) - 1);
    end_time = clock();
    unsigned int merge_1000n = end_time - start_time;
    cout << merge_1000n << "\t\t";

    delete[] merge_arr_1000n;


    // 10000 * N
    int* merge_arr_10000n = new int[10000 * N];
    copyArray(arr_10000n, merge_arr_10000n, 10000 * N);

    start_time = clock();
    mergeSort(merge_arr_10000n, 0, (10000 * N) - 1);
    end_time = clock();
    unsigned int merge_10000n = end_time - start_time;
    cout << merge_10000n << "\t\t";

    delete[] merge_arr_10000n;

    cout << "None\t\t" << "Stable\t\t" << "Unnatural\t\t" << endl;
}