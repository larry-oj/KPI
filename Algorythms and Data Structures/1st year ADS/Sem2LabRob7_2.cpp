#include <iostream>
#include <string>
#include <vector>
#include <map>
#include <stack>
#include <queue>


using namespace std;


queue<int> Q;


void PrintMatrix(vector<vector<int>> matrix);
void PrintVector(vector<string> _vector);
vector<vector<int>> CreateIncidenceMatrix(vector<vector<int>> matrix);
vector<string> CreateEdgesFromIncidence(vector<vector<int>> matrix);
vector<string> CrateAdjListFormAdjMatrix(vector<vector<int>> matrix);
vector<vector<int>> CrateAdjacencyMatrix(vector<vector<int>> matrix);
vector<string> CreateEdgesListFormAdjMatrix(vector<vector<int>> matrix);
vector<string> CreateAdjListFormIncidenceMatrix(vector<vector<int>> matrix);
void BreadthFirstSearch(vector<vector<int>> matrix);
void DepthFirstSearch(vector<vector<int>> matrix);


int main()
{
    vector<vector<int>> graph_one_adjacency_matrix {
    //   a  b  c  d  e  f
        {0, 0, 0, 1, 1, 0}, // a
        {0, 0, 1, 0, 1, 0}, // b
        {0, 1, 0, 0, 0, 1}, // c
        {1, 0, 0, 0, 0, 1}, // d
        {1, 1, 0, 0, 1, 1}, // e
        {0, 0, 1, 1, 1, 0}  // f
    };
    vector<vector<int>> graph_one_incidence_matrix {
        {0, -1, 0, 0, 1, 0, 0, 0},  // a
        {0, 0, -1, 0, 0, 1, 0, 0},  // b
        {0, 0, 0, 0, 0, -1, -1, 0}, // c
        {0, 0, 0, 0, -1, 0, 0, -1}, // d
        {2, 1, 1, 1, 0, 0, 0, 0},   // e
        {0, 0, 0, -1, 0, 0, 1, 1}   // f
    };
    vector<string> graph_one_edges = {"e->e", "e->a", "e->b", "e->f", "a->d", "b->c", "f->c", "f->d"};
    vector<string> graph_one_adjacency = {"ee", "ea", "eb", "ef", "ad", "bc", "fc", "fd"};

    {
        cout << "Adjency matrix:" << endl;
        PrintMatrix(graph_one_adjacency_matrix);
        cout << endl;

        cout << "Incidence matrix:" << endl;
        PrintMatrix(graph_one_incidence_matrix);
        cout << endl;

        cout << "The list of edges:" << endl;
        PrintVector(graph_one_edges);
        cout << endl;

        cout << "The list of adjacencies:" << endl;
        PrintVector(graph_one_adjacency);
        cout << endl;

        cout << "Converted incidence matrix:" << endl;
        PrintMatrix(CreateIncidenceMatrix(graph_one_adjacency_matrix));
        cout << endl;
        
        cout << "List of edges form incidence matrix:" << endl;
        PrintVector(CreateEdgesFromIncidence(graph_one_incidence_matrix));
        cout << endl;

        cout << "Adjacency list form adjacency matrix:" << endl;
        PrintVector(CrateAdjListFormAdjMatrix(graph_one_adjacency_matrix));
        cout << endl;

        cout << "Converted adjacency matrix:" << endl;
        PrintMatrix(CrateAdjacencyMatrix(graph_one_incidence_matrix));
        cout << endl;

        cout << "Edges list form adjacency matrix:" << endl;
        PrintVector(CreateEdgesListFormAdjMatrix(graph_one_adjacency_matrix));
        cout << endl;

        cout << "Adjacency list form incidence matrix:" << endl;
        PrintVector(CreateAdjListFormIncidenceMatrix(graph_one_incidence_matrix));
        cout << endl;

        cout << "Breadth First Search:" << endl;
        BreadthFirstSearch(graph_one_adjacency_matrix);
        cout << endl;

        cout << "Depth First Search:" << endl;
        DepthFirstSearch(graph_one_adjacency_matrix);
        cout << endl;
    }

    return 0;
}


void PrintMatrix(vector<vector<int>> matrix)
{

    for (int row = 0; row < matrix.size(); row++)
    {
        for (int column = 0; column < matrix[0].size(); column++)
        {
            cout << matrix[row][column] << " ";
        }
        cout << endl;
    }
}


void PrintVector(vector<string> _vector)
{
    for (auto i : _vector) cout << i << "  ";
    cout << endl;
}


