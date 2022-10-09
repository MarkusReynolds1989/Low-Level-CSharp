#include <stddef.h>

extern int max(const int* input, size_t length){
    int result = -2147483648;

    for(size_t i = 0; i < length; i += 1){
        if(input[i] > result) {
            result = input[i];
        }
    } 

    return result;
}