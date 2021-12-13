#include <iostream>
#include <cstdlib>
#include "windows.h"
#include <time.h>
#define N 16

using namespace std;

void writeArray(int a[], long size) {
    for (int i = 0; i < size; i++) {
        cout << a[i] << " ";
    }
    cout << endl;
}
void selectSort(int a[], long size) {
    long i, j, k;
    int x;
    for (i = 0; i < size; i++) {
        k = i;
        x = a[i];
        for (j = i + 1; j < size; j++)
            if (a[j] < x) {
                k = j;
                x = a[j];
            }
        a[k] = a[i];
        a[i] = x;
    }
}
void insertSort(int a[], long size) {
    long i, j;
    int x;
    for (i = 0; i < size; i++) {
        x = a[i];
        j = i - 1;
        while (x < a[j] && j >= 0) {
            a[j + 1] = a[j];
            j = j - 1;
        }
        a[j + 1] = x;
    }
}
void bubbleSort(int a[], long size)
{
    long i, j;
    int x;
    for (i = 0; i < size - 1; i++) {
        for (j = 0; j < size - i - 1; j++) {
            if (a[j] > a[j + 1]) {
                x = a[j];
                a[j] = a[j + 1];
                a[j + 1] = x;
            }
        }
    }
}
void shakerSort(int a[], long size, bool param) {
    long j, k = size - 1;
    long lb = 1, ub = size - 1;
    int x;
    do
    {
        for (j = ub; j > 0; j--) {
            if (a[j - 1] > a[j]) {
                x = a[j - 1];
                a[j - 1] = a[j];
                a[j] = x;
                k = j;
                if (param) writeArray(a, size);
            }
        }
        lb = k + 1;
        for (j = 1; j <= ub; j++) {
            if (a[j - 1] > a[j]) {
                x = a[j - 1];
                a[j - 1] = a[j];
                a[j] = x;
                k = j;
                if (param) writeArray(a, size);
            }
        }
        ub = k - 1;
        if (param) writeArray(a, size);
    } while (lb < ub);
}


