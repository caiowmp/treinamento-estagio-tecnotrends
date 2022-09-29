function letrasFaltando(texto){
    texto = texto.toLowerCase();
    var letras = ['a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'];
    var faltando = [];
    for (let letra of letras){
        if(texto.indexOf(letra) === -1){
            faltando.push(letra);
        }
    }
    return faltando;
}