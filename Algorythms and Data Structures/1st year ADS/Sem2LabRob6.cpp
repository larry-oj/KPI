#include <iostream>
#include <string>
#include <utility>
#include <algorithm>
#include <list>
#define P 9;


using namespace std;


struct DATA {
    string info;
    int personal_number;
    int experience;
};
struct NODE {
    DATA data;
    NODE* Left;
    NODE* Right;
};


void AddNode(DATA data, NODE*& node, bool type);
void ShowTree(NODE* node, int l, bool type);
void FindTotalExperience(NODE* node, int* total_experience);
void DeleteTree(NODE* node);
void CopyTree(NODE* node1, NODE*& node2);


int main()
{
    DATA data;
    NODE* tree = NULL;
    NODE* tree2 = NULL;
    int total_experience = 0;

    int tmp = P;

    for(int i = 0; i < tmp; i++)
    {
        cout << "Enter general info: ";
        cin >> data.info;

        cout << "Enter personal number: ";
        cin >> data.personal_number;

        cout << "Enter experience: ";
        cin >> data.experience;
        cout << endl;

        AddNode(data, tree, false);
    }

    ShowTree(tree, 1, false);

    FindTotalExperience(tree, &total_experience);

    cout << "Average experience: " << total_experience / tmp << endl;


    CopyTree(tree, tree2);

    ShowTree(tree2, 1, true);


    DeleteTree(tree);
    DeleteTree(tree2);

    return 0;
}


void AddNode(DATA data, NODE*& node, bool type) // false = experience; true = personal_number
{
    if (node == NULL) 
    {
        node = new NODE;

        node->data = data;
        node->Left = NULL;
        node->Right = NULL;

        return;
    }

    switch(type)
    {
        case false:
            {
                if (data.experience < node->data.experience)
                {
                    AddNode(data, node->Left, false);
                }

                else
                {
                    AddNode(data, node->Right, false);
                }
            }
            break;
        
        case true:
            {
                if (data.personal_number < node->data.personal_number)
                {
                    AddNode(data, node->Left, true);
                }

                else
                {
                    AddNode(data, node->Right, true);
                }
            }
            break;
    }

    
}


void ShowTree(NODE* node, int l, bool type) 
{
    if (node != NULL)
    {
        switch(type)
        {
            case false:
                {
                    ShowTree(((*node).Right), l + 2, false);
                    for (int i = 1; i <= l; i++) cout << " ";
                    cout << (*node).data.experience << endl;
                    ShowTree(((*node).Left), l + 2, false);
                }
                break;

            case true:
                {
                    ShowTree(((*node).Right), l + 2, true);
                    for (int i = 1; i <= l; i++) cout << " ";
                    cout << (*node).data.personal_number << endl;
                    ShowTree(((*node).Left), l + 2, true);
                }
                break;
        }
        
    }
}


void FindTotalExperience(NODE* node, int* total_experience)
{
    if (node == NULL) return;

    *total_experience += node->data.experience;

    FindTotalExperience(node->Left, total_experience);
    FindTotalExperience(node->Right, total_experience);
}


void DeleteTree(NODE* node)
{
    if (node == NULL) return;

    DeleteTree(node->Left);
    DeleteTree(node->Right);

    delete node;
    node = NULL;
}


void CopyTree(NODE* node1, NODE*& node2)
{
    if (node1 == NULL) return;

    AddNode(node1->data, node2, true);

    CopyTree(node1->Left, node2);
    CopyTree(node1->Right, node2);
}