vector<vector<int>> CreateIncidenceMatrix(vector<vector<int>> matrix)
{
    int tops_count = matrix.size();
    int edges_count = 0;
    for (int row = 0; row < tops_count; row++)
    {
        for (int column = 0 + row; column < tops_count; column++)
        {
            if (matrix[row][column] == 1) edges_count++;
        }
    }

    vector<vector<int>> result;
    vector<int> tmp;
    for (int i = 0; i < tops_count; i++) 
    {
        result.push_back(tmp);
        for (int j = 0; j < edges_count; j++)
        {
            result[i].push_back(0);
        }
    }
    
    int top = 0;
    for (int row = 0; row < tops_count; row++)
    {
        for (int column = 0 + row; column < tops_count; column++)
        {
            if (matrix[column][row] == 1)
            {
                result[column][top] = 1;
                if (result[row][top] == 1) result[row][top] = 2;
                else result[row][top] = 1;
                top++;
            }
        }
    }

    return result;
}


vector<string> CreateEdgesFromIncidence(vector<vector<int>> matrix)
{
    vector<string> result;
    string tmp;
    for (int column = 0; column < matrix[0].size(); column++)
    {
        for (int row = 0; row < matrix.size(); row++)
        {
            if (matrix[row][column] == -1) 
            {
                switch(row)
                {
                    case 0:
                        tmp += "a";
                        break;

                    case 1:
                        tmp += "b";
                        break;

                    case 2:
                        tmp += "c";
                        break;

                    case 3:
                        tmp += "d";
                        break;

                    case 4:
                        tmp += "e";
                        break;

                    case 5:
                        tmp += "f";
                        break;
                }     
            }
            else if (matrix[row][column] == 1)
            {
                tmp = "->" + tmp;
                switch(row)
                {
                    case 0:
                        tmp = "a" + tmp;
                        break;

                    case 1:
                        tmp = "b" + tmp;
                        break;

                    case 2:
                        tmp = "c" + tmp;
                        break;

                    case 3:
                        tmp = "d" + tmp;
                        break;

                    case 4:
                        tmp = "e" + tmp;
                        break;

                    case 5:
                        tmp = "f" + tmp;
                        break;
                }
            }
            else if (matrix[row][column] == 2)
            {
                switch(row)
                {
                    case 0:
                        tmp += "a->a";
                        break;

                    case 1:
                        tmp += "b->b";
                        break;

                    case 2:
                        tmp += "c->c";
                        break;

                    case 3:
                        tmp += "d->d";
                        break;

                    case 4:
                        tmp += "e->e";
                        break;

                    case 5:
                        tmp += "f->f";
                        break;
                }
            }
        }
        result.push_back(tmp);
        tmp = "";
    }

    return result;
}


vector<string> CrateAdjListFormAdjMatrix(vector<vector<int>> matrix)
{
    vector<string> result;
    string tmp;

    for (int row = 0; row < matrix.size(); row++)
    {
        for (int column = 0 + row; column < matrix[0].size(); column++)
        {
            if (matrix[row][column] == 1)
            {
                switch(row)
                {
                    case 0:
                        tmp += "a";
                        break;

                    case 1:
                        tmp += "b";
                        break;

                    case 2:
                        tmp += "c";
                        break;

                    case 3:
                        tmp += "d";
                        break;

                    case 4:
                        tmp += "e";
                        break;

                    case 5:
                        tmp += "f";
                        break;
                }

                switch(column)
                {
                    case 0:
                        tmp += "a";
                        break;

                    case 1:
                        tmp += "b";
                        break;

                    case 2:
                        tmp += "c";
                        break;

                    case 3:
                        tmp += "d";
                        break;

                    case 4:
                        tmp += "e";
                        break;

                    case 5:
                        tmp += "f";
                        break;
                }

                result.push_back(tmp);
            }
            tmp = "";
        }
    }

    return result;
}


vector<vector<int>> CrateAdjacencyMatrix(vector<vector<int>> matrix)
{
    int tops_count = matrix.size();

    vector<vector<int>> result;
    vector<int> tmp;
    for (int i = 0; i < tops_count; i++) 
    {
        result.push_back(tmp);
        for (int j = 0; j < tops_count; j++)
        {
            result[i].push_back(0);
        }
    }

    int top = 0;
    bool crutch = false;
    for (int column = 0; column < matrix[0].size(); column++)
    {
        for (int row = 0; row < matrix.size(); row++)
        {
            if (matrix[row][column] == 2)
            {
                result[row][row] = 1;
                top = 0;
                break;
            }
            else if (abs(matrix[row][column]) == 1 && crutch != true)
            {
                top = row;
                crutch = true;
            }
            else if (abs(matrix[row][column]) == 1)
            {
                result[top][row] = 1;
                result[row][top] = 1;
                top = 0;
                crutch = false;
                break;
            }
        }
    }

    return result;
}


