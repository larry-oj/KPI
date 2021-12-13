STSEG SEGMENT PARA STACK 'STACK'
    db 64 dup ('STACK')
STSEG ends

DSEG SEGMENT PARA PUBLIC 'DATA'
    input_str db "Enter your number (-4681 to 4681): $" ; because 4681 * 7 = 32767
    error_str db "Error! Your number is either too big, or is not a number at all! $"
    result_str db "Result: $"
    endline_str db 13,10,'$'

    input db 6, ?, 6 dup ('?')
    result db 7 dup ('?')

    multiplier dw 7
    digit dw 10    
DSEG ends

CSEG SEGMENT PARA PUBLIC 'CODE'
    MAIN proc far
        assume cs: CSEG, ds: DSEG, ss: STSEG
        
        push ds

        mov ax, 0
        push ax
        
        mov ax, DSEG
        mov ds, ax

        ; write instructions
        lea dx, input_str
        call write

        ; read input
        lea dx, input
        call readline

        ; call conversion
        call convert_to_num

        and dx, 7 ; dx is set to 7 if error happened in convert_to_num proc
        jnz error_message
        imul multiplier
        jo error_message ; if overflow happens after imul

        ; convert to string to output
        call convert_to_str

        ; write result
        lea dx, result_str
        call write
        lea dx, result
        call write
        
        ; end program
        jmp end_main

        error_message:
            lea dx, error_str
            call writeline
        
        end_main:
            ret
    MAIN endp

    
    ; write
    write proc far
        ; required before call:
        ; lea dx, [string]

        mov ah, 9
        int 21h

        ret
	write endp


    ; write with endline
    writeline proc far
        ; required before call:
        ; lea dx, [string]

        call write
        
        lea dx, endline_str
        int 21h

        ret
	writeline endp


    ; read and save
    readline proc
        ; required before call:
        ; lea dx, [where to save]

        mov ah, 0Ah	
        int 21h

        lea dx, endline_str
        call write

        ret
    readline endp


    ; convert string to num
    convert_to_num proc
        lea si, input+1

        mov cx, 0
        mov ax, 0
        mov bx, 0
        mov dx, 0
        mov di, 0

        mov cl, [si]
        inc si

        mov bl, [si] ; first char in string
        cmp bl, '-'
        jne convert_to_num_loop ; if number is positive

        mov di, 1 ; if number ss negative
        dec cx
        inc si

        convert_to_num_loop:
            mov bl, [si] ; current char
            inc si ; next char

            ; check if the current symbos is a number
            cmp bl, '0'
            jb mark_error
            cmp bl, '9'
            ja mark_error
            
            sub bl, '0' ; get number form current symbol

            ; if overflow
            mul digit
            jo mark_error
            add ax, bx ; add to result
            jo mark_error

            loop convert_to_num_loop

        mov dx, 0;
        and di, 1 ; if di == 0, the number is positive
        jz end_proc
        neg ax
        jmp end_proc

        mark_error:
            mov dx, 7

        end_proc:
            ret
    convert_to_num endp


    ; convert string to num
    convert_to_str proc
        lea si, result

        mov cx, 0

        ; check if number is positive
        or ax, ax
        jns positive

        ; set '-' at the beggining of result
        mov dl, '-'
        mov [si], dl

        inc si ; inc address (set after '-')
        neg ax ; make num positive

        positive:
            mov dx, 0
            div digit ; ax = ax / error_checker
            add dl, '0' ; add '0' to get symbol code
            push dx
            inc cx
            test ax, ax ; check for remaining numbers
            jnz positive

        write_loop:
            pop ax
            mov [si], al ; write chat to current si address
            inc si ; increase address
            loop write_loop

        ; add '$' to the end of a string
        mov dl, '$'
        mov [si], dl
        ret
    convert_to_str endp
    
CSEG ends
END MAIN