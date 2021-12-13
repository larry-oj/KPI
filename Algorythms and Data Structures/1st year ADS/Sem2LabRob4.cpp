#define _CRT_SECURE_NO_DEPRECATE
#include <iostream>
#include <string>
#include <cstdio>
#include <stdio.h>
#include <list>
#include <algorithm>


using namespace std;


struct NOTEBOOK
{
    /*1*/ int diagonal;
    /*2*/ string manufacturer;
    /*3*/ string model;
    /*4*/ bool bluetooth;
    /*5*/ int keys_number;
    /*6*/ int price;
    /*7*/ string operating_system;
};


void ReadFile(list<NOTEBOOK>& notebooks);
void Print(list<NOTEBOOK>& notebooks);
void PrintReverse(list<NOTEBOOK>& notebooks);
void WriteFile(list<NOTEBOOK>& notebooks);
void FindItemInList(list<NOTEBOOK>& notebooks, int type, string value);
void FindTheBestItem(list<NOTEBOOK>& notebooks);
void AddToTop(list<NOTEBOOK>& notebooks);
void AddToBottom(list<NOTEBOOK>& notebooks);
void RemoveItem(list<NOTEBOOK>& notebooks, int index);

bool CompareDiagonals(const NOTEBOOK& first, const NOTEBOOK& second);
bool ComparePrice(const NOTEBOOK& first, const NOTEBOOK& second);


int main()
{
    list<NOTEBOOK> notebooks;

    ReadFile(notebooks);

    {
        cout << "Choose action:" << endl;
        cout << "1 - Print list" << endl;
        cout << "2 - Reverse-Print list" << endl;
        cout << "3 - Add element to the top" << endl;
        cout << "4 - Add element to the bottom" << endl;
        cout << "5 - Find the best item" << endl;
        cout << "6 - Find specific item" << endl;
        cout << "7 - Sort items by diagonal" << endl;
        cout << "8 - Sort items by price" << endl << endl;
    }

    while(true)
    {
        int x, y;
        string option;

        cout << "> ";
        cin >> x;
        cout << endl;
        

        switch(x)
        {
            case 1:
                {
                    Print(notebooks);
                }
                break;

            case 2:
                {
                    PrintReverse(notebooks);
                }
                break;

            case 3:
                {
                    AddToTop(notebooks);
                }
                break;

            case 4:
                {
                    AddToBottom(notebooks);
                }
                break;

            case 5:
                {
                    FindTheBestItem(notebooks);
                }
                break;

            case 6:
                {
                    cout << "Choose specification: " << endl;
                    cout << "1 - Diagonal" << endl;
                    cout << "2 - Manufacturer" << endl;
                    cout << "3 - Model" << endl;
                    cout << "4 - Bluetooth" << endl;
                    cout << "5 - Number of keys" << endl;
                    cout << "6 - Price" << endl;
                    cout << "7 - Operating system" << endl;

                    cin >> y;
                    cout << endl;
                    
                    cout << "Enter value: ";
                    cin >> option;
                    cout << endl;

                    FindItemInList(notebooks, y, option);
                }
                break;

            case 7:
                {
                    notebooks.sort(CompareDiagonals);
                }
                break;

            case 8:
                {
                    notebooks.sort(ComparePrice);
                }
                break;
        }
    }

    return 0;
}


void ReadFile(list<NOTEBOOK>& notebooks)
{
    FILE* file = fopen("D:\\Code\\C++\\ConsoleApplication4\\Debug\\data.txt", "r");

    bool tmp;
    char d[100], m[100], mo[100], b[100], k[100], p[100], o[100];
    while (!feof(file))
    {
        fscanf(file, "%s %s %s %s %s %s %s ", d, m, mo, b, k, p, o);
        if (b == "true") { tmp = true; }
        else { tmp = false; }
        NOTEBOOK notebook = { atoi(d), m, mo, tmp, atoi(k), atoi(p), o };
        notebooks.push_front(notebook);
    }

    fclose(file);
}


void Print(list<NOTEBOOK>& notebooks)
{
    for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
    {
        cout << it->diagonal << " - " << it->manufacturer << " - "
            << it->model << " - " << it->bluetooth << " - " << it->keys_number
            << " - " << it->price << " - " << it->operating_system << endl;
    }
    cout << endl;
}


void PrintReverse(list<NOTEBOOK>& notebooks)
{
    for_each(notebooks.rbegin(),
        notebooks.rend(),
        [](const auto& it) {
            cout << it.diagonal << " - " << it.manufacturer << " - "
                << it.model << " - " << it.bluetooth << " - " << it.keys_number
                << " - " << it.price << " - " << it.operating_system << endl;
        });
    cout << endl;
}


