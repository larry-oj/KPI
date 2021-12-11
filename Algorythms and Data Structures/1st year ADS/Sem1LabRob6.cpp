#include <iostream>
#include <stdio.h>

int main()
{
    int row, column;

    // 1 - 2
    for (row = 0; row < 2; row++) {
        for (column = 0; column < 7; column++) {
            if (column == row || column == 7 - row - 1) { printf("XX"); }
            else { printf("  "); } }
        printf("\n"); }
    
    // 3 - 4
    for (row = 1; row >= 0; --row) {
        for (column = 0; column < 1; ++column)
            printf("  ");
        for (column = 0; column < row; ++column)
            printf("  ");
        for (column = 0; column < 5 - row * 2; ++column)
            printf("XX");
        printf("\n"); }

    // 5
    for (row = 0; row < 1; row++) {
        for (column = 0; column < 1; ++column)
            printf("  ");
        for (column = 0; column < 5; column++)
            printf("XX");
        printf("\n"); }

    // 6 - 7
    for (row = 0; row < 2; row++) {
        for (column = 0; column < 1; ++column)
            printf("  ");
        for (column = 0; column < row; ++column)
            printf("  ");
        for (column = 0; column < 5 - row * 2; ++column)
            printf("XX");
        printf("\n"); }

    // 8 - 10
    for (row = 2; row >= 0; --row) {
        for (column = 0; column < 1; ++column)
            printf("    ");
        for (column = 0; column < row; ++column)
            printf("  ");
        for (column = 0; column < 10 - row * 2; ++column)
            printf("XX");
        printf("\n"); }

    // 11 - 12
    for (row = 0; row < 2; row++) {
        for (column = 0; column < 1; ++column)
            printf("      ");
        for (column = 0; column < row; ++column)
            printf("  ");
        for (column = 0; column < 8 - row * 2; ++column)
            printf("XX");
        printf("\n"); }

    for (row = 1; row >= 0; --row) {
        for (column = 0; column < 1; ++column)
            printf("    ");
        for (column = 0; column < row; ++column)
            printf("  ");
        for (column = 0; column < 10 - row * 2; ++column)
            printf("XX");
        printf("\n"); }
}


