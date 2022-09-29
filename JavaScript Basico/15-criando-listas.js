var list = [1,2,3],
    listElement = document.querySelector('ul');

function insertList(){
    var html = '';
    for (let item of list){
        html += `<li>Item ${item}</li>`;
    }
    listElement.innerHTML = html;
}

insertList();