void WriteFile(list<NOTEBOOK>& notebooks)
{
    FILE* file = fopen("data.txt", "w");

    for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
    {
        fprintf(file, "%s %s %s %s %s %s %s ", it->diagonal, it->manufacturer, it->model, it->bluetooth, it->keys_number, it->price, it->operating_system);
    }

    fclose(file);
}


void FindItemInList(list<NOTEBOOK>& notebooks, int type, string value)
{
    switch (type)    // 1 - diagonal // 2 - manufacturer // 3 - model // 4 - bluetooth // 5 - keys // 6 - price // 7 - operation system
    {
        case 1:
            {
                for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
                {
                    string tmp = to_string(it->diagonal);
                    if (it->diagonal == stoi(value))
                        cout << it->diagonal << " - " << it->manufacturer << " - "
                        << it->model << " - " << it->bluetooth << " - " << it->keys_number
                        << " - " << it->price << " - " << it->operating_system << endl;
                }
                cout << endl;
            }
        break;

        case 2:
            {
                for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
                {
                    if (it->manufacturer == value)
                        cout << it->diagonal << " - " << it->manufacturer << " - "
                        << it->model << " - " << it->bluetooth << " - " << it->keys_number
                        << " - " << it->price << " - " << it->operating_system << endl;
                }
            }
            break;

        case 3:
            {
                for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
                {
                    if (it->model == value)
                        cout << it->diagonal << " - " << it->manufacturer << " - "
                        << it->model << " - " << it->bluetooth << " - " << it->keys_number
                        << " - " << it->price << " - " << it->operating_system << endl;
                }
            }
            break;

        case 4:
            {
                // xd
            }
            break;

        case 5:
            {
                for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
                {
                    if (it->keys_number == stoi(value))
                        cout << it->diagonal << " - " << it->manufacturer << " - "
                        << it->model << " - " << it->bluetooth << " - " << it->keys_number
                        << " - " << it->price << " - " << it->operating_system << endl;
                }
            }
            break;

        case 6:
            {
                for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
                {
                    if (it->price == stoi(value))
                        cout << it->diagonal << " - " << it->manufacturer << " - "
                        << it->model << " - " << it->bluetooth << " - " << it->keys_number
                        << " - " << it->price << " - " << it->operating_system << endl;
                }
            }
            break;

        case 7:
            {
                for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
                {
                    if (it->operating_system == value)
                        cout << it->diagonal << " - " << it->manufacturer << " - "
                        << it->model << " - " << it->bluetooth << " - " << it->keys_number
                        << " - " << it->price << " - " << it->operating_system << endl;
                }
            }
            break;
    }

    cout << endl;
}


void FindTheBestItem(list<NOTEBOOK>& notebooks)
{
    list<NOTEBOOK> best, final_item;
    NOTEBOOK best_item;
    int tmp_num = 0;
    
    for (list<NOTEBOOK>::iterator it = notebooks.begin(); it != notebooks.end(); it++)
    {
        if (it->diagonal > tmp_num)
        {
            tmp_num = it->diagonal;
            best_item = {it->diagonal, it->manufacturer, it->model, it->bluetooth, it->keys_number, it->price, it->operating_system};
        }
    }
    best.push_front(best_item);

    tmp_num = 1000000;

    for (list<NOTEBOOK>::iterator it = best.begin(); it != best.end(); it++)
    {
        if (it->price < tmp_num)
        {
            best_item = {it->diagonal, it->manufacturer, it->model, it->bluetooth, it->keys_number, it->price, it->operating_system};
        }
    }

    final_item.push_front(best_item);
    
    Print(best);

    cout << endl;
}


void AddToTop(list<NOTEBOOK>& notebooks)
{
    NOTEBOOK notebook = {11, "xiaomi", "air", false, 80, 15000, "windows"};
    notebooks.push_front(notebook);
}


void AddToBottom(list<NOTEBOOK>& notebooks)
{
    NOTEBOOK notebook = {20, "huawei", "pro", false, 80, 12000, "dos"};
    notebooks.push_back(notebook);
}


void RemoveItem(list<NOTEBOOK>& notebooks, int index)
{
    list<NOTEBOOK>::iterator it = notebooks.begin();

    if (index == 1) { notebooks.erase(it); return; }
    
    advance(it, index - 1);

    notebooks.erase(it);
}


bool CompareDiagonals(const NOTEBOOK& first, const NOTEBOOK& second)
{
    if (first.diagonal < second.diagonal)
	{
		return true;
	}
	else
	{
		return false;
	}
}


bool ComparePrice(const NOTEBOOK& first, const NOTEBOOK& second)
{
    if (first.price < second.price)
	{
		return true;
	}
	else
	{
		return false;
	}
}
