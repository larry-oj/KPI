STSEG SEGMENT PARA STACK "STACK" 
STSEG ENDS

DSEG SEGMENT PARA PUBLIC "DATA"
    str_enter_x DB "Enter X: $"
    str_x DB 7, "?", 7 DUP("?")
    str_result DB 10, 13, "The result is: $"
    str_remainder DB 10, 13, "Remainder: $"
    str_error DB 10, 13, "Error!$"

    err_checker DW 10
    x_num DW 0
    remainder DW 0
    result DW 0
DSEG ENDS

CSEG SEGMENT PARA PUBLIC "CODE"
    MAIN PROC FAR
        ASSUME cs: CSEG, ds: DSEG, ss:STSEG

        push ds
        xor ax, ax 
        push ax

        mov ax, DSEG 
        mov ds, ax

        lea dx, str_enter_x
        call writeline

        lea dx, str_x
        call readline

        call x_to_num
        and dx, 7 ; dx is set to 7 if error happened in to_x_num proc
        jnz err_handler

        mov bx, x_num 
        cmp bx, 5 ; operand comparison command
        jg third_case ; x > 5
        je first_or_second_case ; x = 5
        jl first_or_second_case ; x < 5

        first_or_second_case:
            cmp bx, -5
            jg second_case ; x > -5
            je first_case ; x = -5
            jl first_case ; x < -5
        ret

        first_case:
            call case_one 
        ret

        second_case:
            call case_two
        ret

        third_case:
            call case_three
        ret

        err_handler:
            mov ah, 9
            lea dx, str_error
            int 21h
        ret

    MAIN ENDP

    writeline proc far
        mov ah, 9
        int 21h
        ret
    writeline endp

    readline proc far
        mov ah, 10
        int 21h
        ret
    readline endp


    case_one proc far ; (1 + x^2) / (1 - x)
        mov ax, x_num;
        imul ax ; x^2
        jo first_overflow

        add ax, 1 ; 1 + x^2
        jo first_overflow

        mov bx, 1
        sub bx, x_num ; 1 - x
        jo first_overflow

        mov dx, 0

        idiv bx ; (1 + x^2) / (1 - x)
        jo first_overflow

        mov result, ax
        mov remainder, dx

        lea dx, str_result
        call writeline
        call write_results

        lea dx, str_remainder
        call writeline
        call write_remainder

        ret

        first_overflow:
            lea dx, str_error
            call writeline
        ret

    case_one endp

    case_two proc far ; x^2 + 375
        mov ax, x_num
        imul ax ; x^2
        jo second_overflow

        mov bx, 375
        add ax, bx ; x^2 + 375
        jo second_overflow

        mov result, ax

        lea dx, str_result
        call writeline

        lea dx, result
        call write_results

        ret

        second_overflow:
            lea dx, str_error
            call writeline
        ret

    case_two endp

    case_three proc far ; x^2 / 10
        mov ax, x_num
        imul ax ; x^2
        jo second_overflow

        mov bx, 10
        idiv bx ; x^2 / 10
        jo second_overflow

        mov result, ax
        mov remainder, dx

        lea dx, str_result
        call writeline
        call write_results

        lea dx, str_remainder
        call writeline
        call write_remainder

        ret

        third_overflow:
            lea dx, str_error
            call writeline
        ret

    case_three endp

    x_to_num proc far
        lea si, str_x+1

        mov cx, 0
        mov ax, 0
        mov bx, 0
        mov dx, 0
        mov di, 0

        mov cl, [si]
        inc si
        mov bl, [si] ; first char in string

        cmp bl, '-'
        jne x_to_num_loop ; if number is positive

        mov di, 1 ; if number ss negative
        dec cx
        inc si

        x_to_num_loop:
            mov bl, [si] ; current char
            inc si ; next char

            ; check if the current symbol is a number
            cmp bl, '0'
            jb x_error
            cmp bl, '9'
            ja x_error

            sub bl, '0' ; get number form current symbol

            ; if overflow
            imul err_checker
            jo x_error

            add ax, bx ; add to result
            jo x_error

            loop x_to_num_loop

        mov dx, 0;

        and di, 1 ; if di == 0, the number is positive
        jz x_end

        neg ax
        jmp x_end

        x_error:
            mov dx, 7

        x_end:
            mov x_num, ax
            ret

    x_to_num endp 
    
    write_results proc far
        mov bx, result
        or bx, bx
        jns m1
        mov al, '-'
        int 29H
        neg bx 
        m1:
            mov ax, bx
            xor cx, cx
            mov bx, 10
        m2: 
            xor dx, dx
            div bx
            add dl, '0'
            push dx
            inc cx
            test ax, ax
            jnz m2
        m3:
            pop ax
            int 29H
            loop m3
            ret
    write_results endp

    write_remainder proc far
        mov bx, remainder
        or bx, bx
        jns m11
        mov al, '-'
        int 29H
        neg bx 
        m11:
            mov ax, bx
            xor cx, cx
            mov bx, 10
        m22: 
            xor dx, dx
            div bx
            add dl, '0'
            push dx
            inc cx
            test ax, ax
            jnz m22
        m33:
            pop ax
            int 29H
            loop m33
            ret
    write_remainder endp
CSEG ENDS
END MAIN