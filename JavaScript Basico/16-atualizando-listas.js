var list = [1,2,3],
	listElement = document.querySelector('ul');

function insert(event){
	if(event.keyCode === 13){
		list.push(event.target.value);
		event.target.value = '';
		render();
	}
}

function remove(index){
	list.splice(index, 1);
	render();
}

function render(){
	listElement.innerHTML = list.map((item, index) => {
		return `<li onclick="remove(${index})" >${item}</li>`;
	}).join('');
}


render();