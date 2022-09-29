function mensagem(){
    var num = Math.trunc((Math.random()*3)+1);
    switch(num){
        case 1: console.log("oi"); break;
        case 2: console.log("olá"); break;
        case 3: console.log("iai"); break;
    }
}