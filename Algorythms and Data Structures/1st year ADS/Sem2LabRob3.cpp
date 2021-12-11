#include <iostream>
#include <fstream>
#include <string>

using namespace std;


struct list 
{
    double point;
    double result;
    struct list* ptr;
};


struct list* Initialize(double a, double b)
{
    struct list* lst;
    lst = (struct list*)malloc(sizeof(struct list));
    lst->point = a;
    lst->result = b;
    lst->ptr = NULL;
    return(lst);
}


struct list* AddElement(list* lst, double a, double b)
{
    struct list* temp, * p;
    temp = (struct list*)malloc(sizeof(list));
    p = lst->ptr;
    lst->ptr = temp;
    temp->point = a;
    temp->result = b;
    temp->ptr = p;
    return(temp);
}


struct list* DeleteHead(list* root)
{
    struct list* temp;
    temp = root->ptr;
    free(root);
    return(temp);
}


list* DeleteAt(list* root, int pos)
{
    if (pos == 0)return DeleteHead(root);
    list* elBeforeDelete = root;
    list* elDelete;
    int counter = 0;
    while (counter != pos - 1)
    {
        elBeforeDelete = elBeforeDelete->ptr;
        counter++;
    }
    elDelete = elBeforeDelete->ptr;
    elBeforeDelete->ptr = elDelete->ptr;
    free(elDelete);
    return root;
}


void PrintList(list* lst)
{
    struct list* p;
    p = lst;
    cout << "X       \tY" << endl;
    do
    {
        cout << p->point << "       \t" << p->result << endl;
        p = p->ptr;
    } while (p != NULL);
}


void Calculate()
{
    double pi = atan(1) * 4;
    ofstream myfile;
    myfile.open("D:\\Code\\C++\\Sem2LabRob3\\File\\data.txt");
    for (double x = 0; x <= pi / 4; x += pi / 40)
    {
        myfile << x << " - " << sin(x) + cos(x);
        if (x != pi / 4) myfile << "\n";
    }
    myfile.close();
}


double Between(double pointBetween)
{
    return sin(pointBetween / 2) + cos(pointBetween / 2);
}


list* Read()
{
    string line;
    list* list = Initialize(0, 0);
    ifstream myfile("D:\\Code\\C++\\Sem2LabRob3\\File\\data.txt");
    if (myfile.is_open())
    {
        string point;
        string result;
        int index;
        int lineNum = 0;
        string pointPrevious;
        while (getline(myfile, line))
        {
            index = line.find(" - ");
            point = line.substr(0, index);
            point = point.substr(0, 7);
            result = line.substr(index + 3);
            if (lineNum != 0)
            {
                double pointBetween = (stod(pointPrevious) + stol(point)) / 2;
                double betweenResult = Between(pointBetween);
                AddElement(list, pointBetween, betweenResult);
            }
            pointPrevious = point;
            lineNum++;
            AddElement(list, stod(point), stod(result));
        }
        list = DeleteHead(list);
        PrintList(list);
        myfile.close();
    }
    else cout << "err";
    return list;
}


void DeleteFive(list* lst)
{
    int index;
    list* list = lst;
    for (int i = 0; i < 5; i++)
    {
        cout << "enter number" << endl;
        cin >> index;
        list = DeleteAt(list, index - 1);
        cout << endl;
        PrintList(lst);
    }
}


int main()
{
    Calculate();
    list* list = Read();
    DeleteFive(list);
}