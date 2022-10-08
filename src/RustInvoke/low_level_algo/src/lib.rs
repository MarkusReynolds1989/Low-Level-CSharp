#[no_mangle]
pub unsafe extern fn reverse_array(input: *mut [i32], count: usize) {
    // Modifies the i32 array in place by reversing it.
    let mut end = count - 1;
    let mut start = 0;

    while start < end {
        let temp = (*input)[end];
        (*input)[end] = (*input)[start];
        (*input)[start] = temp;

        start += 1;
        end -= 1;
    }
}