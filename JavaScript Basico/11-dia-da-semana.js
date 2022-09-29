function diaSemana(ano,mes,dia){
    var data = (new Date(ano,mes-1,dia)).getDay();
    var days = ['Dom','Seg','Ter','Qua','Qui','Sex','Sab'];
    return days[data];
}