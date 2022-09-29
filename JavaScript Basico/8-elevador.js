var Elevador = {
    andar: 0,
    topo: 3,
    print(){
        console.log(`Andar: ${this.andar}`);
    },
    subir(){
        if(this.andar < 3){
            this.andar++;
        }
        this.print();
    },
    descer(){
        if(this.andar > 0){
            this.andar--;
        }
        this.print();
    }
}