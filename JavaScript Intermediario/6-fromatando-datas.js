function formateDate (date) {
    return date.replace(/(\d{2})\/(\d{2})\/(\d{4})/, '$3-$2-$1');
}

console.log(formateDate('04/10/2022'));