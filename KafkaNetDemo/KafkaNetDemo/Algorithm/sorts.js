let a = [5,3,67,8,4,61,7,92,65,2];

//insert item into the correct position
function insertionSort(arr) {
  let n = arr.length;
  let i,j, last;
  for(i = 1; i < n; i++) {
    let last = arr[i];
    j = i;
    while(j>=0 && arr[j-1]>=arr[j]) {
      arr[j] = arr[j-1];
      j--;
    }
    arr[j] = last;
  }
  return arr;
}

console.log('insertionSort');
a = insertionSort(a);
console.log(a);

//
a = [5,3,67,8,4,61,7,92,65,2];
function interchangeSort(arr) {
  let n = arr.length;
  let i,j,t;
  for(i = 0; i < n-1; i++) {
    for(j = i+1; j < n; j++) {
      if(arr[i]>arr[j]) {
        t = arr[i];
        arr[i] = arr[j];
        arr[j] = t;
      }
    }
  }
  return arr;
}

console.log('interchangeSort');
a = interchangeSort(a);
console.log(a);

//choose the min value and exchange it with the current value
a = [5,3,67,8,4,61,7,92,65,2];
function selectionSort(arr) {
  let n = arr.length;
  let i,j,min,t;
  for(i = 0; i < n-1; i++) {
    min=i;
    for(j = i+1; j < n; j++) {
      if(arr[min]>arr[j]) {
        min = j;
      }
    }
    t = arr[i];
    arr[i] = arr[min];
    arr[min] = t;
  }
  return arr;
}

console.log('selectionSort');
a = selectionSort(a);
console.log(a);

//bubbleSort
a = [5,3,67,8,4,61,7,92,65,2];
function bubbleSort(arr) {
  let n = arr.length;
  let i,j,min,t;
  for(i = n-1; i > 0; i--) {
    for(j = 0; j < i; j++) {
      if(arr[j] > arr[j+1]) {
        t = arr[j];
        arr[j] = arr[j+1];
        arr[j+1] = t;
      }
    }
  }
  return arr;
}

console.log('bubbleSort');
a = bubbleSort(a);
console.log(a);

//bubbleSort
a = [5,3,67,8,4,61,7,92,65,2];
function bubbleSort(arr) {
  let n = arr.length;
  let i,j,min,t;
  for(i = n-1; i > 0; i--) {
    for(j = 0; j < i; j++) {
      if(arr[j] > arr[j+1]) {
        t = arr[j];
        arr[j] = arr[j+1];
        arr[j+1] = t;
      }
    }
  }
  return arr;
}

console.log('bubbleSort');
a = bubbleSort(a);
console.log(a);