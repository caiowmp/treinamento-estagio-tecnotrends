function setState(){
    var values = getValues();
    history.pushState({values}, 'checkboxes');
}


function getValues(){
    var a = document.querySelector('#a').checked,
        b = document.querySelector('#b').checked,
        c = document.querySelector('#c').checked;
    return {a, b, c};
}

window.onpopstate = function(){
    var values = history.state.values;

    document.querySelector('#a').checked = values.a;
    document.querySelector('#b').checked = values.b;
    document.querySelector('#c').checked = values.c;
}