int main()
{
    SetConsoleCP(1251); 
    SetConsoleOutputCP(1251);

    srand(time(NULL));


    // #1
    int *example = new int[10];
    for (int i = 0; i < 10; i++) {
        example[i] = rand() % (20 * N) + 1;
    }
    shakerSort(example, 10, true);
    writeArray(example, 10);
    delete[] example;


    cout << endl << "Type\t\t" << "N*10\t\t" << "N*100\t\t" << "N*10000\t\t" << "Memory\t\t" << "Tenacity\t" << "Naturalness" << endl;


    // Selection Sort 10*N
    int *select_10n_a = new int[10 * N];
    for (int i = 0; i < 10 * N; i++) {
        select_10n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int select_10n_st = clock();
    selectSort(select_10n_a, 10 * N);
    unsigned int select_10n_et = clock();
    unsigned int select_10n_tt = select_10n_et - select_10n_st;
    delete[] select_10n_a;

    // Selection Sort 100*N
    int *select_100n_a = new int[100 * N];
    for (int i = 0; i < 100 * N; i++) {
        select_100n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int select_100n_st = clock();
    selectSort(select_100n_a, 100 * N);
    unsigned int select_100n_et = clock();
    unsigned int select_100n_tt = select_100n_et - select_100n_st;
    // nativeness check
    unsigned int select_100n_st_s = clock();
    selectSort(select_100n_a, 100 * N);
    unsigned int select_100n_et_s = clock();
    unsigned int select_100n_tt_s = select_100n_et_s - select_100n_st_s;
    bool select_s = true;
    if (select_100n_tt_s >= select_100n_tt) select_s = false;
    delete[] select_100n_a;

    // Selection Sort 10000*N
    int *select_10000n_a = new int[10000 * N];
    for (int i = 0; i < 10000 * N; i++) {
        select_10000n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int select_10000n_st = clock();
    selectSort(select_10000n_a, 10000 * N);
    unsigned int select_10000n_et = clock();
    unsigned int select_10000n_tt = select_10000n_et - select_10000n_st;
    delete[] select_10000n_a;

    // Output
    cout << "Selection\t" << select_10n_tt << "\t\t" << select_100n_tt << "\t\t" << select_10000n_tt << "\t\t" << "None\t\t" << "Stable\t\t";
    if (select_s) { cout << "Natural" << endl; }
    else { cout << "Unnatural" << endl; }


    // Bubble Sort 10*N
    int *bubble_10n_a = new int[10 * N];
    for (int i = 0; i < 10 * N; i++) {
        bubble_10n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int bubble_10n_st = clock();
    bubbleSort(bubble_10n_a, 10 * N);
    unsigned int bubble_10n_et = clock();
    unsigned int bubble_10n_tt = bubble_10n_et - bubble_10n_st;
    delete[] bubble_10n_a;

    // Bubble Sort 100*N
    int *bubble_100n_a = new int[100 * N];
    for (int i = 0; i < 100 * N; i++) {
        bubble_100n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int bubble_100n_st = clock();
    bubbleSort(bubble_100n_a, 100 * N);
    unsigned int bubble_100n_et = clock();
    unsigned int bubble_100n_tt = bubble_100n_et - bubble_100n_st;
    // nativeness check
    unsigned int bubble_100n_st_s = clock();
    bubbleSort(bubble_100n_a, 100 * N);
    unsigned int bubble_100n_et_s = clock();
    unsigned int bubble_100n_tt_s = bubble_100n_et_s - bubble_100n_st_s;
    bool bubble_s = true;
    if (bubble_100n_tt_s >= bubble_100n_tt) bubble_s = false;
    delete[] bubble_100n_a;

    // Bubble Sort 10000*N
    int *bubble_10000n_a = new int[10000 * N];
    for (int i = 0; i < 10000 * N; i++) {
        bubble_10000n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int bubble_10000n_st = clock();
    bubbleSort(bubble_10000n_a, 10000 * N);
    unsigned int bubble_10000n_et = clock();
    unsigned int bubble_10000n_tt = bubble_10000n_et - bubble_10000n_st;
    delete[] bubble_10000n_a;

    // Output
    cout << "Bubble\t\t" << bubble_10n_tt << "\t\t" << bubble_100n_tt << "\t\t" << bubble_10000n_tt << "\t\t" << "None\t\t" << "Stable\t\t";
    if (bubble_s) { cout << "Natural" << endl; }
    else { cout << "Unnatural" << endl; }


    // Shaker Sort 10*N
    int *shaker_10n_a = new int[10 * N];
    for (int i = 0; i < 10 * N; i++) {
        shaker_10n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int shaker_10n_st = clock();
    shakerSort(shaker_10n_a, 10 * N, false);
    unsigned int shaker_10n_et = clock();
    unsigned int shaker_10n_tt = shaker_10n_et - shaker_10n_st;
    delete[] shaker_10n_a;

    // Shaker Sort 100*N
    int *shaker_100n_a = new int[100 * N];
    for (int i = 0; i < 100 * N; i++) {
        shaker_100n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int shaker_100n_st = clock();
    shakerSort(shaker_100n_a, 100 * N, false);
    unsigned int shaker_100n_et = clock();
    unsigned int shaker_100n_tt = shaker_100n_et - shaker_100n_st;
    // nativeness check
    unsigned int shaker_100n_st_s = clock();
    shakerSort(shaker_100n_a, 100 * N, false);
    unsigned int shaker_100n_et_s = clock();
    unsigned int shaker_100n_tt_s = shaker_100n_et_s - shaker_100n_st_s;
    bool shaker_s = true;
    if (shaker_100n_tt_s >= shaker_100n_tt) shaker_s = false;
    delete[] shaker_100n_a;

    // Shaker Sort 10000*N
    int *shaker_10000n_a = new int[10000 * N];
    for (int i = 0; i < 10000 * N; i++) {
        shaker_10000n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int shaker_10000n_st = clock();
    shakerSort(shaker_10000n_a, 10000 * N, false);
    unsigned int shaker_10000n_et = clock();
    unsigned int shaker_10000n_tt = shaker_10000n_et - shaker_10000n_st;
    delete[] shaker_10000n_a;

    // Output
    cout << "Shaker\t\t" << shaker_10n_tt << "\t\t" << shaker_100n_tt << "\t\t" << shaker_10000n_tt << "\t\t" << "None\t\t" << "Stable\t\t";
    if (shaker_s) { cout << "Natural" << endl; }
    else { cout << "Unnatural" << endl; }


    // Insertion Sort 10*N
    int *insert_10n_a = new int[10 * N];
    for (int i = 0; i < 10 * N; i++) {
        insert_10n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int insert_10n_st = clock();
    insertSort(insert_10n_a, 10 * N);
    unsigned int insert_10n_et = clock();
    unsigned int insert_10n_tt = insert_10n_et - insert_10n_st;
    delete[] insert_10n_a;

    // Insertion Sort 100*N
    int *insert_100n_a = new int[100 * N];
    for (int i = 0; i < 100 * N; i++) {
        insert_100n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int insert_100n_st = clock();
    insertSort(insert_100n_a, 100 * N);
    unsigned int insert_100n_et = clock();
    unsigned int insert_100n_tt = insert_100n_et - insert_100n_st;
    // nativeness check
    unsigned int insert_100n_st_s = clock();
    insertSort(insert_100n_a, 100 * N);
    unsigned int insert_100n_et_s = clock();
    unsigned int insert_100n_tt_s = insert_100n_et_s - insert_100n_st_s;
    bool insert_s = true;
    if (insert_100n_tt_s >= insert_100n_tt) insert_s = false;
    delete[] insert_100n_a;

    // Insertion Sort 10000*N
    int *insert_10000n_a = new int[10000 * N];
    for (int i = 0; i < 10000 * N; i++) {
        insert_10000n_a[i] = rand() % (20 * N) + 1;
    }
    unsigned int insert_10000n_st = clock();
    insertSort(insert_10000n_a, 10000 * N);
    unsigned int insert_10000n_et = clock();
    unsigned int insert_10000n_tt = insert_10000n_et - insert_10000n_st;
    delete[] insert_10000n_a;

    // Output
    cout << "Insertion\t" << insert_10n_tt << "\t\t" << insert_100n_tt << "\t\t" << insert_10000n_tt << "\t\t" << "None\t\t" << "Stable\t\t";
    if (insert_s) { cout << "Natural" << endl; }
    else { cout << "Unnatural" << endl; }
}