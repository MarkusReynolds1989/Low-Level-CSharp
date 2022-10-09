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

#[no_mangle]
// Takes a function, an array, and a count and then iterates over it mutating the state in the array
// by applying the function supplied.
pub unsafe extern fn iterate_over(function: fn(i32) -> i32, input: *mut [i32], count: usize) {
    // If for some reason we determined that the Rust for was so much faster
    // so we decided to use that for mutation.
    for x in 0..count {
        (*input)[x] = function((*input)[x])
    }
}

// Implement a function that sums up the given array of ints.
#[no_mangle]
pub unsafe extern fn sum_array(input: *const [i32], count: usize) -> i32 {
    println!("Not yet implemented.");
    0
}