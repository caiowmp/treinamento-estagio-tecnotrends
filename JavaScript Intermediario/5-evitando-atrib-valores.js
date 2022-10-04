var myObj = {
    value: 0,
    get a() {
        return this.value;
    },
    set a(x) {
        if(x % 2 !== 0) {
            throw new Error();
        }
        this.valor = x;
    }
}