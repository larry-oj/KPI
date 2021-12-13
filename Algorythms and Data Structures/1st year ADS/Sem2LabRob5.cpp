#include <iostream>
#include <string>
#include <stack>
#include <vector>
#include <sstream>
#include <list>


using namespace std;


stack<int> CreateIntStack(int size);
void PrintIntStack(stack<int> s);
void Divide(stack<int>& stack1, stack<int>& stack2);
bool CheckBalance(string brackets);


int main()
{
    int size;
    cout << "Enter stack size: ";
    cin >> size;

    stack<int> int_stack = CreateIntStack(size);
    PrintIntStack(int_stack);
    cout << endl;
    stack<int> int_stack_odd;

    Divide(int_stack, int_stack_odd);

    PrintIntStack(int_stack);
    cout << endl;
    PrintIntStack(int_stack_odd);
    cout << endl << endl;

    string brackets;
    cout << "Enter brackets sequence: " << endl;
    while(true)
    {
        cout << "> ";
        cin >> brackets;

        CheckBalance(brackets) ? cout << "Balanced" << endl << endl : cout << "Not balanced" << endl << endl; 
    }

    return 0;
}


stack<int> CreateIntStack(int size)
{
    stack<int> r;
    string s;

    for (int i = 0; i < size; i++)
    {
        cout << i + 1 << "> ";
        cin >> s;
        r.push(atoi(s.c_str()));
    }
    
    return r;
}


void PrintIntStack(stack<int> s)
{
    if (s.empty()) return;

    int x = s.top();

    s.pop();

    cout << x << ' ';

    PrintIntStack(s);

    s.push(x);
}


void Divide(stack<int>& stack1, stack<int>& stack2)
{
    if (stack1.empty()) return;

    int x = stack1.top();

    stack1.pop();

    if (x % 2 == 0)
    {
        Divide(stack1, stack2);
        stack1.push(x);
    }
    else
    {
        Divide(stack1, stack2);
        stack2.push(x);
    }
}


bool CheckBalance(string brackets)
{ 
    stack<char> s;
    char x;
 
    for (int i = 0; i < brackets.length(); i++)
    {
        if (brackets[i] == '(' || brackets[i] == '[' || brackets[i] == '{')
        {
            s.push(brackets[i]);
            continue;
        }

        if (s.empty()) return false;
 
        switch (brackets[i]) {
            case ')':
                {
                    x = s.top();
                    s.pop();
                    if (x == '{' || x == '[') return false;
                }
                break;
    
            case '}':
                {
                    x = s.top();
                    s.pop();
                    if (x == '(' || x == '[') return false;
                }
                break;
    
            case ']':
                {
                    x = s.top();
                    s.pop();
                    if (x == '(' || x == '{') return false;
                }
                break;
        }
    }

    return (s.empty());
}
