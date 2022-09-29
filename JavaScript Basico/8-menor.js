var arr = [1,2,3,6,8,0,9,63];

function menor(myArr){
    myArr.sort((a,b) => a > b ? 1 : -1);
    return myArr[0];
}

console.log(menor(arr));