vector<string> CreateEdgesListFormAdjMatrix(vector<vector<int>> matrix)
{
    vector<string> result;
    string tmp;

    for (int row = 0; row < matrix.size(); row++)
    {
        for (int column = 0 + row; column < matrix[0].size(); column++)
        {
            if (matrix[row][column] == 1)
            {
                switch(row)
                {
                    case 0:
                        tmp += "a";
                        break;

                    case 1:
                        tmp += "b";
                        break;

                    case 2:
                        tmp += "c";
                        break;

                    case 3:
                        tmp += "d";
                        break;

                    case 4:
                        tmp += "e";
                        break;

                    case 5:
                        tmp += "f";
                        break;
                }

                tmp += "-";

                switch(column)
                {
                    case 0:
                        tmp += "a";
                        break;

                    case 1:
                        tmp += "b";
                        break;

                    case 2:
                        tmp += "c";
                        break;

                    case 3:
                        tmp += "d";
                        break;

                    case 4:
                        tmp += "e";
                        break;

                    case 5:
                        tmp += "f";
                        break;
                }

                result.push_back(tmp);
            }
            tmp = "";
        }
    }

    return result;
}


vector<string> CreateAdjListFormIncidenceMatrix(vector<vector<int>> matrix)
{
    vector<string> result;
    string tmp;
    for (int column = 0; column < matrix[0].size(); column++)
    {
        for (int row = 0; row < matrix.size(); row++)
        {
            if (matrix[row][column] == -1) 
            {
                switch(row)
                {
                    case 0:
                        tmp += "a";
                        break;

                    case 1:
                        tmp += "b";
                        break;

                    case 2:
                        tmp += "c";
                        break;

                    case 3:
                        tmp += "d";
                        break;

                    case 4:
                        tmp += "e";
                        break;

                    case 5:
                        tmp += "f";
                        break;
                }
            }
            else if (matrix[row][column] == 1)
            {
                switch(row)
                {
                    case 0:
                        tmp = "a" + tmp;
                        break;

                    case 1:
                        tmp = "b" + tmp;
                        break;

                    case 2:
                        tmp = "c" + tmp;
                        break;

                    case 3:
                        tmp = "d" + tmp;
                        break;

                    case 4:
                        tmp = "e" + tmp;
                        break;

                    case 5:
                        tmp = "f" + tmp;
                        break;
                }
            }
            else if (matrix[row][column] == 2)
            {
                switch(row)
                {
                    case 0:
                        tmp += "aa";
                        break;

                    case 1:
                        tmp += "bb";
                        break;

                    case 2:
                        tmp += "cc";
                        break;

                    case 3:
                        tmp += "dd";
                        break;

                    case 4:
                        tmp += "ee";
                        break;

                    case 5:
                        tmp += "ff";
                        break;
                }
            }
        }
        result.push_back(tmp);
        tmp = "";
    }

    return result;
}


void BreadthFirstSearch(vector<vector<int>> matrix)
{
    const int n = 6;
    int start = 5;

    bool *visited = new bool[n];
    for (int i = 0; i < n; i++)
        visited[i] = false;

    int unit = start - 1;
    visited[unit] = true;
    Q.push(unit);


    while (!Q.empty())
    {
        unit = Q.front();
        Q.pop();

        switch (unit + 1)
        {
            case 1:
                cout << "a ";
                break;
            
            case 2:
                cout << "b ";
                break;

            case 3:
                cout << "c ";
                break;

            case 4:
                cout << "d ";
                break;

            case 5:
                cout << "e ";
                break;

            case 6:
                cout << "f ";
                break;
        }

        for (int i = 0; i < n; i++)
        {
            if (matrix[unit][i] && !visited[i])
            {
                Q.push(i);

                visited[i] = true;
            }
        }
    }

    delete[] visited;

    cout << endl;
}


void DepthFirstSearch(vector<vector<int>> matrix)
{
    const int n = 6;
    stack<int> Stack;

    int nodes[n];
    for (int i = 0; i < n; i++)
    {
        nodes[i] = 0;
    }

    Stack.push(4);

    while (!Stack.empty())
    {
        int node = Stack.top();

        Stack.pop();

        if (nodes[node] == 2)
            continue;

        nodes[node] = 2;

        for (int j = n - 1; j >= 0; j--)
        {
            if (matrix[node][j] == 1 && nodes[j] != 2)
            {
                Stack.push(j);
                nodes[j] = 1;
            }
        }

        switch (node + 1)
        {
            case 1:
                cout << "a ";
                break;
            
            case 2:
                cout << "b ";
                break;

            case 3:
                cout << "c ";
                break;

            case 4:
                cout << "d ";
                break;

            case 5:
                cout << "e ";
                break;

            case 6:
                cout << "f ";
                break;
        }
    }
}
