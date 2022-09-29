var texto = '';

for(var i = 0; i < 10; i++){
    texto += '*';
    console.log(texto);
}

for(var i = 10; i > 0; i--){
    var texto2 = ''
    for(var j = 0; j < i; j++){
        texto2 += '*';
    }
    console.log(texto2);
}